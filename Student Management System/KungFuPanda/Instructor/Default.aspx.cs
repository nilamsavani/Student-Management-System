using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KungFuPanda.Instructor
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
            }
        }

        protected void lnkSignIn_Click(object sender, EventArgs e)
        {
            CheckLogin();
        }
        public void CheckLogin()
        {
            try
            {
                using (KungFuEntities2 entities = new KungFuEntities2())
                {
                    var InstructorID = (from obj in entities.tblInstructors
                                        where obj.INSTRUCTOR_NAME == txtUserName.Text && obj.INSTRUCTOR_PASSWORD==txtPassword.Text
                                        select obj.INSTRUCTOR_ID).FirstOrDefault();
                    if (InstructorID != 0)
                    {
                        Response.Redirect("StudentReport.aspx");
                    }
                    else
                    {

                        divErrorMsg.Visible = true;
                        return;
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}