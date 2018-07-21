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
    public partial class AssignRankList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        [WebMethod()]
        public static string getAssignedRankdata()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from c in entities.tblStudentProgresses
                                     orderby c.STU_PROG_ID descending
                                     select new
                                     {
                                         STU_PROG_ID = c.STU_PROG_ID,
                                         STU_PROG_STATUS = c.STU_PROG_STATUS,
                                         STU_NUM = c.tblStudent.STU_NUM,
                                         RANK_BELT_COLOR = c.tblRank.RANK_BELT_COLOR,
                                         STU_PROG_RANK_DATE = c.STU_PROG_RANK_DATE
                                     }).ToList();
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    var list = JsonConvert.SerializeObject(studentID,
                                Formatting.None,
                                new JsonSerializerSettings()
                                {
                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                });

                    //return Content(list, "application/json");
                    return list;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod()]
        public static string ChangeStatus(string strAssignRankID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iAssignRankID = int.Parse(strAssignRankID);
                    var query = (from c in entities.tblStudentProgresses
                                 where c.STU_PROG_ID == iAssignRankID
                                 select c).Single();

                    if (query.STU_PROG_STATUS == true)
                    {
                        query.STU_PROG_STATUS = false;
                    }
                    else
                    {
                        query.STU_PROG_STATUS = true;
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
            Response.Redirect("AssignRank.aspx");
        }
    }
}