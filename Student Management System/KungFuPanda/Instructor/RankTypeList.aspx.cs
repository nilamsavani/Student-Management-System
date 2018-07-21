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
    public partial class RankTypeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        [WebMethod()]
        public static string getRankdata()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from c in entities.tblRanks
                                     orderby c.RANK_ID descending
                                     select new
                                     {
                                         RANK_ID = c.RANK_ID,
                                         RANK_BELT_COLOR = c.RANK_BELT_COLOR,
                                         RANK_REQUIREMENTS = c.RANK_REQUIREMENTS,
                                         RANK_STATUS = c.RANK_STATUS,
                                         RANK_IS_DEFAULT = c.RANK_IS_DEFAULT
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
        public static string ChangeStatus(string strRankID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iRankID = int.Parse(strRankID);
                    var query = (from c in entities.tblRanks
                                 where c.RANK_ID == iRankID
                                 select c).Single();

                    if (query.RANK_STATUS == true)
                    {
                        query.RANK_STATUS = false;
                    }
                    else
                    {
                        query.RANK_STATUS = true;
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
            Response.Redirect("RankType.aspx");
        }
    }
}