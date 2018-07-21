using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                fillPaymentTypeddl();
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
                    var query = (from c in entities.tblPayments
                                 where c.PAYMENT_ID == iPaymentID
                                 select c).FirstOrDefault();
                    txtAmount.Text = query.PAYMENT_AMOUNT.ToString();
                    string date = query.PAYMENT_DATE.ToString();
                    DateTime dt = DateTime.ParseExact(date, "dd-MM-yyyy hh:mm:ss tt", null);
                    txtDOP.Text = dt.ToString("MM/dd/yyyy");
                    var objStudent = entities.tblStudents.Where(s => s.STU_ID == query.STU_ID).OrderByDescending(s => s.STU_NUM).FirstOrDefault();
                    txtStudentNumber.Text = objStudent.STU_NUM;
                    ddlPaymentType.SelectedValue = query.PAYMENT_TYPE_ID.ToString();
                    if (query.PAYMENT_STATUS == true)
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
        public void fillPaymentTypeddl()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    var category = (from c in entities.tblPaymentTypes
                                    where c.PAYMENT_TYPE_STATUS == true
                                    select new { c.PAYMENT_TYPE_ID, c.PAYMENT_TYPE }).ToList();

                    ddlPaymentType.DataValueField = "PAYMENT_TYPE_ID";
                    ddlPaymentType.DataTextField = "PAYMENT_TYPE";
                    
                    ddlPaymentType.DataSource = category;
                    ddlPaymentType.DataBind();
                   // ddlPaymentType.Items.Add(new ListItem("Select Payment Type", "0"));
                    ddlPaymentType.Items.Insert(0, new ListItem("--Select Payment Type--"));
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAddPayment_Click(object sender, EventArgs e)
        {
            InsertPayment();
        }
        public void InsertPayment()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from obj in entities.tblStudents
                                     where obj.STU_NUM == txtStudentNumber.Text
                                     select obj.STU_ID).FirstOrDefault();


                    //var query = entities.tblStudents.AsEnumerable().Where(s => s.STU_NUM.Contains(txtStudentNumber.Text)).Select(s => s.STU_ID).Distinct();
                    if (studentID != 0)
                    {
                        int ihdnPKID = int.Parse(hdnPKId.Value);
                        var objPayment = entities.tblPayments.Where(s => s.PAYMENT_ID == ihdnPKID).OrderByDescending(s => s.PAYMENT_ID).FirstOrDefault();
                        if (objPayment == null)
                        {
                            objPayment = new tblPayment();
                        }
                        objPayment.PAYMENT_AMOUNT = Convert.ToDecimal(txtAmount.Text);
                        objPayment.PAYMENT_DATE = DateTime.ParseExact(txtDOP.Text, "MM/dd/yyyy", null);
                        objPayment.PAYMENT_TYPE_ID =Convert.ToInt32(ddlPaymentType.SelectedValue);
                        objPayment.PAYMENT_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objPayment.PAYMENT_CREATED_DATE = DateTime.Now;
                        objPayment.STU_ID = Convert.ToInt32(studentID);
                        objPayment.PAYMENT_STATUS = chkIsApprove.Checked;

                        if (objPayment.PAYMENT_ID == 0)
                        {
                            entities.tblPayments.Add(objPayment);
                        }
                        else
                        {
                            objPayment.PAYMENT_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objPayment.PAYMENT_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objPayment).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objPayment = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("PaymentList.aspx");
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
            txtAmount.Text = "";
            txtDOP.Text = "";
            txtStudentNumber.Text = "";
            ddlPaymentType.SelectedIndex = 0;
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