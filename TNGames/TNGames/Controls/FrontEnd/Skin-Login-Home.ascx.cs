using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using System.Web.Security;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_Login_Home : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User user = Utils.GetCurrentUser();
                if (user != null)
                {
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            divLoggedIn.Visible = false;
            divLogin.Visible = false;

            if (Page.User.Identity.IsAuthenticated)
                divLoggedIn.Visible = true;
            else
                divLogin.Visible = true;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = string.Empty;
            string password = string.Empty;

            if (txtEmail != null)
                email = txtEmail.Text;

            if (txtPassword != null)
            {
                password = txtPassword.Text;
                password = Utils.EncodePassword(password);
            }

            User user = TNHelper.GetUserByEmail(email);
            if (user == null)
            {
                Utils.ShowMessage(lblMsg, "Địa chỉ email đăng nhập không tồn tại.");
            }
            else if (user != null && !user.Active)
            {
                Utils.ShowMessage(lblMsg, "Tài khoản của bạn chưa được kích hoạt. Liện hệ với admin để được hỗ trợ.");
            }
            else
            {
                if (string.Compare(user.Password, password, false) == 0)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, chkSave.Checked);
                    Session[TNHelper.LoginKey] = user;
                    TNHelper.LogAction(LogType.UserLog, "Đăng nhập thành công");

                    string returnUrl = Page.Request.QueryString["returnurl"];
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        returnUrl = Page.Server.UrlDecode(returnUrl);
                        Page.Response.Redirect(returnUrl, true);
                    }
                    else
                    {
                        if (user.IsAdmin)
                            Page.Response.Redirect("/admincp", true);
                        else
                            Page.Response.Redirect("/", true);
                    }
                }
                else
                {
                    Utils.ShowMessage(lblMsg, "Mật khẩu đăng nhập chưa đúng.");
                }
            }
        }        
    }
}
