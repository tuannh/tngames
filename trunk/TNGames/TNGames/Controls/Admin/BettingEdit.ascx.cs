using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core;
using System.Globalization;
using TNGames.Core.Helper;

namespace TNGames.Controls.Admin
{
    public partial class BettingEdit : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool scoreupdate = false;
                if (Page.Request.QueryString["scoreupdate"] != null)
                    bool.TryParse(Page.Request.QueryString["scoreupdate"], out scoreupdate);

                if (!scoreupdate)
                {
                    pnlInfo.Visible = true;
                    pnlScore.Visible = false;
                    LoadEditData();
                }
                else
                {
                    pnlInfo.Visible = false;
                    pnlScore.Visible = true;
                    LoadEditScoreData();
                }
            }
        }

        protected void LoadEditScoreData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            Betting obj = DomainManager.GetObject<Betting>(id);
            if (obj != null)
            {
                btnSaveScore.Enabled = !obj.IsCalculate;

                litHome.Text = obj.HomeTeam;
                litVisiting.Text = obj.VisitingTeam;

                if (obj.IsUpdateScore)
                {
                    txtHomeScore.Text = obj.HomeGoalScore.ToString();
                    txtVisitngScore.Text = obj.VisitingGoalScore.ToString();
                }
            }
        }

        protected void LoadEditData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            Betting obj = DomainManager.GetObject<Betting>(id);
            if (obj != null)
            {
                btnSave.Enabled = !(obj.BettingUserses.Count > 0);
                btnDelete.Enabled = !(obj.BettingUserses.Count > 0);

                BizBettingGameSettings setting = TNHelper.GetBettingGameSettings();
                if (setting.AllowDelete)
                    btnDelete.Enabled = true;

                txtName.Text = obj.BettingName;
                txtDesc.Text = obj.Description;
                txtHomeTeam.Text = obj.HomeTeam;
                txtVisitingTeam.Text = obj.VisitingTeam;

                txtHomeGoalScore.Text = obj.HomeGoalScore.ToString();
                txtHomeGoalScore.Visible = obj.IsUpdateScore;
                txtVisitingGoalScore.Text = obj.VisitingGoalScore.ToString();
                txtVisitingGoalScore.Visible = obj.IsUpdateScore;

                if (obj.PlayDate.HasValue)
                {
                    txtPlayDate.Text = obj.PlayDate.Value.ToString(TNHelper.DateFormat);
                    txtPlayTime.Text = obj.PlayDate.Value.ToString(TNHelper.TimeFormat);
                }

                if (obj.StartDate.HasValue)
                    txtStartDate.Text = obj.StartDate.Value.ToString(TNHelper.DateFormat);

                if (obj.EndDate.HasValue)
                    txtEndDate.Text = obj.EndDate.Value.ToString(TNHelper.DateFormat);

                radYes.Checked = obj.Active;
                radNo.Checked = !obj.Active;

                if (obj.IsUpdateScore) // đã tính điểm thưởng
                    trScore.Visible = obj.IsUpdateScore;

                if (obj.IsUpdateScore)
                    pnlAdd.Visible = false;

                // load rate list
                rptRate.DataSource = obj.BettingRateses;
                rptRate.DataBind();
            }
            else // add new
            {
                btnAdd_Click(null, null);
            }
            btnDelete.Visible = (id > 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Valid data

            if (txtName.Text.Trim().Length == 0)
            {
                Utils.ShowMessage(lblMsg, "Tên dự đoán không thể rỗng");
                return;
            }

            if (txtHomeTeam.Text.Trim().Length == 0)
            {
                Utils.ShowMessage(lblMsg, "Tên đội A không thể rỗng");
                return;
            }

            if (txtVisitingTeam.Text.Trim().Length == 0)
            {
                Utils.ShowMessage(lblMsg, "Tên đội B không thể rỗng");
                return;
            }

            List<BettingRate> lst = GetRateList();
            if (lst == null || (lst != null && lst.Count == 0))
            {
                Utils.ShowMessage(lblMsg, "Bạn chưa nhập tỷ lệ");
                return;
            }

            if (lst != null)
            {
                BettingRate rate = lst[0];
                if (rate.HomeRate == 0 && rate.VisitingRate == 0)
                {
                    Utils.ShowMessage(lblMsg, "Bạn chưa nhập tỷ lệ");
                    return;
                }
            }

            DateTime? playDate = GetDate(txtPlayDate.Text.Trim(), txtPlayTime.Text.Trim());
            if (txtPlayDate.Text.Trim().Length > 0 && !playDate.HasValue)
            {
                Utils.ShowMessage(lblMsg, "Ngày thi đấu không hợp lệ");
                return;
            }

            DateTime? startDate = GetDate(txtStartDate.Text.Trim());
            if (txtStartDate.Text.Trim().Length > 0 && !startDate.HasValue)
            {
                Utils.ShowMessage(lblMsg, "Ngày bắt đầu không hợp lệ");
                return;
            }

            DateTime? endDate = GetDate(txtEndDate.Text.Trim());
            if (txtEndDate.Text.Trim().Length > 0 && !endDate.HasValue)
            {
                Utils.ShowMessage(lblMsg, "Ngày kết thúc không hợp lệ");
                return;
            }

            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                Utils.ShowMessage(lblMsg, "Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                return;
            }

            #endregion

            if (Page.IsValid)
            {
                string strId = Page.RouteData.Values["id"] as string;
                int id = 0;
                int.TryParse(strId, out id);


                Betting obj = DomainManager.GetObject<Betting>(id);
                if (obj == null)
                    obj = new Betting();

                obj.BettingName = TextInputUtil.GetSafeInput(txtName.Text);
                obj.Description = TextInputUtil.GetSafeInput(txtDesc.Text);
                obj.HomeTeam = TextInputUtil.GetSafeInput(txtHomeTeam.Text);
                obj.VisitingTeam = TextInputUtil.GetSafeInput(txtVisitingTeam.Text);

                int homescore = 0;
                int.TryParse(txtHomeGoalScore.Text.Trim(), out homescore);
                obj.HomeGoalScore = homescore;

                int visitingscore = 0;
                int.TryParse(txtVisitingGoalScore.Text.Trim(), out visitingscore);
                obj.VisitingGoalScore = visitingscore;

                obj.PlayDate = playDate;
                obj.StartDate = startDate;

                if (endDate.HasValue)
                    endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);
                obj.EndDate = endDate;

                obj.Active = radYes.Checked;

                #region betting rate

                obj.BettingRateses.Clear();
                foreach (BettingRate br in lst)
                {
                    BettingRate rate = new BettingRate();
                    rate.HomeRate = br.HomeRate;
                    rate.VisitingRate = br.VisitingRate;
                    rate.Order = br.Order;
                    rate.Betting = obj;
                    obj.BettingRateses.Add(rate);
                }

                #endregion

                string msg = string.Empty;
                if (obj.Id > 0)
                {
                    obj.ModifiedDate = DateTime.Now;
                    DomainManager.Update(obj);
                    msg = Page.Server.UrlEncode("Cập nhật thông tin thành công");
                }
                else
                {
                    obj.CreatedDate = DateTime.Now;
                    DomainManager.Insert(obj);
                    msg = Page.Server.UrlEncode("Thêm thông tin thành công");
                }

                Page.Response.Redirect(string.Format("/admincp/betting-list?msg={0}", msg), true);
            }
        }

        protected void btnSaveScore_Click(object sender, EventArgs e)
        {

            #region Valid data

            if (txtHomeScore.Text.Trim().Length == 0)
            {
                Utils.ShowMessage(lblMsg, "Bạn chưa nhập số bàn thắng đội nhà");
                return;
            }

            if (txtVisitngScore.Text.Trim().Length == 0)
            {
                Utils.ShowMessage(lblMsg, "Bạn chưa nhập số bàn thắng đội khách");
                return;
            }

            #endregion

            if (Page.IsValid)
            {
                string strId = Page.RouteData.Values["id"] as string;
                int id = 0;
                int.TryParse(strId, out id);

                Betting obj = DomainManager.GetObject<Betting>(id);
                if (obj == null)
                    obj = new Betting();

                int homeScore = 0;
                int.TryParse(txtHomeScore.Text, out homeScore);

                int visitingScore = 0;
                int.TryParse(txtVisitngScore.Text, out visitingScore);

                obj.IsUpdateScore = true;
                obj.HomeGoalScore = homeScore;
                obj.VisitingGoalScore = visitingScore;

                string msg = string.Empty;
                if (obj.Id > 0)
                {
                    obj.ModifiedDate = DateTime.Now;
                    DomainManager.Update(obj);

                    // TNHelper.UpdateBettingResult(obj.Id);
                    msg = Page.Server.UrlEncode("Cập nhập tỷ số và điểm của người chơi thành công.");
                }
                else
                {
                    msg = Page.Server.UrlEncode("Cập nhật dữ liệu không thành công.");
                }

                Page.Response.Redirect(string.Format("/admincp/betting-list?msg={0}", msg), true);
            }
        }

        protected void btnCancelScore_Click(object sender, EventArgs e)
        {

        }

        private DateTime? GetDate(string datevalue, string time)
        {
            try
            {
                string value = string.Format("{0} {1}", datevalue, time);
                IFormatProvider format = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
                DateTime? date = !string.IsNullOrEmpty(value) ? new DateTime?(
                                        DateTime.ParseExact(value, TNHelper.DateTimeFormat, format)) : null;
                return date;
            }
            catch
            {
                return null;
            }
        }

        private DateTime? GetDate(string value)
        {
            try
            {
                IFormatProvider format = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
                DateTime? date = !string.IsNullOrEmpty(value) ? new DateTime?(
                                        DateTime.ParseExact(value, TNHelper.DateFormat, format)) : null;
                return date;
            }
            catch
            {
                return null;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            Betting obj = DomainManager.GetObject<Betting>(id);
            if (obj != null)
            {
                DomainManager.Delete(obj);
                string msg = Page.Server.UrlEncode("Xóa trận đấu thành công");
                Page.Response.Redirect(string.Format("/admincp/betting-list?msg={0}", msg), true);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BettingRate rate;
            float homeRate;
            float visitingRate;
            List<BettingRate> lst = GetRateList();

            rate = new BettingRate();
            homeRate = 0;
            if (ddlHome.SelectedValue != null)
                float.TryParse(ddlHome.SelectedValue, out homeRate);

            float homeRateN = 0;
            if (ddlHomeN.SelectedValue != null)
                float.TryParse(ddlHomeN.SelectedValue, out homeRateN);

            visitingRate = 0;
            if (ddlVisiting.SelectedValue != null)
                float.TryParse(ddlVisiting.SelectedValue, out visitingRate);

            float visitingRateN = 0;
            if (ddlVisitingN.SelectedValue != null)
                float.TryParse(ddlVisitingN.SelectedValue, out visitingRateN);

            int order = 0;
            if (txtOrder.Text.Trim().Length > 0)
                int.TryParse(txtOrder.Text.Trim(), out order);

            if (lst != null && lst.Count > 0)
            {
                foreach (BettingRate r in lst)
                {
                    if (r.HomeRate == homeRate && r.VisitingRate == visitingRate)
                    {
                        Utils.ShowMessage(lblMsg, string.Format("Tỷ lệ {0}:{1} đã tồn tại trong danh sách. Bạn hãy kiểm tra lại", ddlHome.SelectedItem.Text, ddlVisiting.SelectedItem.Text));
                        return;
                    }
                }
            }

            rate.HomeRate = homeRate + homeRateN;
            rate.VisitingRate = visitingRate + visitingRateN;
            rate.Order = order;
            lst.Add(rate);

            rptRate.DataSource = lst;
            rptRate.DataBind();

            // reset selected
            ddlHome.SelectedValue = null;
            ddlHomeN.SelectedValue = null;
            ddlVisiting.SelectedValue = null;
            ddlVisitingN.SelectedValue = null;
        }

        private List<BettingRate> GetRateList()
        {
            List<BettingRate> lst = new List<BettingRate>();
            BettingRate rate = new BettingRate();
            float homeRate = 0;
            float homeRateN = 0;
            float visitingRate = 0;
            float visitingRateN = 0;

            if (rptRate.Items.Count > 0)
            {
                foreach (RepeaterItem item in rptRate.Items)
                {
                    rate = new BettingRate();
                    DropDownList tmpDdlHome = item.FindControl("ddlHome") as DropDownList;
                    DropDownList tmpDdlHomeN = item.FindControl("ddlHomeN") as DropDownList;
                    DropDownList tmpDddlVisiting = item.FindControl("ddlVisiting") as DropDownList;
                    DropDownList tmpDddlVisitingN = item.FindControl("ddlVisitingN") as DropDownList;
                    TextBox tmpTxtOrder = item.FindControl("txtOrder") as TextBox;

                    homeRate = 0;
                    if (tmpDdlHome.SelectedValue != null)
                        float.TryParse(tmpDdlHome.SelectedValue, out homeRate);

                    homeRateN = 0;
                    if (tmpDdlHomeN.SelectedValue != null)
                        float.TryParse(tmpDdlHomeN.SelectedValue, out homeRateN);

                    visitingRate = 0;
                    if (tmpDddlVisiting.SelectedValue != null)
                        float.TryParse(tmpDddlVisiting.SelectedValue, out visitingRate);

                    visitingRateN = 0;
                    if (tmpDddlVisitingN.SelectedValue != null)
                        float.TryParse(tmpDddlVisitingN.SelectedValue, out visitingRateN);

                    int order = 0;
                    if (tmpTxtOrder.Text.Trim().Length > 0)
                        int.TryParse(tmpTxtOrder.Text, out order);

                    rate.HomeRate = homeRate + homeRateN;
                    rate.VisitingRate = visitingRate + visitingRateN;
                    rate.Order = order;
                    lst.Add(rate);
                }
            }

            return lst;
        }

        protected void rptRate_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int index = 0;
                int.TryParse(e.CommandArgument.ToString(), out index);
                List<BettingRate> lst = GetRateList();

                if (lst != null && lst.Count > 0 && index < lst.Count)
                {
                    lst.RemoveAt(index);
                    rptRate.DataSource = lst;
                    rptRate.DataBind();
                }
            }
        }

        protected void rptRate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BettingRate rate = e.Item.DataItem as BettingRate;

                DropDownList tmpDdlHomeN = e.Item.FindControl("ddlHomeN") as DropDownList;
                DropDownList tmpDdlHome = e.Item.FindControl("ddlHome") as DropDownList;
                DropDownList tmpDddlVisitingN = e.Item.FindControl("ddlVisitingN") as DropDownList;
                DropDownList tmpDddlVisiting = e.Item.FindControl("ddlVisiting") as DropDownList;

                ListItem item = tmpDdlHome.Items.FindByValue(TNHelper.GetFloatValue(rate.HomeRate).ToString());
                if (item != null)
                    item.Selected = true;

                item = tmpDdlHomeN.Items.FindByValue(TNHelper.GetIntValue(rate.HomeRate).ToString());
                if (item != null)
                    item.Selected = true;

                item = tmpDddlVisitingN.Items.FindByValue(TNHelper.GetIntValue(rate.VisitingRate).ToString());
                if (item != null)
                    item.Selected = true;

                item = tmpDddlVisiting.Items.FindByValue(TNHelper.GetFloatValue(rate.VisitingRate).ToString());
                if (item != null)
                    item.Selected = true;
            }
        }
    }
}