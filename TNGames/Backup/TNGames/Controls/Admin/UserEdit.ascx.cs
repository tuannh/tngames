using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core;
using TNGames.Core.Helper;

namespace TNGames.Controls.Admin
{
    public partial class UserEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefaultData();
                LoadEditData();
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


        private void LoadEditData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            User obj = DomainManager.GetObject<User>(id);
            if (obj != null)
            {
                if (!obj.IsAdmin)
                {
                    txtDisplayName.Text = obj.DisplayName;
                    txtFullName.Text = obj.FullName;
                    txtEmail.Text = obj.Email;
                    txtAddress.Text = obj.Address;
                    txtIDNumber.Text = obj.IDNumber;
                    txtPhone.Text = obj.Phone;
                    txtPoint.Text = obj.Point.ToString("N0");
                    txtPoint.Enabled = false;

                    radYes.Checked = obj.Active;
                    radNo.Checked = !obj.Active;

                    radIsAdmin.Checked = obj.IsAdmin;
                    radIsUser.Checked = !obj.IsAdmin;

                    ListItem item = ddlProvince.Items.FindByValue(obj.Province);
                    if (item != null)
                        item.Selected = true;

                    if (obj.Birthday.HasValue)
                    {
                        ddlDay.SelectedValue = obj.Birthday.Value.Day.ToString();
                        ddlMonth.SelectedValue = obj.Birthday.Value.Month.ToString();
                        txtYear.Text = obj.Birthday.Value.Year.ToString();
                    }

                    btnDelete.Visible = true;
                }
                else
                {
                    Utils.ShowMessage(lblMsg, "Không thể chỉnh sửa thông tin admin.");
                }
            }
            else
            {
                btnDelete.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            #region Valid data

            string email = TextInputUtil.GetSafeInput(txtEmail.Text);
            User user = TNHelper.GetUserByEmail(email);
            if (user != null && user.Id != id)
            {
                Utils.ShowMessage(lblMsg, string.Format("Địa chỉ email <b>{0}</b> đã được sử dụng bởi <b>{1}</b>", email, user.FullName));
                return;
            }

            if (string.Compare(txtPassword.Text, txtRetypePass.Text, false) != 0)
            {
                Utils.ShowMessage(lblMsg, "Mật khẩu chưa trùng khớp. Hãy kiểm tra lại");
                return;
            }

            DateTime? birthday = null;
            if (!string.IsNullOrEmpty(ddlDay.SelectedValue) && !string.IsNullOrEmpty(ddlMonth.SelectedValue) && !string.IsNullOrEmpty(txtYear.Text))
            {
                string strDate = string.Format("{0}/{1}/{2}", Convert.ToInt32(ddlDay.SelectedValue).ToString("00"), Convert.ToInt32(ddlMonth.SelectedValue).ToString("00"), txtYear.Text);
                birthday = Utils.GetDate(strDate);
                if (!birthday.HasValue)
                {
                    Utils.ShowMessage(lblMsg, "Ngày sinh không hợp lệ. Hãy kiểm tra lại.");
                    return;
                }
            }

            #endregion

            if (Page.IsValid)
            {
                User obj = DomainManager.GetObject<User>(id);
                if (obj == null)
                    obj = new User();

                obj.DisplayName = TextInputUtil.GetSafeInput(txtDisplayName.Text.Trim());
                obj.FullName = TextInputUtil.GetSafeInput(txtFullName.Text.Trim());
                obj.Email = TextInputUtil.GetSafeInput(txtEmail.Text.Trim());
                obj.Address = TextInputUtil.GetSafeInput(txtAddress.Text.Trim());
                obj.Province = TextInputUtil.GetSafeInput(ddlProvince.SelectedValue);
                obj.Phone = TextInputUtil.GetSafeInput(txtPoint.Text);
                obj.IDNumber = TextInputUtil.GetSafeInput(txtIDNumber.Text);
                obj.Active = radYes.Checked;
                obj.Birthday = birthday;
                obj.IsAdmin = radIsAdmin.Checked;

                if (obj.Active)
                    obj.ActiveCode = string.Empty;

                if (!string.IsNullOrEmpty(txtPassword.Text))
                { 
                    string pass  = TextInputUtil.GetSafeInput(txtPassword.Text.Trim());
                    obj.Password = Utils.EncodePassword(pass);
                }

                if (obj.Id > 0)
                {
                    DomainManager.Update(obj);
                }
                else
                {
                    obj.RegisterDate = DateTime.Now;
                    obj.ActiveCode = string.Empty;
                    DomainManager.Insert(obj);
                }

                Page.Response.Redirect("/admincp/user-list");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            User obj = DomainManager.GetObject<User>(id);
            if (obj != null)
            {
                DomainManager.Delete(obj);
                Page.Response.Redirect("/admincp/user-list");
            }
        }
    }
}