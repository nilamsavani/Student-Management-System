using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class Class : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillClassLevelddl();
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
                    int iClassID = int.Parse(hdnPKId.Value);
                    var query = (from p in entities.tblClasses
                                 where p.CLASS_ID == iClassID
                                 select p).FirstOrDefault();
                    txtDay.Text = query.CLASS_DAY;
                    txtTime.Text = query.CLASS_TIME;
                    ddlClassType.SelectedValue = query.CLASS_LEVEL_ID.ToString();
                    if (query.CLASS_STATUS == true)
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
        public void fillClassLevelddl()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    var category = (from c in entities.tblClassLevels
                                    where c.CLASS_STATUS == true
                                    select new { c.CLASS_LEVEL_ID, c.CLASS_LEVEL_NAME }).ToList();

                    ddlClassType.DataValueField = "CLASS_LEVEL_ID";
                    ddlClassType.DataTextField = "CLASS_LEVEL_NAME";

                    ddlClassType.DataSource = category;
                    ddlClassType.DataBind();
                    // ddlPaymentType.Items.Add(new ListItem("Select Payment Type", "0"));
                    ddlClassType.Items.Insert(0, new ListItem("--Select Class Level--"));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void InsertClass()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    var classTypeID = int.Parse(ddlClassType.SelectedValue);
                    int ihdnPKID = int.Parse(hdnPKId.Value);
                    Int32 ClassID = 0;
                    if (ihdnPKID == 0)
                    {
                        ClassID = (from obj in entities.tblClasses
                                   where obj.CLASS_LEVEL_ID == classTypeID && obj.CLASS_DAY == txtDay.Text && obj.CLASS_TIME == txtTime.Text
                                   select obj.CLASS_ID).FirstOrDefault();
                    }
                    else
                    {
                        ClassID = (from obj in entities.tblClasses
                                   where obj.CLASS_LEVEL_ID == classTypeID && obj.CLASS_DAY == txtDay.Text && obj.CLASS_TIME == txtTime.Text && obj.CLASS_ID != ihdnPKID
                                   select obj.CLASS_ID).FirstOrDefault();

                    }

                    if (ClassID == 0)
                    {
                        var objClass = entities.tblClasses.Where(s => s.CLASS_ID == ihdnPKID).OrderByDescending(s => s.CLASS_ID).FirstOrDefault();
                        if (objClass == null)
                        {
                            objClass = new tblClass();
                        }
                        objClass.CLASS_LEVEL_ID = Convert.ToInt32(ddlClassType.SelectedValue);
                        objClass.CLASS_DAY = txtDay.Text;
                        objClass.CLASS_TIME = txtTime.Text;
                        objClass.CLASS_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objClass.CLASS_CREATED_DATE = DateTime.Now;
                        objClass.CLASS_STATUS = chkIsApprove.Checked;
                        if (objClass.CLASS_ID == 0)
                        {
                            entities.tblClasses.Add(objClass);
                        }
                        else
                        {
                            objClass.CLASS_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objClass.CLASS_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objClass).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objClass = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("ClassList.aspx");
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
            ddlClassType.SelectedIndex = 0;
            txtDay.Text = "";
            txtTime.Text = "";
            chkIsApprove.Checked = true;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
            divMessage.InnerHtml = "";
            divMessage.InnerText = "Data resetted successfully.";
            divMessage.Visible = true;
        }
        protected void btnAddClass_Click(object sender, EventArgs e)
        {
            InsertClass();
        }
    }
}