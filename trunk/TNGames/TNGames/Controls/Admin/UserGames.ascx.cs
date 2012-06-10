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
    public partial class UserGames : System.Web.UI.UserControl
    {
        Pager pager
        {
            get
            {

                if (GameType == 0)
                    return pagerBetting;
                else if (GameType == 1)
                    return pagerPrediction;
                else if (GameType == 2)
                    return pagerQuestion;

                return pagerBetting;
            }
        }

        /// <summary>
        /// 0: betting
        /// 1: prediction
        /// 2: question
        /// default: betting
        /// </summary>
        public int GameType
        {
            get
            {
                int gameType = 0;
                if (Page.Request.QueryString["type"] != null)
                {
                    int.TryParse(Page.Request.QueryString["type"], out gameType);
                }

                return gameType;
            }
        }

        public int PageIndex
        {
            get
            {
                int index = 1;
                if (Page.Request.QueryString["PageIndex"] != null)
                {
                    int.TryParse(Page.Request.QueryString["PageIndex"], out index);
                    index = index > 0 ? index : 1;
                    pager.CurrentIndex = index;
                }

                return pager.CurrentIndex;
            }
        }

        public int PageSize
        {
            get
            {
                int size = 10;
                if (Page.Request.QueryString["PageSize"] != null)
                {
                    int.TryParse(Page.Request.QueryString["PageSize"], out size);
                    size = size > 0 ? size : 10;
                    pager.PageSize = size;
                }

                return pager.PageSize;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (pager != null)
            {
                pager.Visible = (pager.PageCount > 1);
            }
        }

        protected void LoadData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int userId = 0;
            int.TryParse(strId, out userId);

            User user = TNHelper.GetUserById(userId);
            if (user != null)
                litUser.Text = user.DisplayName;

            pnlBetting.Visible = false;
            pnlPrediction.Visible = false;
            pnlQuestion.Visible = false;
            int totalRow = 0;

            if (GameType == 0)
            {
                pnlBetting.Visible = true;

                List<BettingUser> lstBetting = TNHelper.GetBettingUserGameInfo(userId);
                if (lstBetting != null)
                {
                    totalRow = lstBetting.Count;
                    lstBetting = lstBetting.Skip((PageIndex - 1) * PageSize)
                                           .Take(PageSize).ToList();
                }

                if (lstBetting != null && lstBetting.Count > 0)
                {
                    rptBetting.DataSource = lstBetting;
                    rptBetting.DataBind();
                }
                else
                {
                    Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu trò chơi thử tài kiến thức");
                }
            }
            else if (GameType == 1)
            {
                pnlPrediction.Visible = true;

                List<PredictionGameUser> lstPrediction = TNHelper.GetPredictionUserGameInfo(userId);
                if (lstPrediction != null)
                {
                    totalRow = lstPrediction.Count;
                    lstPrediction = lstPrediction.Skip((PageIndex - 1) * PageSize)
                                           .Take(PageSize).ToList();
                }

                if (lstPrediction != null && lstPrediction.Count > 0)
                {
                    rptPrediction.DataSource = lstPrediction;
                    rptPrediction.DataBind();
                }
                else
                {
                    Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu trò chơi dự đoán");
                }
            }
            else if (GameType == 2)
            {
                pnlQuestion.Visible = true;

                List<QuestionUser> lstQuestion = TNHelper.GetQuestionUserGameInfo(userId);
                if (lstQuestion != null)
                {
                    totalRow = lstQuestion.Count;
                    lstQuestion = lstQuestion.Skip((PageIndex - 1) * PageSize)
                                           .Take(PageSize).ToList();
                }

                if (lstQuestion != null && lstQuestion.Count > 0)
                {
                    rptQuestion.DataSource = lstQuestion;
                    rptQuestion.DataBind();
                }
                else
                {
                    Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu trò chơi dự đoán");
                }
            }

            if (pager != null)
                pager.ItemCount = totalRow;
        }

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = currnetPageIndx;
            LoadData();
        }

        protected void rptBetting_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BettingUser bu = e.Item.DataItem as BettingUser;
                Literal litScoreRate = e.Item.FindControl("litScoreRate") as Literal;
                Literal litWinPoint = e.Item.FindControl("litWinPoint") as Literal;
                Repeater rptBettingDetail = e.Item.FindControl("rptBettingDetail") as Repeater;
                Literal litOnSiteTime = e.Item.FindControl("litOnSiteTime") as Literal;

                if (rptBettingDetail != null && bu.BettingUserDetailses.Count > 0)
                {
                    rptBettingDetail.DataSource = bu.BettingUserDetailses;
                    rptBettingDetail.DataBind();
                }

                if (litScoreRate != null)
                {
                    if (bu.Betting != null && bu.Betting.IsUpdateScore)
                        litScoreRate.Text = string.Format("{0} - {1}", bu.Betting.HomeGoalScore, bu.Betting.VisitingGoalScore);
                    else
                        litScoreRate.Text = "Đang cập nhật";
                }

                if (litWinPoint != null)
                {
                    if (bu.Betting != null && bu.Betting.IsCalculate && bu.BettingUserDetailses.Count > 0)
                    {
                        int point = bu.WinPoint - (bu.BettingUserDetailses[0] as BettingUserDetail).BettingPoint;
                        point = (point < 0 ? 0 : point);
                        litWinPoint.Text = point.ToString("N0");
                    }
                    else
                        litWinPoint.Text = "Đang cập nhật";
                }

                if (litOnSiteTime != null)
                {
                    litOnSiteTime.Text = string.Format("{0} phút {1} giây", (bu.Time / 60), (bu.Time % 60));
                }
            }
        }

        protected void rptBetting_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptPrediction_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PredictionGameUser pu = e.Item.DataItem as PredictionGameUser;
                Literal litTotalQuestion = e.Item.FindControl("litTotalQuestion") as Literal;
                Literal litRightAnswer = e.Item.FindControl("litRightAnswer") as Literal;
                Literal litBonusPoint = e.Item.FindControl("litBonusPoint") as Literal;
                Literal litOnSiteTime = e.Item.FindControl("litOnSiteTime") as Literal;
                if (litTotalQuestion != null)
                    litTotalQuestion.Text = pu.PredictionGameUserDetailses.Count.ToString("N0");

                if (litRightAnswer != null)
                {
                    if (pu.PredictionGame != null && pu.PredictionGame.IsCalculate)
                    {
                        int count = 0;
                        foreach (PredictionGameUserDetail detail in pu.PredictionGameUserDetailses)
                        {
                            if (detail.Prediction != null)
                            {
                                PredictionAnswer rightAnswer = detail.Prediction.PredictionAnswerses.Cast<PredictionAnswer>().Where(p => p.IsCorrectAnswer).FirstOrDefault();
                                if (rightAnswer != null && detail.PredictionAnswer != null && rightAnswer.Id == detail.PredictionAnswer.Id)
                                {
                                    count++;
                                }
                            }
                        }
                        litRightAnswer.Text = count.ToString("N0");
                    }
                    else
                    {
                        litRightAnswer.Text = "Đang cập nhật";
                    }
                }

                if (litBonusPoint != null)
                {
                    if (pu.PredictionGame != null && pu.PredictionGame.IsCalculate)
                        litBonusPoint.Text = pu.WinPoint.ToString("N0");
                    else
                        litBonusPoint.Text = "Đang cập nhật";
                }

                if (litOnSiteTime != null)
                {
                    litOnSiteTime.Text = string.Format("{0} phút {1} giây", (pu.Time / 60), (pu.Time % 60));
                }
            }
        }

        protected void rptPrediction_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                QuestionUser qu = e.Item.DataItem as QuestionUser;
                Literal litTotalQuestion = e.Item.FindControl("litTotalQuestion") as Literal;
                Literal litRightAnswer = e.Item.FindControl("litRightAnswer") as Literal;
                Literal litBonusPoint = e.Item.FindControl("litBonusPoint") as Literal;
                Literal litOnSiteTime = e.Item.FindControl("litOnSiteTime") as Literal;
                if (litTotalQuestion != null)
                    litTotalQuestion.Text = qu.QuestionUserDetailses.Count.ToString("N0");

                if (litRightAnswer != null)
                {
                    int count = 0;
                    foreach (QuestionUserDetail detail in qu.QuestionUserDetailses)
                    {
                        if (detail.Question != null && detail.Question.CorrectAnswer != null &&
                            detail.Answer != null && detail.Question.CorrectAnswer.Id == detail.Answer.Id)
                        {
                            count++;
                        }
                    }

                    litRightAnswer.Text = count.ToString("N0");
                }

                if (litBonusPoint != null)
                {
                    litBonusPoint.Text = qu.WinPoint.ToString("N0");
                }

                if (litOnSiteTime != null)
                {
                    litOnSiteTime.Text = string.Format("{0} phút {1} giây", (qu.Time / 60), (qu.Time % 60));
                }
            }
        }

        protected void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = 0;
                int.TryParse(e.CommandArgument.ToString(), out id);

                QuestionUser qu = DomainManager.GetObject<QuestionUser>(id);
                if (qu != null)
                {
                    DomainManager.Delete(qu);
                    TNHelper.RemoveRankingCaches();
                    LoadData();
                    Utils.ShowMessage(lblMsg, "Xóa thông tin game thử tài kiến thức của người chơi thành công.");
                }
            }
        }
    }
}