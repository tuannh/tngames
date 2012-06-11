using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core;
using TNGames.Core.Helper;
using System.Drawing;

namespace TNGames.Controls.FrontEnd
{
    public partial class Register : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefaultData();
            }

        }

        private void LoadDefaultData()
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            #region Valid data

            string captcha = Session[TNHelper.CaptchaKey] as string;
            if (string.Compare(txtCaptcha.Text, captcha, false) != 0)
            {
                Utils.ShowMessage(lblMsg, "Mã captcha chưa đúng. Bạn hãy kiểm tra lại.");
                return;
            }

            string email = TextInputUtil.GetSafeInput(txtEmail.Text.Trim());
            if (!TNHelper.IsValidRegisterEmail(email))
            {
                Utils.ShowMessage(lblMsg, string.Format("Địa chỉ email <b>{0}</b> đã được sử dụng.", email));
                return;
            }

            if (string.Compare(txtPassword.Text, txtRetypePass.Text, false) != 0)
            {
                Utils.ShowMessage(lblMsg, "Mật khẩu chưa trùng khớp. Hãy kiểm tra lại");
                return;
            }

            #endregion

            if (Page.IsValid)
            {
                User user = new User();
                user.DisplayName = TextInputUtil.GetSafeInput(txtDisplayName.Text);
                user.FullName = TextInputUtil.GetSafeInput(txtFullName.Text);
                user.Email = email;
                user.Password = Utils.EncodePassword(txtPassword.Text);
                user.RegisterDate = DateTime.Now;
                string code = Utils.GenerateNewActiveCode();
                user.ActiveCode = code;
                user.Point = TNHelper.GetSettings().DefaultPoint;

                if (DomainManager.Insert(user))
                {
                    bool isSent = SendActiveEmail(user, code);
                    if (isSent)
                        Utils.ShowMessage(lblMsg, string.Format("Đăng ký tài khoản thành công. Email kích hoạt đã đựoc gởi tới địa chỉ email <b>{0}</b> của bạn.", user.Email));
                    else
                    {
                        Utils.ShowMessage(lblMsg, string.Format("Gởi email kích hoạt không thành công. Bạn liên hệ admin để kích hoạt tài khoản.", user.Email));
                        lblMsg.ForeColor = Color.Red;
                    }

                    TNHelper.LogAction(user, LogType.UserLog, "Đăng ký tài khoản");
                    pnlRegister.Visible = false;
                }
                else
                {
                    Utils.ShowMessage(lblMsg, "Đăng ký tài khoản không thành công. Bạn hãy thử lại");
                }
            }
        }

        private bool SendActiveEmail(User user, string code)
        {
            string subject = TNHelper.GetSettings().ActiveEmailSubject;
            string body = TNHelper.GetSettings().ActiveEmailTemplate;
            string from = TNHelper.GetSettings().DefaultSender;
            string to = user.Email;

            subject = Utils.ResolveMessage(subject, user);
            body = Utils.ResolveMessage(body, user);

            return Utils.SendEmail(from, to, subject, body);
        }


    }
}