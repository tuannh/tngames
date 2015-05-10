using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;
using TNGames.Core.Domain;
using TNGames.Core;
using System.Web.Routing;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_BettingGame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BizBettingGameSettings biz = TNHelper.GetBettingGameSettings();
            if (biz != null && biz.IsPaused)
                Page.Response.Redirect("/", true);
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Betting betting = e.Item.DataItem as Betting;
                Literal litHomeRate = e.Item.FindControl("litHomeRate") as Literal;
                Literal litVisitingRate = e.Item.FindControl("litVisitingRate") as Literal;
                Literal litPlayDate = e.Item.FindControl("litPlayDate") as Literal;
                RadioButton radTeamA = e.Item.FindControl("radTeamA") as RadioButton;
                RadioButton radTeamB = e.Item.FindControl("radTeamB") as RadioButton;
                Literal litRateId = e.Item.FindControl("litRateId") as Literal;
                Literal litBettingInfo = e.Item.FindControl("litBettingInfo") as Literal;

                if (litBettingInfo != null && betting.PlayDate.HasValue && betting.BettingRateses.Count > 0)
                {
                    BettingRate rate = betting.BettingRateses[0] as BettingRate;
                    litBettingInfo.Text = string.Format("{0}: {1} - {2} ({3} - {4})", betting.PlayDate.Value.ToString("dd/MM"),
                                    betting.HomeTeam, betting.VisitingTeam, rate.HomeRateText,
                                    rate.VisitingRateText);
                }

                if (radTeamA != null)
                    radTeamA.GroupName = "Betting" + betting.Id;

                if (radTeamB != null)
                    radTeamB.GroupName = "Betting" + betting.Id;

                if (litPlayDate != null && betting.PlayDate.HasValue)
                    litPlayDate.Text = betting.PlayDate.Value.ToString(TNHelper.DateFormat) + "<br/>" + betting.PlayDate.Value.ToString(TNHelper.TimeFormat);

                if (betting.BettingRateses.Count > 0)
                {
                    BettingRate rate = betting.BettingRateses[0] as BettingRate;
                    if (litHomeRate != null)
                        litHomeRate.Text = rate.HomeRateText;

                    if (litVisitingRate != null)
                        litVisitingRate.Text = rate.VisitingRateText;

                    if (litRateId != null)
                        litRateId.Text = rate.Id.ToString();
                }
            }
        }

        private void LoadData()
        {
            List<Betting> lst = TNHelper.GetAllActiveBetting();
            if (lst != null && lst.Count > 0)
            {
                rptList.DataSource = lst;
                rptList.DataBind();
            }

            else
            {
                Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu yêu cầu");
            }
        }
    }
}