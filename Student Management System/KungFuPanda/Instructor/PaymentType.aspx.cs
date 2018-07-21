using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class PaymentType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["ID"].ToString() != "")
                    {
                        hdnPKId.Value = Request.QueryString["ID"].ToString();
                        setValuesToControls();
                    }
                }
            }
        }
        public void setValuesToControls()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iPaymentID = int.Parse(hdnPKId.Value);
                    var query = (from p in entities.tblPaymentTypes
                                 where p.PAYMENT_TYPE_ID == iPaymentID
                                 select p).FirstOrDefault();
                    txtPaymentType.Text = query.PAYMENT_TYPE;
                    txtAmount.Text = query.PAYMENT_TYPE_AMOUNT.ToString();
                    if (query.PAYMENT_TYPE_STATUS == true)
                    {
                        chkIsApprove.Checked = true;
                    }
                    else
                    {
                        chkIsApprove.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAddPaymentType_Click(object sender, EventArgs e)
        {
            InsertPaymentType();
        }
        public void InsertPaymentType()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int ihdnPKID = int.Parse(hdnPKId.Value);
                    Int32 PaymentTypeID = 0;
                    if (ihdnPKID == 0)
                    {
                         PaymentTypeID = (from obj in entities.tblPaymentTypes
                                             where obj.PAYMENT_TYPE == txtPaymentType.Text
                                             select obj.PAYMENT_TYPE_ID).FirstOrDefault();
                    }
                    else
                    {
                         PaymentTypeID = (from obj in entities.tblPaymentTypes
                                             where (obj.PAYMENT_TYPE == txtPaymentType.Text && obj.PAYMENT_TYPE_ID != ihdnPKID)
                                             select obj.PAYMENT_TYPE_ID).FirstOrDefault();
                    }
                    
                    if (PaymentTypeID == 0)
                    {
                        
                        var objPaymentType = entities.tblPaymentTypes.Where(s => s.PAYMENT_TYPE_ID == ihdnPKID).OrderByDescending(s => s.PAYMENT_TYPE_ID).FirstOrDefault();
                        if (objPaymentType == null)
                        {
                            objPaymentType = new tblPaymentType();
                        }
                        objPaymentType.PAYMENT_TYPE = txtPaymentType.Text;
                        objPaymentType.PAYMENT_TYPE_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objPaymentType.PAYMENT_TYPE_CREATED_DATE = DateTime.Now;
                        objPaymentType.PAYMENT_TYPE_STATUS = chkIsApprove.Checked;
                        objPaymentType.PAYMENT_TYPE_AMOUNT = Convert.ToDecimal(txtAmount.Text);

                        if (objPaymentType.PAYMENT_TYPE_ID == 0)
                        {
                            entities.tblPaymentTypes.Add(objPaymentType);
                        }
                        else
                        {
                            objPaymentType.PAYMENT_TYPE_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objPaymentType.PAYMENT_TYPE_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objPaymentType).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objPaymentType = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("PaymentTypeList.aspx");
                        }

                        
                        ResetControls();
                        divMessage.Visible = true;
                        divErrorMsg.Visible = false;
                    }
                    else
                    {

                        divErrorMsg.Visible = true;
                        divMessage.Visible = false;
                        return;
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }

        public void ResetControls()
        {
            txtPaymentType.Text = "";
            txtAmount.Text = "";
            chkIsApprove.Checked = true;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
            divMessage.InnerHtml = "";
            divMessage.InnerText = "Data resetted successfully.";
            divMessage.Visible = true;
        }
    }
}