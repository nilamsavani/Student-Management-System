using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class Parent : System.Web.UI.Page
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
                    int iParentID = int.Parse(hdnPKId.Value);
                    var query = (from p in entities.tblParents
                                 where p.PARENT_ID == iParentID
                                 select p).FirstOrDefault();
                    txtFName.Text = query.PARENT_FNAME;
                    txtMName.Text = query.PARENT_MNAME;
                    txtLName.Text = query.PARENT_LNAME;
                    txtEmail.Text = query.PARENT_EMAIL;
                    txtMobileNumber.Text = query.PARENT_MNUM;
                    var objStudent = entities.tblStudents.Where(s => s.STU_ID == query.STU_ID).OrderByDescending(s => s.STU_NUM).FirstOrDefault();
                    txtStudentNumber.Text = objStudent.STU_NUM;
                    if (query.PARENT_GENDER == true)
                    {
                        rbtnMale.Checked = true;
                        rbtnFemale.Checked = false;
                    }
                    else {
                        rbtnMale.Checked = false;
                        rbtnFemale.Checked = true;
                    }
                    if (query.PARENT_IS_STUDENT == true)
                    {
                        chkIsStudent.Checked = true;
                    }
                    else
                    {
                        chkIsStudent.Checked = false;
                    }

                    if (query.PARENT_STATUS == true)
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
        protected void btnAddParent_Click(object sender, EventArgs e)
        {
            InsertParent();
        }
        public void InsertParent()
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
                        var objParent = entities.tblParents.Where(s => s.PARENT_ID == ihdnPKID).OrderByDescending(s => s.PARENT_ID).FirstOrDefault();
                        if (objParent == null)
                        {
                            objParent = new tblParent();
                        }
                        objParent.PARENT_FNAME = txtFName.Text;
                        objParent.PARENT_MNAME = txtMName.Text;
                        objParent.PARENT_LNAME = txtLName.Text;
                        objParent.PARENT_MNUM = txtMobileNumber.Text;
                        objParent.PARENT_EMAIL = txtEmail.Text;
                        objParent.PARENT_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objParent.PARENT_CREATED_DATE = DateTime.Now;
                        objParent.STU_ID = Convert.ToInt32(studentID);
                        if (rbtnMale.Checked)
                        {
                            objParent.PARENT_GENDER = true;
                        }
                        else
                        {
                            objParent.PARENT_GENDER = false;
                        }
                        objParent.PARENT_IS_STUDENT = chkIsStudent.Checked;
                        objParent.PARENT_STATUS = chkIsApprove.Checked;

                        if (objParent.PARENT_ID == 0)
                        {
                            entities.tblParents.Add(objParent);
                        }
                        else
                        {
                            objParent.PARENT_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objParent.PARENT_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objParent).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objParent = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("ParentList.aspx");
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
            txtFName.Text = "";
            txtMName.Text = "";
            txtLName.Text = "";
            txtEmail.Text = "";
            txtStudentNumber.Text = "";
            txtMobileNumber.Text = "";
            rbtnMale.Checked = true;
            rbtnFemale.Checked = false;
            chkIsStudent.Checked = false;
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