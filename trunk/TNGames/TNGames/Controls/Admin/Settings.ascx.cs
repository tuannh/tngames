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
    public partial class Settings : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadEditData();
            }
        }

        private void LoadEditData()
        {
            BizSettings biz = TNHelper.GetSettings();
            if (biz != null)
            {
                txtSmtpServer.Text = biz.SmtpServer;
                txtSmtpUsername.Text = biz.SmtpUsername;
                txtSmtpPassword.Text = biz.SmtpPassword;
                chkSmtpAuthentication.Checked = biz.SmtpAuthentication;
                txtSmtpPort.Text = biz.SmtpPort.ToString();

                txtDefaultSender.Text = biz.DefaultSender;
                txtSiteUrl.Text = biz.SiteUrl;

                txtAcitveSubject.Text = biz.ActiveEmailSubject;
                txtActiveEmailTemplate.Text = biz.ActiveEmailTemplate;

                txtResetSubject.Text = biz.ResetEmailSubject;
                txtRestBody.Text = biz.ResetEmailTemplate;

                txtDefaultPoint.Text = biz.DefaultPoint.ToString();
                txtHomeDisplayItem.Text = biz.HomeDisplayItem.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BizSettings biz = TNHelper.GetSettings();
                if (biz == null)
                    biz = new BizSettings();

                biz.SmtpServer = TextInputUtil.GetSafeInput(txtSmtpServer.Text);
                biz.SmtpUsername = TextInputUtil.GetSafeInput(txtSmtpUsername.Text);
                if (txtSmtpPassword.Text.Trim().Length > 0)
                    biz.SmtpPassword = TextInputUtil.GetSafeInput(txtSmtpPassword.Text);
                biz.SmtpAuthentication = chkSmtpAuthentication.Checked;

                int port;
                int.TryParse(txtSmtpPort.Text.Trim(), out port);
                if (port == 0)
                    port = 80;
                biz.SmtpPort = port;

                biz.DefaultSender = TextInputUtil.GetSafeInput(txtDefaultSender.Text);
                biz.SiteUrl = TextInputUtil.GetSafeInput(txtSiteUrl.Text.Trim());
                biz.ActiveEmailSubject = TextInputUtil.GetSafeInput(txtAcitveSubject.Text);
                biz.ActiveEmailTemplate = TextInputUtil.GetSafeInput(txtActiveEmailTemplate.Text);

                biz.ResetEmailSubject = TextInputUtil.GetSafeInput(txtResetSubject.Text);
                biz.ResetEmailTemplate = TextInputUtil.GetSafeInput(txtRestBody.Text);

                int point;
                int.TryParse(txtDefaultPoint.Text.Trim(), out point);
                if (point == 0)
                    point = new BizSettings().DefaultPoint;
                biz.DefaultPoint = point;


                int homeDisplayItem;
                int.TryParse(txtHomeDisplayItem.Text.Trim(), out homeDisplayItem);
                if (homeDisplayItem == 0)
                    homeDisplayItem = new BizSettings().HomeDisplayItem;
                biz.HomeDisplayItem = homeDisplayItem;

                Setting setting = DomainManager.GetObject<Setting>(1);
                if (setting == null)
                    setting = new Setting();

                setting.SettingValue = Utils.SerializeObject<BizSettings>(biz);

                if (setting.Id == 0)
                    DomainManager.Insert(setting);
                else
                    DomainManager.Update(setting);

                TNHelper.RemoveCaches();
                Utils.ShowMessage(lblMsg, "Cập nhật thông tin cấu hình thành công.");
            }
        }
    }
}