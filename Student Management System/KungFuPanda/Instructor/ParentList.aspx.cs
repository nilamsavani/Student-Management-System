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
    public partial class ParentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }
        }
        [WebMethod()]
        public static string getParentsdata()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    
                    var studentID = (from c in entities.tblParents
                                     join s in entities.tblStudents on c.STU_ID equals s.STU_ID
                                     orderby c.PARENT_ID descending
                                     select new
                                     {
                                         PARENT_ID = c.PARENT_ID,
                                         STU_NUM = s.STU_NUM,
                                         PARENT_FNAME = c.PARENT_FNAME,
                                         PARENT_MNAME = c.PARENT_MNAME,
                                         PARENT_LNAME = c.PARENT_LNAME,
                                         PARENT_MNUM = c.PARENT_MNUM,
                                         PARENT_EMAIL = c.PARENT_EMAIL,
                                         PARENT_GENDER = c.PARENT_GENDER,
                                         PARENT_STATUS = c.PARENT_STATUS,
                                         PARENT_IS_STUDENT = c.PARENT_IS_STUDENT
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
        public static string ChangeStatus(string strParentID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iParentID = int.Parse(strParentID);
                    var query = (from c in entities.tblParents
                                    where c.PARENT_ID == iParentID
                                 select c).Single();

                    if (query.PARENT_STATUS == true)
                    {
                        query.PARENT_STATUS = false;
                    }
                    else
                    {
                        query.PARENT_STATUS = true;
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
            Response.Redirect("Parent.aspx");
        }
    }
}