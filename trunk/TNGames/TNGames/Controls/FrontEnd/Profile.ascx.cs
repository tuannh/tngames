using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core;
using TNGames.Core.Helper;

namespace TNGames.Controls.FrontEnd
{

    public partial class Profile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefaultData();
                LoadUserData();
            }
        }

        private void LoadUserData()
        {
            User user = Utils.GetCurrentUser();
            if (user != null)
            {
                txtFullName.Text = user.FullName;
                txtDisplayName.Text = user.DisplayName;
                txtPhone.Text = user.Phone;
                txtEmail.Text = user.Email;
                txtEmail.Enabled = false;

                txtIDNumber.Text = user.IDNumber;
                if (!string.IsNullOrEmpty(user.IDNumber))
                    txtIDNumber.Enabled = false;

                if (user.Birthday.HasValue)
                {
                    ddlDay.SelectedValue = user.Birthday.Value.Day.ToString();
                    ddlMonth.SelectedValue = user.Birthday.Value.Month.ToString();
                    txtYear.Text = user.Birthday.Value.Year.ToString();

                    ddlDay.Enabled = ddlMonth.Enabled = false;
                    txtYear.Enabled = false;
                }

                txtAddress.Text = user.Address;
                ListItem item = ddlProvince.Items.FindByValue(user.Province);
                if (item != null)
                    item.Selected = true;

                litPoint.Text = user.TotalPoint.ToString("N0");
            }
        }

        private void LoadDefaultData()
        {
            for (int i = 1; i <= 31; i++)
                ddlDay.Items.Add(i.ToString());

            for (int i = 1; i <= 12; i++)
                ddlMonth.Items.Add(i.ToString());

            ddlDay.Items.Insert(0, "dd");
            ddlMonth.Items.Insert(0, "mm");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strDate = string.Format("{0}/{1}/{2}", Convert.ToInt32(ddlDay.SelectedValue).ToString("00"), Convert.ToInt32(ddlMonth.SelectedValue).ToString("00"), txtYear.Text);
            DateTime? birthday = Utils.GetDate(strDate);
            if (!birthday.HasValue)
            {
                Utils.ShowMessage(lblMsg, "Ngày sinh không hợp lệ. Hãy kiểm tra lại.");
                return;
            }

            User user = Utils.GetCurrentUser();
            if (Page.IsValid && user != null)
            {
                user = DomainManager.GetObject<User>(user.Id);
                if (user != null)
                {
                    user.FullName = TextInputUtil.GetSafeInput(txtFullName.Text.Trim());
                    user.DisplayName = TextInputUtil.GetSafeInput(txtDisplayName.Text.Trim());
                    user.Phone = TextInputUtil.GetSafeInput(txtPhone.Text);

                    if (string.IsNullOrEmpty(user.IDNumber))
                    {
                        if (txtIDNumber.Text.Trim().Length > 0)
                        {
                            bool valid = TNHelper.IsValidIdNumber(txtIDNumber.Text.Trim());
                            if (valid)
                                user.IDNumber = TextInputUtil.GetSafeInput(txtIDNumber.Text.Trim());
                            else
                            {
                                Utils.ShowMessage(lblMsg, string.Format("CMND \"{0}\" đã được đăng ký. Bạn hãy kiểm tra lại.", txtIDNumber.Text.Trim()));
                                return;
                            }
                        }
                    }

                    if (!user.Birthday.HasValue)
                    {
                        user.Birthday = birthday;
                    }

                    user.Address = TextInputUtil.GetSafeInput(txtAddress.Text);
                    user.Province = ddlProvince.SelectedValue;

                    if (user.Id > 0)
                    {
                        DomainManager.Update(user);
                        Utils.ResetCurrentUser();
                        LoadUserData();
                        TNHelper.LogAction(LogType.UserLog, "Cập nhật thông tin tài khoản");

                        Utils.ShowMessage(lblMsg, "Thông tin tài khoản của bạn được cập nhật thành công");
                    }
                }
            }
        }

        protected void btnResetAccount_Click(object sender, EventArgs e)
        {
            User user = Utils.GetCurrentUser();
            if (user != null && TNHelper.ResetUser(user.Id))
            {
                TNHelper.LogAction(LogType.UserLog, "Khởi tạo thông tin tài khoản");
                litPoint.Text = TNHelper.GetSettings().DefaultPoint.ToString("N0");
                Utils.ShowMessage(lblMsgPoint, "Tài khoản của bạn đã được khởi tạo lại thành công.");
            }
            else
            {
                Utils.ShowMessage(lblMsgPoint, "Khởi tạo tài khoản không thành công.");
            }
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
                        TNHelper.LogAction(LogType.UserLog, "Đổi mật khẩu thành công");
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