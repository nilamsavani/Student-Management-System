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
    public partial class PaymentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        [WebMethod()]
        public static string getPaymentdata()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from c in entities.tblPayments
                                     join sp in entities.tblStudents on c.STU_ID equals sp.STU_ID
                                     join r in entities.tblPaymentTypes on c.PAYMENT_TYPE_ID equals r.PAYMENT_TYPE_ID
                                     orderby c.PAYMENT_ID descending
                                     select new
                                     {
                                         PAYMENT_ID = c.PAYMENT_ID,
                                         PAYMENT_STATUS = c.PAYMENT_STATUS,
                                         STU_NUM = sp.STU_NUM,
                                         PAYMENT_TYPE = r.PAYMENT_TYPE,
                                         PAYMENT_TYPE_AMOUNT= c.PAYMENT_AMOUNT,
                                         PAYMENT_DATE = c.PAYMENT_DATE
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
        public static string ChangeStatus(string strPaymentID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iPaymentID = int.Parse(strPaymentID);
                    var query = (from c in entities.tblPayments
                                 where c.PAYMENT_ID == iPaymentID
                                 select c).Single();

                    if (query.PAYMENT_STATUS == true)
                    {
                        query.PAYMENT_STATUS = false;
                    }
                    else
                    {
                        query.PAYMENT_STATUS = true;
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
            Response.Redirect("Payment.aspx");
        }
    }
}