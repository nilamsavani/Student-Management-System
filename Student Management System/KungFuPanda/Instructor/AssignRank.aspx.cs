using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda
{
    public partial class AssignRank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillRankTypeddl();
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
                    int iStudentProgressID = int.Parse(hdnPKId.Value);
                    var query = (from p in entities.tblStudentProgresses
                                 where p.STU_PROG_ID == iStudentProgressID
                                 select p).FirstOrDefault();

                    
                    ddlRankType.SelectedValue = query.RANK_ID.ToString();

                    string date = query.STU_PROG_RANK_DATE.ToString();
                    DateTime dt = DateTime.ParseExact(date, "dd-MM-yyyy hh:mm:ss tt", null);
                    txtRankDate.Text = dt.ToString("MM/dd/yyyy");
                    if (query.STU_PROG_STATUS == true)
                    {
                        chkIsApprove.Checked = true;
                    }
                    else
                    {
                        chkIsApprove.Checked = false;
                    }
                    var objStudent = entities.tblStudents.Where(s => s.STU_ID == query.STU_ID).OrderByDescending(s => s.STU_NUM).FirstOrDefault();
                    txtStudentNumber.Text = objStudent.STU_NUM;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void fillRankTypeddl()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    var category = (from c in entities.tblRanks
                                    where c.RANK_STATUS == true
                                    select new { c.RANK_ID, c.RANK_BELT_COLOR }).ToList();

                    ddlRankType.DataValueField = "RANK_ID";
                    ddlRankType.DataTextField = "RANK_BELT_COLOR";
                    ddlRankType.DataSource = category;
                    ddlRankType.DataBind();
                    ddlRankType.Items.Insert(0, new ListItem("--Select Belt Type--"));
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAssignRank_Click(object sender, EventArgs e)
        {
            AssignRankStudent();
        }
        public void AssignRankStudent()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from obj in entities.tblStudents
                                     where obj.STU_NUM == txtStudentNumber.Text
                                     select obj.STU_ID).FirstOrDefault();
                    var rankID = int.Parse(ddlRankType.SelectedValue);

                    int ihdnPKID = int.Parse(hdnPKId.Value);
                    Int32 studentprogressID = 0;
                    if (ihdnPKID == 0)
                    {
                        studentprogressID = (from obj in entities.tblStudentProgresses
                                             where obj.RANK_ID == rankID && obj.STU_ID == studentID
                                             select obj.STU_PROG_ID).FirstOrDefault();
                    }
                    else
                    {
                        studentprogressID = (from obj in entities.tblStudentProgresses
                                             where obj.RANK_ID == rankID && obj.STU_ID == studentID && obj.STU_PROG_ID != ihdnPKID
                                             select obj.STU_PROG_ID).FirstOrDefault();

                    }

                    if (studentID != 0 && studentprogressID == 0)
                    {
                        var objStudentProgress = entities.tblStudentProgresses.Where(s => s.STU_PROG_ID == ihdnPKID).OrderByDescending(s => s.STU_PROG_ID).FirstOrDefault();
                        if (objStudentProgress == null)
                        {
                            objStudentProgress = new tblStudentProgress();
                        }
                        objStudentProgress.RANK_ID = Convert.ToInt32(ddlRankType.SelectedValue);
                        objStudentProgress.STU_ID = Convert.ToInt32(studentID);
                        objStudentProgress.STU_PROG_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objStudentProgress.STU_PROG_CREATED_DATE = DateTime.Now;
                        objStudentProgress.STU_PROG_RANK_DATE = DateTime.ParseExact(txtRankDate.Text, "MM/dd/yyyy", null);
                        objStudentProgress.STU_PROG_STATUS = chkIsApprove.Checked;
                        if (objStudentProgress.STU_PROG_ID == 0)
                        {
                            entities.tblStudentProgresses.Add(objStudentProgress);
                        }
                        else
                        {
                            objStudentProgress.STU_PROG_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objStudentProgress.STU_PROG_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objStudentProgress).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objStudentProgress = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("AssignRankList.aspx");
                        }
                        ResetControls();
                        divMessage.Visible = true;
                        divErrorMsg.Visible = false;
                    }
                    else
                    {

                        if (studentID == 0)
                        {
                            divErrorMsg.InnerHtml = "";
                            divErrorMsg.InnerText = "Student does not exist with this number.";
                        }
                        else
                        {
                            divErrorMsg.InnerHtml = "";
                            divErrorMsg.InnerText = "This belt is already assigned to this student.";
                        }
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
            txtStudentNumber.Text = "";
            txtRankDate.Text = "";
            ddlRankType.SelectedIndex = 0;
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