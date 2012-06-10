using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using TNGames.Core;

namespace TNGames.Controls.FrontEnd
{
    public partial class ForgotPassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            User user = TNHelper.GetUserByEmail(email);
            if (user == null)
            {
                Utils.ShowMessage(lblMsg, "Địa chỉ email không tồn tại.");
            }
            else if (user != null && !user.Active)
            {
                Utils.ShowMessage(lblMsg, "Tài khoản của bạn chưa được kích hoạt. Hãy kích hoạt tài của bạn trước khi gởi yêu cầu mật khẩu mới.");
            }
            else
            {
                user = DomainManager.GetObject<User>(user.Id);
                if (user != null)
                {
                    string newpass = Utils.GetNewPassword();
                    string oldpass = user.Password;
                    user.Password = newpass;

                    string from = TNHelper.GetSettings().DefaultSender;
                    string to = user.Email;
                    string subject = TNHelper.GetSettings().ResetEmailSubject;
                    subject = Utils.ResolveMessage(subject, user);

                    string content = TNHelper.GetSettings().ResetEmailTemplate;
                    content = Utils.ResolveMessage(content, user);

                    if (Utils.SendEmail(from, to, subject, content))
                    {
                        pnlReset.Visible = false;
                        user.Password = Utils.EncodePassword(newpass);
                        DomainManager.Update(user);
                        Utils.ResetCurrentUser();
                        Utils.ShowMessage(lblMsg, string.Format("Mật khẩu mới đã được gởi vào địa chỉ email <b>{0}</b>", email));
                    }
                    else
                    {
                        user.Password = oldpass;
                        DomainManager.Update(user);
                        Utils.ResetCurrentUser();
                        Utils.ShowMessage(lblMsg, "Gởi mật khẩu không thành công. Bạn hãy thử lại sau.");
                    }
                }
            }
        }
    }
}