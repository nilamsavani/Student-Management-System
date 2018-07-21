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
    public partial class StudentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        [WebMethod()]
        //public static string getStudentData(string strFromDate,string strToDate)
        public static string getStudentData()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    // DateTime strFromDate1 = DateTime.ParseExact(strFromDate, "MM/dd/yyyy", null);
                    //DateTime strToDate1 = DateTime.ParseExact(strToDate, "MM/dd/yyyy", null);
                    //var studentID = (from c in entities.tblStudents
                    //                 join p in entities.tblParents on c.STU_ID equals p.STU_ID into tmpMapp
                    //                 from p in tmpMapp.DefaultIfEmpty()
                    //                 select new
                    //                 {
                    //                     STU_ID = c.STU_ID,
                    //                     STU_FNAME = c.STU_FNAME,
                    //                     STU_MNAME = c.STU_MNAME,
                    //                     STU_LNAME = c.STU_LNAME,
                    //                     STU_NUM = c.STU_NUM,
                    //                     STU_DOB = c.STU_DOB,
                    //                     STU_DOJ = c.STU_DOJ,
                    //                     STU_MNUM = c.STU_MNUM,
                    //                     STU_EMAIL = c.STU_EMAIL,
                    //                     STU_STREET = c.STU_STREET,
                    //                     STU_CITY = c.STU_CITY,
                    //                     STU_STATE = c.STU_STATE,
                    //                     STU_ZIP = c.STU_ZIP,
                    //                     STU_STATUS = c.STU_STATUS,
                    //                     PARENT_NAME = p.PARENT_FNAME + " " + p.PARENT_MNAME +" " + p.PARENT_LNAME,
                    //                     PARENT_GENDER= p.PARENT_GENDER
                    //                 }).ToList();
                    var studentID = (from c in entities.tblStudents
                                         // where (c.STU_DOB >= strFromDate1 && c.STU_DOB <= strToDate1)
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
                                     }).ToList().OrderByDescending(x => x.STU_ID);
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
        public static string getStudentParentData(string strStudentID)
        {

            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var StudentID = int.Parse(strStudentID);
                    var parent = (from p in entities.tblParents
                                  where p.STU_ID == StudentID
                                  orderby p.PARENT_ID descending
                                  select new
                                  {
                                      PARENT_NAME = p.PARENT_FNAME + " " + p.PARENT_MNAME + " " + p.PARENT_LNAME,
                                      PARENT_GENDER = p.PARENT_GENDER,
                                      PARENT_EMAIL = p.PARENT_EMAIL,
                                      PARENT_IS_STUDENT = p.PARENT_IS_STUDENT,
                                      PARENT_MNUM = p.PARENT_MNUM,
                                      PARENT_ID = p.PARENT_ID

                                  }).ToList();
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    var list = JsonConvert.SerializeObject(parent,
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
    }
}