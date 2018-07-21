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
    public partial class AttendanceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        [WebMethod()]
        public static string getAttendancedata()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from s in entities.tblStudents
                                     join a in entities.tblAttendences on s.STU_ID equals a.STU_ID
                                     join c in entities.tblClasses on a.CLASS_ID equals c.CLASS_ID
                                     join cl in entities.tblClassLevels on c.CLASS_LEVEL_ID equals cl.CLASS_LEVEL_ID
                                     orderby a.ATT_ID descending
                                     select new
                                     {
                                         ATT_DATE = a.ATT_DATE,
                                         STU_NUM = s.STU_NUM,
                                         ATT_STATUS = a.ATT_STATUS,
                                         CLASS_DAY = c.CLASS_DAY,
                                         CLASS_TIME = c.CLASS_TIME,
                                         CLASS_LEVEL_NAME = cl.CLASS_LEVEL_NAME,
                                         ATT_ID = a.ATT_ID
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
        [WebMethod()]
        public static string ChangeStatus(string strAttendanceID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iAttendanceID = int.Parse(strAttendanceID);
                    var query = (from c in entities.tblAttendences
                                 where c.ATT_ID == iAttendanceID
                                 select c).Single();

                    if (query.ATT_STATUS == true)
                    {
                        query.ATT_STATUS = false;
                    }
                    else
                    {
                        query.ATT_STATUS = true;
                    }
                    entities.SaveChanges();
                }
                return "True";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAddParent_Click(object sender, EventArgs e)
        {
            Response.Redirect("Attendance.aspx");
        }
    }
}