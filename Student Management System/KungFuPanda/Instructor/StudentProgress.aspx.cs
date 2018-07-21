using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace KungFuPanda.Instructor
{
    public partial class StudentProgress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        [WebMethod()]
        public static string getStudentData()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from s in entities.tblStudents
                                     join sp in entities.tblStudentProgresses on s.STU_ID equals sp.STU_ID
                                     join r in entities.tblRanks on sp.RANK_ID equals r.RANK_ID
                                     orderby s.STU_ID descending
                                     select new
                                     {
                                         STU_ID = s.STU_ID,
                                         STU_NUM = s.STU_NUM
                                     }).ToList().Distinct();
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    var list = JsonConvert.SerializeObject(studentID,
                                Formatting.None,
                                new JsonSerializerSettings()
                                {
                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                });

                    return list;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod()]
        public static string getStudentProgressData(string strStudentID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int sid = int.Parse(strStudentID);
                    var studentID = (from s in entities.tblStudents
                                     join sp in entities.tblStudentProgresses on s.STU_ID equals sp.STU_ID
                                     join r in entities.tblRanks on sp.RANK_ID equals r.RANK_ID
                                     where s.STU_ID == sid
                                     orderby sp.STU_PROG_RANK_DATE descending
                                     select new
                                     {
                                         STU_PROG_RANK_DATE = sp.STU_PROG_RANK_DATE,
                                         STU_ID = s.STU_ID,
                                         STU_NUM = s.STU_NUM,
                                         RANK_BELT_COLOR = r.RANK_BELT_COLOR
                                         
                                     }).ToList();
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    var list = JsonConvert.SerializeObject(studentID,
                                Formatting.None,
                                new JsonSerializerSettings()
                                {
                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                });

                    return list;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}