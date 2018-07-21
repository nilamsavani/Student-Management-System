using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class ClassType : System.Web.UI.Page
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
                    int iClassLevelID = int.Parse(hdnPKId.Value);
                    var query = (from c in entities.tblClassLevels
                                 where c.CLASS_LEVEL_ID == iClassLevelID
                                 select c).FirstOrDefault();
                    txtClassLevel.Text = query.CLASS_LEVEL_NAME;
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
        public void InsertClassType()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    int ihdnPKID = int.Parse(hdnPKId.Value);
                    Int32 ClassTypeID = 0;
                    if (ihdnPKID == 0)
                    {
                        ClassTypeID = (from obj in entities.tblClassLevels
                                         where obj.CLASS_LEVEL_NAME == txtClassLevel.Text
                                         select obj.CLASS_LEVEL_ID).FirstOrDefault();
                    }
                    else
                    {
                        ClassTypeID = (from obj in entities.tblClassLevels
                                       where (obj.CLASS_LEVEL_NAME == txtClassLevel.Text && obj.CLASS_LEVEL_ID != ihdnPKID)
                                         select obj.CLASS_LEVEL_ID).FirstOrDefault();
                    }

                    if (ClassTypeID == 0)
                    {

                        var objClassLevel = entities.tblClassLevels.Where(s => s.CLASS_LEVEL_ID == ihdnPKID).OrderByDescending(s => s.CLASS_LEVEL_ID).FirstOrDefault();
                        if (objClassLevel == null)
                        {
                            objClassLevel = new tblClassLevel();
                        }
                        objClassLevel.CLASS_LEVEL_NAME = txtClassLevel.Text;
                        objClassLevel.CLASS_LEVEL_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objClassLevel.CLASS_LEVEL_CREATED_DATE = DateTime.Now;
                        objClassLevel.CLASS_STATUS = chkIsApprove.Checked;

                        if (objClassLevel.CLASS_LEVEL_ID == 0)
                        {
                            entities.tblClassLevels.Add(objClassLevel);
                        }
                        else
                        {
                            objClassLevel.CLASS_LEVEL_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objClassLevel.CLASS_LEVEL_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objClassLevel).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objClassLevel = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("ClassTypeList.aspx");
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
            txtClassLevel.Text = "";
            chkIsApprove.Checked = true;

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
            divMessage.InnerHtml = "";
            divMessage.InnerText = "Data resetted successfully.";
            divMessage.Visible = true;
        }

        protected void btnAddClassType_Click(object sender, EventArgs e)
        {
            InsertClassType();
        }
    }
}