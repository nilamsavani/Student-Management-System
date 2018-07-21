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
    public partial class PaymentTransaction : System.Web.UI.Page
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

                    var studentID = (from p in entities.tblPayments
                                     join s in entities.tblStudents on p.STU_ID equals s.STU_ID
                                     group p by new { p.STU_ID, s.STU_NUM } into g
                                     orderby g.Key.STU_ID descending
                                     select new
                                     {
                                         STU_ID = g.Key.STU_ID,
                                         STU_NUM = g.Key.STU_NUM,
                                         TotalAmount = g.Sum(p => p.PAYMENT_AMOUNT),
                                         TotalPaidAmount = g.Where(p => p.PAYMENT_STATUS == true).Sum(d => (Int32?)d.PAYMENT_AMOUNT),
                                         TotalAmountToBePaid = g.Where(p => p.PAYMENT_STATUS == false).Sum(d => (Int32?)d.PAYMENT_AMOUNT),
                                     }).ToList().Select(x => new {
                                         STU_NUM = x.STU_NUM,
                                         TotalAmount = x.TotalAmount == null? 0 : x.TotalAmount.Value,
                                         TotalPaidAmount = x.TotalPaidAmount == null ? 0 : x.TotalPaidAmount.Value,
                                         TotalAmountToBePaid = x.TotalAmountToBePaid == null ? 0 : x.TotalAmountToBePaid.Value,
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