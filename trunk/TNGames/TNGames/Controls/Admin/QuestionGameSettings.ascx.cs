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
    public partial class QuestionGameSettings : System.Web.UI.UserControl
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
            IList<QuestionGame> lst = DomainManager.GetAll<QuestionGame>();
            if (lst != null)
            {
                lst = lst.Where(p => p.Active).ToList();

                ddlQG.DataSource = lst;
                ddlQG.DataValueField = "Id";
                ddlQG.DataTextField = "QuestionGameName";
                ddlQG.DataBind();

                ddlQG.Items.Insert(0, new ListItem("[Rỗng]", "0"));
            }

            BizQuestionGameSettings biz = TNHelper.GetQuestionGameSettings();
            if (biz != null)
            {
                txtTime.Text = biz.Timer.ToString();
                txtPlayNum.Text = biz.NumPlayPerDay.ToString();
                radPauseYes.Checked = biz.IsPaused;
                radPauseNo.Checked = !biz.IsPaused;

                txtMaxDisplayItem.Text = biz.MaxDisplayItem.ToString();

                ListItem item = ddlQG.Items.FindByValue(biz.QuestionGameID.ToString());
                if (item != null)
                    item.Selected = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BizQuestionGameSettings biz = new BizQuestionGameSettings();

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
                if (!string.IsNullOrEmpty(ddlQG.SelectedValue))
                    int.TryParse(ddlQG.SelectedValue, out gameid);

                biz.QuestionGameID = gameid;
                biz.IsPaused = radPauseYes.Checked;

                Setting setting = DomainManager.GetObject<Setting>(2);
                if (setting == null)
                {
                    throw new Exception("No question game settings");
                }

                setting.SettingValue = Utils.SerializeObject<BizQuestionGameSettings>(biz);

                if (setting.Id == 0)
                    DomainManager.Insert(setting);
                else
                    DomainManager.Update(setting);

                TNHelper.RemoveCaches();
                Utils.ShowMessage(lblMsg, "Cập nhập cấu hình game thử tài kiến thức thành công.");
            }
        }
    }
}