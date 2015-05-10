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
    public partial class Login : System.Web.UI.UserControl
    {
        protected Label lblMsg;
        protected TextBox txtEmail;
        protected TextBox txtPassword;
        protected Button btnLogin;

        protected Literal litDisplayName;
        protected Literal litPoint;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User user = Utils.GetCurrentUser();
                if (user != null)
                {
                    if (litDisplayName != null)
                        litDisplayName.Text = user.DisplayName ?? user.FullName;

                    if (litPoint != null)
                        litPoint.Text = user.Point.ToString("N0");
                }
            }
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
                    FormsAuthentication.SetAuthCookie(user.Email, false);
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

        protected override void OnInit(EventArgs e)
        {
            string logout = Page.Request.QueryString["logout"];
            if (string.Compare(logout, "true", true) == 0)
            {
                Utils.ResetCurrentUser();
                FormsAuthentication.SignOut();
                Page.Response.Redirect("~/", true);
            }

            base.OnInit(e);

            txtEmail = loginView.FindControl("txtEmail") as TextBox;
            txtPassword = loginView.FindControl("txtPassword") as TextBox;
            btnLogin = loginView.FindControl("btnLogin") as Button;
            lblMsg = loginView.FindControl("lblMsg") as Label;

            litDisplayName = loginView.FindControl("litDisplayName") as Literal;
            litPoint = loginView.FindControl("litPoint") as Literal;
        }
    }
}
