using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core;
using TNGames.Core.Helper;
using TNGames.Core.Domain;

namespace TNGames.Controls.Admin
{
    public partial class BettingGameSettings : System.Web.UI.UserControl
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
            BizBettingGameSettings biz = TNHelper.GetBettingGameSettings();
            if (biz != null)
            {
                radPauseYes.Checked = biz.IsPaused;
                radPauseNo.Checked = !biz.IsPaused;

                radAllowDeleteYes.Checked = biz.AllowDelete;
                radAllowDeleteNo.Checked = !biz.AllowDelete;

                txtMaxDisplayItem.Text = biz.MaxDisplayItem.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BizBettingGameSettings biz = new BizBettingGameSettings();

                biz.IsPaused = radPauseYes.Checked;
                biz.AllowDelete = radAllowDeleteYes.Checked;

                int maxDisplayItem = 0;
                int.TryParse(txtMaxDisplayItem.Text.Trim(), out maxDisplayItem);
                biz.MaxDisplayItem = maxDisplayItem;        

                Setting setting = DomainManager.GetObject<Setting>(4);

                if (setting == null)
                    setting = new Setting();

                setting.SettingValue = Utils.SerializeObject<BizBettingGameSettings>(biz);

                if (setting.Id == 0)
                    DomainManager.Insert(setting);
                else
                    DomainManager.Update(setting);

                TNHelper.RemoveCaches();
                Utils.ShowMessage(lblMsg, "Cập nhập cấu hình game thử tài phần tích thành công.");
            }
        }
    }
}