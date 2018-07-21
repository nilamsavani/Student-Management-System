using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class Attendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillClassLevelddl();
                ddlClassType.Items.Clear();
                ddlClassType.Items.Insert(0, new ListItem("--Select Class--"));
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
        public void fillClassLevelddl()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    var category = (from c in entities.tblClassLevels
                                    where c.CLASS_STATUS == true
                                    select new { c.CLASS_LEVEL_ID, c.CLASS_LEVEL_NAME }).ToList();

                    ddlClassLevel.DataValueField = "CLASS_LEVEL_ID";
                    ddlClassLevel.DataTextField = "CLASS_LEVEL_NAME";

                    ddlClassLevel.DataSource = category;
                    ddlClassLevel.DataBind();
                    // ddlPaymentType.Items.Add(new ListItem("Select Payment Type", "0"));
                    ddlClassLevel.Items.Insert(0, new ListItem("--Select Class Level--"));
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void setValuesToControls()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int iAttID = int.Parse(hdnPKId.Value);
                    var query = (from c in entities.tblAttendences
                                 where c.ATT_ID == iAttID
                                 select c).FirstOrDefault();


                    var objStudent = entities.tblStudents.Where(s => s.STU_ID == query.STU_ID).OrderByDescending(s => s.STU_NUM).FirstOrDefault();
                    txtStudentNumber.Text = objStudent.STU_NUM;

                    string date = query.ATT_DATE.ToString();
                    DateTime dt = DateTime.ParseExact(date, "dd-MM-yyyy hh:mm:ss tt", null);
                    txtDate.Text = dt.ToString("MM/dd/yyyy");
                    if (query.ATT_STATUS == true)
                    {
                        chkIsPresent.Checked = true;
                    }
                    else {
                        chkIsPresent.Checked = false;
                    }
                    
                    
                    ddlClassLevel.SelectedValue = query.tblClass.CLASS_LEVEL_ID.ToString();
                    fillClassTypeddl();
                    ddlClassType.SelectedValue = query.CLASS_ID.ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAddAttendance_Click(object sender, EventArgs e)
        {
            InsertAttendance();
        }
        public void InsertAttendance()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    var studentID = (from obj in entities.tblStudents
                                     where obj.STU_NUM == txtStudentNumber.Text
                                     select obj.STU_ID).FirstOrDefault();
                    var classID = int.Parse(ddlClassType.SelectedValue);
                    //DateTime dtFrom = Convert.ToDateTime(txtDate.Text);
                    //var AttendanceID = (from obj in entities.tblAttendences
                    //               where obj.CLASS_ID == ddlClassType.SelectedIndex && obj.STU_ID == studentID && obj.ATT_DATE == dtFrom
                    //                    select obj.ATT_ID).FirstOrDefault();

                   

                    int ihdnPKID = int.Parse(hdnPKId.Value);
                    Int32 AttendanceID = 0;
                    if (ihdnPKID == 0)
                    {
                        AttendanceID = (from obj in entities.tblAttendences
                                        where obj.CLASS_ID == classID && obj.STU_ID == studentID
                                        select obj.ATT_ID).FirstOrDefault();
                    }
                    else
                    {
                        AttendanceID = (from obj in entities.tblAttendences
                                        where obj.CLASS_ID == classID && obj.STU_ID == studentID && obj.ATT_ID != ihdnPKID
                                        select obj.ATT_ID).FirstOrDefault();

                    }

                    if (studentID != 0 && AttendanceID == 0)
                    {
                        var objAttendence = entities.tblAttendences.Where(s => s.ATT_ID == ihdnPKID).OrderByDescending(s => s.ATT_ID).FirstOrDefault();
                        if (objAttendence == null)
                        {
                            objAttendence = new tblAttendence();
                        }
                        objAttendence.STU_ID = Convert.ToInt32(studentID);
                        objAttendence.ATT_DATE = DateTime.ParseExact(txtDate.Text, "MM/dd/yyyy", null);
                        objAttendence.CLASS_ID = Convert.ToInt32(ddlClassType.SelectedValue);
                        objAttendence.ATT_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objAttendence.ATT_CREATED_DATE = DateTime.Now;
                        objAttendence.STU_ID = Convert.ToInt32(studentID);
                        objAttendence.ATT_STATUS = chkIsPresent.Checked;

                        if (objAttendence.ATT_ID == 0)
                        {
                            entities.tblAttendences.Add(objAttendence);
                        }
                        else
                        {
                            objAttendence.ATT_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objAttendence.ATT_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objAttendence).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objAttendence = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("AttendanceList.aspx");
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
                            divErrorMsg.InnerText = "Attendance for this student with class time and date already exist.";
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
            ddlClassLevel.Items.Clear();
            fillClassLevelddl();
            ddlClassType.Items.Clear();
            ddlClassType.Items.Insert(0, new ListItem("--Select Class--"));
            txtStudentNumber.Text = "";
            txtDate.Text = "";
            chkIsPresent.Checked = true;
            
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
            divMessage.InnerHtml = "";
            divMessage.InnerText = "Data resetted successfully.";
            divMessage.Visible = true;
        }

        protected void ddlClassLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillClassTypeddl();
        }
        public void fillClassTypeddl()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    var classLevelID = int.Parse(ddlClassLevel.SelectedValue);
                    var category = (from c in entities.tblClasses
                                    where c.CLASS_STATUS == true && c.CLASS_LEVEL_ID== classLevelID
                                    select new { CLASS_ID= c.CLASS_ID, Class_Name = c.CLASS_DAY + " (" + c.CLASS_TIME +") " }).ToList();

                    ddlClassType.DataValueField = "CLASS_ID";
                    ddlClassType.DataTextField = "Class_Name";

                    ddlClassType.DataSource = category;
                    ddlClassType.DataBind();
                    ddlClassType.Items.Insert(0, new ListItem("--Select Class--"));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}