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
    public partial class PaymentTypeList : System.Web.UI.Page
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

                    var studentID = (from c in entities.tblPaymentTypes
                                     orderby c.PAYMENT_TYPE_ID descending
                                     select new
                                     {
                                         PAYMENT_TYPE_ID = c.PAYMENT_TYPE_ID,
                                         PAYMENT_TYPE = c.PAYMENT_TYPE,
                                         PAYMENT_TYPE_AMOUNT = c.PAYMENT_TYPE_AMOUNT,
                                         PAYMENT_TYPE_STATUS =c.PAYMENT_TYPE_STATUS
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
        public static string ChangeStatus(string strPaymentTypeID)
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iPaymentTypeID = int.Parse(strPaymentTypeID);
                    var query = (from c in entities.tblPaymentTypes
                                 where c.PAYMENT_TYPE_ID == iPaymentTypeID
                                 select c).Single();

                    if (query.PAYMENT_TYPE_STATUS == true)
                    {
                        query.PAYMENT_TYPE_STATUS = false;
                    }
                    else
                    {
                        query.PAYMENT_TYPE_STATUS = true;
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
            Response.Redirect("PaymentType.aspx");
        }
    }
}