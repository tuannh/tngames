using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Cache;
using TNGames.Core.Helper;
using TNGames.Core;
using TNGames.Core.Domain;
using System.Drawing;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_QuestionGame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
                Page.Session[TNHelper.QuestionStarTimeKey] = DateTime.Now;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BizQuestionGameSettings biz = TNHelper.GetQuestionGameSettings();
            if (biz != null && biz.IsPaused)
                Page.Response.Redirect("/", true);

            if (!TNHelper.IsValidToPlayQuestionGame())
            {
                Utils.ShowMessage(lblMsg, "Bạn đã hết số lượt chơi trong ngày. Mời bạn quay lại sau.");
                pnlUpdate.Visible = false;
                pnlStart.Visible = false;
            }
        }

        protected void LoadData()
        {
            // load setting 
            BizQuestionGameSettings settings = TNHelper.GetQuestionGameSettings();
            if (settings != null && settings.Timer > 0)
            {
                hfTimer.Value = settings.Timer.ToString();
                pnlQuestion.Attributes["style"] = "display: none";
            }

            // load radom question and save to cache
            QuestionGame qgame = TNHelper.GetCurrentQuestionGame();
            if (qgame != null && qgame.Questionses.Count > 0)
            {
                string key = string.Format("Question-{0}", Guid.NewGuid().ToString());
                CMSCache.Insert(key, qgame);

                Question question = qgame.Questionses[0] as Question;
                LoadAnswerList(question);
                litQuestion.Text = question.QuestionName;
                litInfo.Text = string.Format("Bạn đang trả lời câu hỏi {0}/{1}", 1, qgame.Questionses.Count);

                hfIndex.Value = "0";
                hfTotal.Value = qgame.Questionses.Count.ToString();
                hfCache.Value = key;
                hfID.Value = question.Id.ToString();
            }
            else
            {
                Utils.ShowMessage(lblMsg, "Mời bạn quay lại sau, bạn vui lòng xem thông báo ở cột bên phải để biết thêm chi tiết");
                divContainer.Visible = false;
            }
        }

        protected void LoadAnswerList(Question question)
        {
            if (question != null)
            {
                radList.DataSource = question.Answerses;
                radList.DataTextField = "AnswerText";
                radList.DataValueField = "Id";
                radList.DataBind();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            pnlQuestion.Attributes.Remove("style");
            int index = int.Parse(hfIndex.Value);
            int total = int.Parse(hfTotal.Value);
            if (index < total)
                SaveCurrentDataToCache();

            index++;
            if (index < total)
            {
                hfIndex.Value = index.ToString();
                litInfo.Text = string.Format("Bạn đang trả lời câu hỏi {0}/{1}", (index + 1), total);
                radList.ClearSelection();

                string key = hfCache.Value;
                QuestionGame qgame = CMSCache.Get(key) as QuestionGame;

                if (qgame != null && qgame.Questionses.Count == total && index < total)
                {
                    Question question = qgame.Questionses[index] as Question;
                    LoadAnswerList(question);
                    litQuestion.Text = question.QuestionName;
                    hfID.Value = question.Id.ToString();
                }
            }

            if (index + 1 == total)
            {
                btnNext.Text = "Xem kết quả";
                btnNext.CssClass = "buttonL last";
            }

            if (index == total)
            {
                btnSubmit_Click(btnSubmit, e);
            }
            else
            {
                pnlQuestion.Visible = true;
                pnlSummary.Visible = false;
            }
        }

        private void SavePlayGame(out int rightAnswer, out int bonusPoint)
        {
            bonusPoint = 0;
            rightAnswer = 0;

            User curenttUser = Utils.GetCurrentUser();
            curenttUser = DomainManager.GetObject<User>(curenttUser.Id);
            if (curenttUser != null)
            {
                int time = 0;
                QuestionUser qu = new QuestionUser();
                qu.PlayDate = DateTime.Now;
                qu.User = curenttUser;
                qu.Time = time;

                string key2 = hfCache.Value;
                QuestionGame qgame = CMSCache.Get(key2) as QuestionGame;
                if (qgame != null)
                {
                    qu.QuestionGame = qgame;

                    foreach (Question p in qgame.Questionses.Cast<Question>())
                    {
                        Answer userAnswer = GetUserAnswer(p);
                        Question question = DomainManager.GetObject<Question>(p.Id);

                        if (userAnswer != null)
                        {
                            QuestionUserDetail detail = new QuestionUserDetail();
                            detail.QuestionUser = qu;
                            detail.Question = question;
                            detail.Answer = userAnswer;
                            qu.QuestionUserDetailses.Add(detail);
                        }

                        if (question != null && question.CorrectAnswer != null &&
                            userAnswer != null && question.CorrectAnswer.Id == userAnswer.Id)
                        {
                            bonusPoint += question.BonusPoint;
                            rightAnswer++;
                        }
                    }
                }


                TimeSpan? onsiteTime = null;
                if (Page.Session[TNHelper.QuestionStarTimeKey] is DateTime)
                {
                    onsiteTime = DateTime.Now - (DateTime)Page.Session[TNHelper.QuestionStarTimeKey];
                    qu.Time = onsiteTime.Value.Seconds;
                }

                qu.WinPoint = bonusPoint;
                if (qu.QuestionGame != null)
                {
                    DomainManager.Insert(qu);

                    curenttUser.PointQuestion += bonusPoint; // cộng vào tổng điểm của game trả lời câu hỏi
                    curenttUser.Point += bonusPoint; // cộng điểm thưởng vào tổng điểm của game cá cược

                    DomainManager.Insert(curenttUser);
                    Utils.ResetCurrentUser();
                    if (bonusPoint == 0)
                        TNHelper.LogAction(LogType.QuestionLog, "Chơi game thử tài kiến thức");
                    else
                        TNHelper.LogAction(LogType.QuestionLog, string.Format("Cộng {0} điểm thưởng vào tổng điểm game thử tài kiến thức.<br/>Cộng {0} điểm thưởng vào tổng điểm game phân tích trận đấu", bonusPoint));
                }
                // remove all relate cache
                CMSCache.RemoveByPattern(hfCache.Value);
            }
        }

        private Answer GetUserAnswer(Question p)
        {
            int answerValue = -1;
            string key = string.Format("UserAnswerList-{0}", hfCache.Value);
            Dictionary<int, int> qa = CMSCache.Get(key) as Dictionary<int, int>;
            if (qa != null && qa.Count > 0)
            {
                if (!qa.TryGetValue(p.Id, out answerValue))
                    answerValue = -1;
            }

            return DomainManager.GetObject<Answer>(answerValue);
        }

        private void SaveCurrentDataToCache()
        {
            int index = int.Parse(hfIndex.Value);
            int total = int.Parse(hfTotal.Value);
            int id = int.Parse(hfID.Value);
            int answer = -1;

            if (!string.IsNullOrEmpty(radList.SelectedValue))
                answer = int.Parse(radList.SelectedValue);

            string key = string.Format("UserAnswerList-{0}", hfCache.Value);
            Dictionary<int, int> qa = CMSCache.Get(key) as Dictionary<int, int>;
            if (qa == null)
                qa = new Dictionary<int, int>();

            if (!qa.Keys.Contains(id))
                qa.Add(id, answer);

            if (answer >= 0)
                CMSCache.Insert(key, qa);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int rightAnswer = 0;
            int bonusPoint = 0;
            int total = int.Parse(hfTotal.Value);
            int index = int.Parse(hfIndex.Value);
            int numOfAnswer = 0;

            string key = string.Format("UserAnswerList-{0}", hfCache.Value);
            Dictionary<int, int> qa = CMSCache.Get(key) as Dictionary<int, int>;
            if (qa != null)
                numOfAnswer = qa.Count;

            SavePlayGame(out rightAnswer, out bonusPoint);
            pnlQuestion.Visible = false;
            pnlSummary.Visible = true;

            if (bonusPoint > 0)
                litBonuss.Text = string.Format("Bạn được cộng {0} điểm vào tài khoản", bonusPoint);
            else
                litBonuss.Text = "Bạn không được điểm thưởng trong trò chơi này";

            litSummry.Visible = false; // không hiển thị số câu trả lời đúng cho người chơi biết
            if (numOfAnswer == total)
                litSummry.Text = string.Format("Bạn đã trả lời đúng {0}/{1} câu hỏi.", rightAnswer, total);
            else
                litSummry.Text = string.Format("Bạn đã trả lời {0}/{1} câu hỏi. Số câu trả lời đúng {2}", numOfAnswer, total, rightAnswer);

            lblTimeout.Visible = false;
            pnlQuestion.Visible = false;
            pnlSummary.Visible = true;
            btnSubmit.Visible = false;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("/", true);
        }
    }
}