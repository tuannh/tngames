using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using TNGames.Core;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_MyAchievement : System.Web.UI.UserControl
    {
        private User LoadUserInfo()
        {
            User user = Utils.GetCurrentUser();
            if (user != null)
            {
                if (litDisplayName != null)
                    litDisplayName.Text = user.DisplayName ?? user.FullName;

                string tmpLink = "";
                if (user.BettingUserses.Count > 0)
                    tmpLink += string.Format("<a href='/thanh-tich/1'{0}>Thử tài phân tích trận đấu</a><br/>", (GameType == 1 ? " class='active'" : ""));

                if (user.PredictionGameUsers.Count > 0)
                    tmpLink += string.Format("<a href='/thanh-tich/2'{0}>Thử tài dự đoán</a><br/>", (GameType == 2 ? " class='active'" : ""));

                if (user.QuestionUserses.Count > 0)
                    tmpLink += string.Format("<a href='/thanh-tich/3'{0}>Thử tài kiến thức</a><br/>", (GameType == 3 ? " class='active'" : ""));

                if (litPlayGame != null)
                {
                    if (!string.IsNullOrEmpty(tmpLink))
                        litPlayGame.Text = tmpLink;
                    else
                        litPlayGame.Text = "Bạn chưa có thông tin các game đã tham gia";
                }
            }

            return user;
        }

        Pager pager
        {
            get
            {

                if (GameType == 1)
                    return pagerBetting;
                else if (GameType == 2)
                    return pagerPrediction;
                else if (GameType == 3)
                    return pagerQuestion;

                return pagerBetting;
            }
        }

        /// <summary>
        /// 1: betting
        /// 2: prediction
        /// 3: question
        /// default: betting
        /// </summary>
        public int GameType
        {
            get
            {
                int gameType = 0;
                string type = Page.RouteData.Values["type"] as string;
                if (!string.IsNullOrEmpty(type))
                    int.TryParse(type, out gameType);

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
                LoadUserInfo();
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
            int userId = 0;
            User user = Utils.GetCurrentUser();
            if (user != null)
                userId = user.Id;

            pnlBetting.Visible = false;
            pnlPrediction.Visible = false;
            pnlQuestion.Visible = false;
            int totalRow = 0;

            if (GameType == 1)
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
            }
            else if (GameType == 2)
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
            }
            else if (GameType == 3)
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
                        int bettingPoint = (bu.BettingUserDetailses[0] as BettingUserDetail).BettingPoint;
                        int point = bu.WinPoint - bettingPoint;
                        float percent = ((float)point / bettingPoint) * 100;
                        if (percent < 0)
                            percent = Math.Abs(percent);

                        // point = (point < 0 ? 0 : point);
                        if (point == 0)
                            litWinPoint.Text = string.Format("<span title='Bạn được trả lại {1} điểm đã đặt'>{0}</span>", point.ToString("N0"), bettingPoint.ToString("N0"));
                        else if (point > 0)
                            litWinPoint.Text = string.Format("<span title='Bạn được thưởng {1}% số điểm đã đặt'>{0}</span>", point.ToString("N0"), percent.ToString("N0"));
                        else
                            litWinPoint.Text = string.Format("<span title='Bạn bị mất {1}% số điểm đã đặt'>{0}</span>", point.ToString("N0"), percent.ToString("N0"));
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
                BizPredictionGameSettings setting = TNHelper.GetPredictionGameSettings();
                PredictionGameUser pu = e.Item.DataItem as PredictionGameUser;
                Literal litTotalQuestion = e.Item.FindControl("litTotalQuestion") as Literal;
                Literal litRightAnswer = e.Item.FindControl("litRightAnswer") as Literal;
                Literal litBonusPoint = e.Item.FindControl("litBonusPoint") as Literal;
                Literal litOnSiteTime = e.Item.FindControl("litOnSiteTime") as Literal;
                Repeater rptPDetail = e.Item.FindControl("rptPDetail") as Repeater;
                Literal litPDetail = e.Item.FindControl("litPDetail") as Literal;
                Panel pnlPopupPDetail = e.Item.FindControl("pnlPopupPDetail") as Panel;

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

                if (litPDetail != null)
                    litPDetail.Text = string.Format("<a class='fancybox' href='#p{0}'>Chi tiết</a>", pu.Id);

                if (rptPDetail != null && pu.PredictionGameUserDetailses != null)
                {
                    rptPDetail.DataSource = pu.PredictionGameUserDetailses.Cast<PredictionGameUserDetail>();
                    rptPDetail.ItemDataBound += new RepeaterItemEventHandler(rptPDetail_ItemDataBound);
                    rptPDetail.DataBind();
                }
            }
        }

        void rptPDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                 e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptPAnswer = e.Item.FindControl("rptPAnswer") as Repeater;
                Repeater rptPUserAnswer = e.Item.FindControl("rptPUserAnswer") as Repeater;

                PredictionGameUserDetail pud = e.Item.DataItem as PredictionGameUserDetail;
                if (rptPAnswer != null && pud.Prediction != null)
                {
                    rptPAnswer.DataSource = pud.Prediction.PredictionAnswerses;
                    rptPAnswer.DataBind();
                }

                if (rptPUserAnswer != null && pud.Prediction != null)
                {
                    if (pud.Prediction.PredictionAnswerses != null)
                    {
                        foreach (PredictionAnswer ans in pud.Prediction.PredictionAnswerses)
                        {
                            if (ans.Id == pud.PredictionAnswer.Id)
                            {
                                ans.IsUserAnswer = true;
                                break;
                            }
                        }
                    }

                    rptPUserAnswer.DataSource = pud.Prediction.PredictionAnswerses;
                    rptPUserAnswer.ItemDataBound += new RepeaterItemEventHandler(rptPUserAnswer_ItemDataBound);
                    rptPUserAnswer.DataBind();
                }
            }
        }

        void rptPUserAnswer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
               e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PredictionAnswer answer = e.Item.DataItem as PredictionAnswer;
                Literal litAnswerCSS = e.Item.FindControl("litAnswerCSS") as Literal;

                if (litAnswerCSS != null)
                {
                    if (answer.IsUserAnswer)
                    {
                        if (answer.IsCorrectAnswer)
                            litAnswerCSS.Text = "class='yourAnswer'";
                        else if (answer.Prediction != null && answer.Prediction.PredictionGame != null && !answer.Prediction.PredictionGame.IsCalculate)
                            litAnswerCSS.Text = "class='yourAnswer'";
                        else
                            litAnswerCSS.Text = "class='yourAnswer incorectAnswer'";
                    }
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

                BizQuestionGameSettings setting = TNHelper.GetQuestionGameSettings();
                QuestionUser qu = e.Item.DataItem as QuestionUser;
                Literal litTotalQuestion = e.Item.FindControl("litTotalQuestion") as Literal;
                Literal litRightAnswer = e.Item.FindControl("litRightAnswer") as Literal;
                Literal litBonusPoint = e.Item.FindControl("litBonusPoint") as Literal;
                Literal litOnSiteTime = e.Item.FindControl("litOnSiteTime") as Literal;
                Repeater rptQDetail = e.Item.FindControl("rptQDetail") as Repeater;
                Literal litQDetail = e.Item.FindControl("litQDetail") as Literal;
                Panel pnlPopupQDetail = e.Item.FindControl("pnlPopupQDetail") as Panel;

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


                if (qu.QuestionGame != null && qu.QuestionGame.Id != setting.QuestionGameID)
                {
                    if (litQDetail != null)
                        litQDetail.Text = string.Format("<a class='fancybox' href='#q{0}'>Chi tiết</a>", qu.Id);

                    if (rptQDetail != null && qu.QuestionUserDetailses != null)
                    {
                        rptQDetail.DataSource = qu.QuestionUserDetailses.Cast<QuestionUserDetail>();
                        rptQDetail.ItemDataBound += new RepeaterItemEventHandler(rptQDetail_ItemDataBound);
                        rptQDetail.DataBind();
                    }
                }
                else
                {
                    if (litQDetail != null)
                        litQDetail.Text = string.Format("<a href='javascript:void(0);' title='{0}'>Đang cập nhật</a>",
                                                        "Bạn chỉ được xem chi tiết đáp án khi bộ đề câu hỏi này không còn được sử dụng ở trang trò chơi thử tài kiến thức");

                    if (pnlPopupQDetail != null)
                        pnlPopupQDetail.Visible = false;
                }
            }
        }

        void rptQDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptAnswer = e.Item.FindControl("rptAnswer") as Repeater;
                Repeater rptUserAnswer = e.Item.FindControl("rptUserAnswer") as Repeater;

                QuestionUserDetail qud = e.Item.DataItem as QuestionUserDetail;
                if (rptAnswer != null && qud.Question != null)
                {
                    rptAnswer.DataSource = qud.Question.Answerses;
                    rptAnswer.DataBind();
                }

                if (rptUserAnswer != null && qud.Question != null)
                {
                    if (qud.Question.Answerses != null)
                    {
                        foreach (Answer ans in qud.Question.Answerses)
                        {
                            if (ans.Id == qud.Answer.Id)
                            {
                                ans.IsUserAnswer = true;
                                break;
                            }
                        }
                    }

                    rptUserAnswer.DataSource = qud.Question.Answerses;
                    rptUserAnswer.ItemDataBound += new RepeaterItemEventHandler(rptUserAnswer_ItemDataBound);
                    rptUserAnswer.DataBind();
                }
            }
        }

        void rptUserAnswer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Answer answer = e.Item.DataItem as Answer;
                Literal litAnswerCSS = e.Item.FindControl("litAnswerCSS") as Literal;

                if (litAnswerCSS != null)
                {
                    if (answer.IsUserAnswer)
                    {
                        if (answer.IsCorrectAnswer)
                            litAnswerCSS.Text = "class='yourAnswer'";
                        else
                            litAnswerCSS.Text = "class='yourAnswer incorectAnswer'";
                    }
                }
            }
        }

        protected void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}