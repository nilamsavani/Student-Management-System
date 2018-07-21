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
    public partial class ClassList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        [WebMethod()]
        public static string getClassdata()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from c in entities.tblClasses
                                     orderby c.CLASS_ID descending
                                     select new
                                     {
                                         CLASS_ID = c.CLASS_ID,
                                         CLASS_STATUS = c.CLASS_STATUS,
                                         CLASS_LEVEL_NAME = c.tblClassLevel.CLASS_LEVEL_NAME,
                                         CLASS_DAY = c.CLASS_DAY,
                                         CLASS_TIME = c.CLASS_TIME
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
        public static string ChangeStatus(string strClassID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iClassID = int.Parse(strClassID);
                    var query = (from c in entities.tblClasses
                                 where c.CLASS_ID == iClassID
                                 select c).Single();

                    if (query.CLASS_STATUS == true)
                    {
                        query.CLASS_STATUS = false;
                    }
                    else
                    {
                        query.CLASS_STATUS = true;
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
            Response.Redirect("Class.aspx");
        }
    }
}