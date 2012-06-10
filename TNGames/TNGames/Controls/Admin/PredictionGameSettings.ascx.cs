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
    public partial class PredictionGameSettings : System.Web.UI.UserControl
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
            IList<PredictionGame> lst = DomainManager.GetAll<PredictionGame>();
            if (lst != null)
            {
                lst = lst.Where(p => p.Active && p.IsCalculate == false).ToList();
                ddlPG.DataSource = lst;
                ddlPG.DataValueField = "Id";
                ddlPG.DataTextField = "PredictionGameName";
                ddlPG.DataBind();

                ddlPG.Items.Insert(0, new ListItem("[Rỗng]", "0"));
            }

            BizPredictionGameSettings biz = TNHelper.GetPredictionGameSettings();
            if (biz != null)
            {
                txtTime.Text = biz.Timer.ToString();
                txtPlayNum.Text = biz.NumPlayPerDay.ToString();
                radPauseYes.Checked = biz.IsPaused;
                radPauseNo.Checked = !biz.IsPaused;

                txtMaxDisplayItem.Text = biz.MaxDisplayItem.ToString();

                ListItem item = ddlPG.Items.FindByValue(biz.PredictionGameID.ToString());
                if (item != null)
                    item.Selected = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BizPredictionGameSettings biz = new BizPredictionGameSettings();

                int playNum;
                int.TryParse(txtPlayNum.Text.Trim(), out playNum);
                biz.NumPlayPerDay = playNum;

                int time;
                int.TryParse(txtTime.Text.Trim(), out time);
                biz.Timer = time;

                int maxDisplayItem;
                int.TryParse(txtMaxDisplayItem.Text.Trim(), out maxDisplayItem);
                biz.MaxDisplayItem = maxDisplayItem;

                int gameid = 0;
                if (!string.IsNullOrEmpty(ddlPG.SelectedValue))
                    int.TryParse(ddlPG.SelectedValue, out gameid);

                biz.PredictionGameID = gameid;
                biz.IsPaused = radPauseYes.Checked;

                Setting setting = DomainManager.GetObject<Setting>(3);
                if (setting == null)
                {
                    setting = new Setting();
                }

                setting.SettingValue = Utils.SerializeObject<BizPredictionGameSettings>(biz);

                if (setting.Id == 0)
                    DomainManager.Insert(setting);
                else
                    DomainManager.Update(setting);

                TNHelper.RemoveCaches();
                Utils.ShowMessage(lblMsg, "Cập nhập cấu hình game thử tài dự đoán thành công.");
            }
        }
    }
}