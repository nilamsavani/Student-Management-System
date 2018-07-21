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
    public partial class StudentList : System.Web.UI.Page
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
                    
                    var studentID = (from c in entities.tblStudents
                                         // where (c.STU_DOB >= strFromDate1 && c.STU_DOB <= strToDate1)
                                     orderby c.STU_ID descending
                                     select new
                                     {
                                         STU_ID = c.STU_ID,
                                         STU_FNAME = c.STU_FNAME,
                                         STU_MNAME = c.STU_MNAME,
                                         STU_LNAME = c.STU_LNAME,
                                         STU_NUM = c.STU_NUM,
                                         STU_DOB = c.STU_DOB,
                                         STU_DOJ = c.STU_DOJ,
                                         STU_MNUM = c.STU_MNUM,
                                         STU_EMAIL = c.STU_EMAIL,
                                         STU_STREET = c.STU_STREET,
                                         STU_CITY = c.STU_CITY,
                                         STU_STATE = c.STU_STATE,
                                         STU_ZIP = c.STU_ZIP,
                                         STU_STATUS = c.STU_STATUS
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
        public static string ChangeStatus(string strStudentID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iStuID = int.Parse(strStudentID);
                    var query = (from c in entities.tblStudents
                                    where c.STU_ID == iStuID
                                    select c).Single();

                    if (query.STU_STATUS == true)
                    {
                        query.STU_STATUS = false;
                    }
                    else
                    {
                        query.STU_STATUS = true;
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
        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("Student.aspx");
        }
    }
}