using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using TNGames.Core;

namespace TNGames.Controls.Admin
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtOldPassword.Text.Length == 0)
            {
                Utils.ShowMessage(lblMsgPassword, "Bạn chưa nhập mật khẩu cũ");
                return;
            }

            if (string.Compare(txtPassword.Text, txtRetypePass.Text, false) != 0)
            {
                Utils.ShowMessage(lblMsgPassword, "Mật khẩu mới không trùng khớp");
                return;
            }

            User user = Utils.GetCurrentUser();
            if (user != null)
            {
                string oldPass = Utils.EncodePassword(txtOldPassword.Text);
                string newPass = Utils.EncodePassword(txtPassword.Text);
                if (string.Compare(oldPass, user.Password, false) == 0)
                {
                    user = DomainManager.GetObject<User>(user.Id);
                    if (user != null)
                    {
                        user.Password = newPass;
                        DomainManager.Update(user);
                        Utils.ResetCurrentUser();
                        Utils.ShowMessage(lblMsgPassword, "Đổi mật khẩu thành công");
                    }
                }
                else
                {
                    Utils.ShowMessage(lblMsgPassword, "Mật khẩu cũ chưa đúng");
                }
            }
        }
    }
}