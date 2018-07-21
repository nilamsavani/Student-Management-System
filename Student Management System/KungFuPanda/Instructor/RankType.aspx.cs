using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class RankType : System.Web.UI.Page
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
                    int iRankTypeID = int.Parse(hdnPKId.Value);
                    var query = (from p in entities.tblRanks
                                 where p.RANK_ID == iRankTypeID
                                 select p).FirstOrDefault();
                    txtBeltColor.Text = query.RANK_BELT_COLOR;
                    txtRequirements.Text = query.RANK_REQUIREMENTS;
                    if (query.RANK_IS_DEFAULT == true)
                    {
                        chkIsDefault.Checked = true;
                    }
                    else {
                        chkIsDefault.Checked = false;
                    }
                    if (query.RANK_STATUS == true)
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
        protected void btnAddBeltType_Click(object sender, EventArgs e)
        {
            InsertBeltType();
        }
        public void InsertBeltType()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {

                    int ihdnPKID = int.Parse(hdnPKId.Value);
                    Int32 beltColorID = 0;
                    if (ihdnPKID == 0)
                    {
                        beltColorID = (from obj in entities.tblRanks
                                       where obj.RANK_BELT_COLOR == txtBeltColor.Text
                                       select obj.RANK_ID).FirstOrDefault();
                    }
                    else
                    {
                        beltColorID = (from obj in entities.tblRanks
                                       where obj.RANK_BELT_COLOR == txtBeltColor.Text && obj.RANK_ID != ihdnPKID
                                       select obj.RANK_ID).FirstOrDefault();

                    }

                    if (beltColorID == 0)
                    {
                        var objRank = entities.tblRanks.Where(s => s.RANK_ID == ihdnPKID).OrderByDescending(s => s.RANK_ID).FirstOrDefault();
                        if (objRank == null)
                        {
                            objRank = new tblRank();
                        }
                        objRank.RANK_BELT_COLOR = txtBeltColor.Text;
                        objRank.RANK_REQUIREMENTS = txtRequirements.Text;
                        objRank.RANK_CREATED_BY = entities.tblInstructors.AsEnumerable().FirstOrDefault().INSTRUCTOR_ID;
                        objRank.RANK_CREATED_DATE = DateTime.Now;
                        objRank.RANK_STATUS = chkIsApprove.Checked;
                        if (chkIsDefault.Checked) {
                            (from x in entities.tblRanks
                                          select x).ToList().ForEach(xx => xx.RANK_IS_DEFAULT = false);
                            entities.SaveChanges();
                        }
                        objRank.RANK_IS_DEFAULT = chkIsDefault.Checked;

                        if (objRank.RANK_ID == 0)
                        {
                            entities.tblRanks.Add(objRank);
                        }
                        else
                        {
                            objRank.RANK_MODIFY_BY = entities.tblInstructors.Select(x => x.INSTRUCTOR_ID).FirstOrDefault();
                            objRank.RANK_MODIFIED_DATE = DateTime.Now;
                            entities.Entry(objRank).State = System.Data.Entity.EntityState.Modified;
                        }
                        entities.SaveChanges();

                        objRank = null;
                        if (hdnPKId.Value != "0")
                        {
                            Response.Redirect("RankTypeList.aspx");
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

            txtBeltColor.Text = "";
            txtRequirements.Text = "";
            chkIsDefault.Checked = false;
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