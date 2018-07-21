using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Globalization;

namespace KungFuPanda.Instructor
{
    public partial class Student : System.Web.UI.Page
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
                    int iStuID = int.Parse(hdnPKId.Value);
                    var query = (from c in entities.tblStudents
                                 where c.STU_ID == iStuID
                                 select c).FirstOrDefault();
                    txtFName.Text = query.STU_FNAME;
                    txtMName.Text = query.STU_MNAME;
                    txtLName.Text = query.STU_LNAME;
                    txtEmail.Text = query.STU_EMAIL;
                    txtMobileNumber.Text = query.STU_MNUM;
                    rbtnMale.Checked = true;
                    rbtnFemale.Checked = false;

                    string date = query.STU_DOB.ToString();
                    DateTime dt = DateTime.ParseExact(date, "dd-MM-yyyy hh:mm:ss tt", null);
                    txtDOB.Text = dt.ToString("MM/dd/yyyy");

                    string date2 = query.STU_DOJ.ToString();
                    DateTime dt2 = DateTime.ParseExact(date2, "dd-MM-yyyy hh:mm:ss tt", null);
                    txtDOJ.Text = dt2.ToString("MM/dd/yyyy");

                    txtStreet.Text = query.STU_STREET;
                    txtCity.Text = query.STU_CITY;
                    txtState.Text = query.STU_STATE;
                    txtZIP.Text = query.STU_ZIP;

                    if (query.STU_STATUS == true)
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
        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            InsertStudent();
        }
        public void InsertStudent()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    //tblStudent obj = entities.tblStudents.AsEnumerable().LastOrDefault<tblStudent>();
                    int ihdnPKID = int.Parse(hdnPKId.Value);
                    tblStudentProgress objStudentProgress = new tblStudentProgress();
                    var objStudent = entities.tblStudents.Where(s => s.STU_ID == ihdnPKID).OrderByDescending(s => s.STU_ID).FirstOrDefault();
                    if (objStudent == null)
                    {
                        objStudent = new tblStudent();
                    }
                    objStudent.STU_FNAME = txtFName.Text;
                    objStudent.STU_MNAME = txtMName.Text;
                    objStudent.STU_LNAME = txtLName.Text;
                    objStudent.STU_MNUM = txtMobileNumber.Text;
                    objStudent.STU_EMAIL = txtEmail.Text;
                    objStudent.STU_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                    objStudent.STU_CREATED_DATE = DateTime.Now;
                    objStudent.STU_DOB = DateTime.ParseExact(txtDOB.Text, "MM/dd/yyyy", null);
                    objStudent.STU_DOJ = DateTime.ParseExact(txtDOJ.Text, "MM/dd/yyyy", null);
                    if (rbtnMale.Checked)
                    {
                        objStudent.STU_GENDER = true;
                    }
                    else
                    {
                        objStudent.STU_GENDER = false;
                    }
                    if (entities.tblStudents.AsEnumerable().OrderByDescending(s => s.STU_ID).FirstOrDefault() == null)
                    {
                        objStudent.STU_NUM = "104911000";
                    }
                    else
                    {
                        if (Convert.ToInt32(hdnPKId.Value) == 0)
                        {
                            objStudent.STU_NUM = Convert.ToString(Convert.ToInt32(entities.tblStudents.AsEnumerable().OrderByDescending(s => s.STU_ID).FirstOrDefault().STU_NUM) + 1);
                        }
                    }
                    objStudent.STU_STATUS = chkIsApprove.Checked;

                    objStudent.STU_STREET = txtStreet.Text;
                    objStudent.STU_CITY = txtCity.Text;
                    objStudent.STU_STATE = txtState.Text;
                    objStudent.STU_ZIP = txtZIP.Text;
                    if (objStudent.STU_ID == 0)
                    {
                        entities.tblStudents.Add(objStudent);
                    }
                    else
                    {
                        objStudent.STU_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                        objStudent.STU_MODIFIED_DATE = DateTime.Now;
                        entities.Entry(objStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    entities.SaveChanges();

                    if (hdnPKId.Value == "0")
                    {

                        objStudentProgress.RANK_ID = entities.tblRanks.AsEnumerable().Where(r => r.RANK_IS_DEFAULT == true).FirstOrDefault().RANK_ID;
                        objStudentProgress.STU_ID = objStudent.STU_ID;
                        objStudentProgress.STU_PROG_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objStudentProgress.STU_PROG_CREATED_DATE = DateTime.Now;
                        objStudentProgress.STU_PROG_RANK_DATE = DateTime.Now;
                        objStudentProgress.STU_PROG_STATUS = true;
                        entities.tblStudentProgresses.Add(objStudentProgress);
                        entities.SaveChanges();


                        var PaymentTypeID = (from c in entities.tblPaymentTypes
                                             where c.PAYMENT_TYPE_STATUS == true
                                             orderby c.PAYMENT_TYPE_ID descending
                                             select new { c.PAYMENT_TYPE_ID, c.PAYMENT_TYPE_AMOUNT }).ToList();
                        foreach (var item in PaymentTypeID)
                        {
                            tblPayment objPayment = new tblPayment();
                            objPayment.PAYMENT_AMOUNT = item.PAYMENT_TYPE_AMOUNT;
                            objPayment.PAYMENT_DATE = DateTime.Now;
                            objPayment.PAYMENT_TYPE_ID = item.PAYMENT_TYPE_ID;
                            objPayment.PAYMENT_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                            objPayment.PAYMENT_CREATED_DATE = DateTime.Now;
                            objPayment.STU_ID = objStudent.STU_ID;
                            objPayment.PAYMENT_STATUS = false;

                            entities.tblPayments.Add(objPayment);
                            entities.SaveChanges();
                            objPayment = null;
                        }
                    }
                    else {
                        Response.Redirect("StudentList.aspx");
                    }
                    hdnPKId.Value = objStudent.STU_ID.ToString();

                    objStudent = null;
                    objStudentProgress = null;
                    ResetControls();
                    divMessage.Visible = true;

                }
            }
            catch (Exception ex)
            {

            }

        }

        public void ResetControls()
        {
            txtFName.Text = "";
            txtMName.Text = "";
            txtLName.Text = "";
            txtEmail.Text = "";
            txtMobileNumber.Text = "";
            rbtnMale.Checked = true;
            rbtnFemale.Checked = false;
            txtDOB.Text = "";
            txtDOJ.Text = "";
            txtStreet.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZIP.Text = "";
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