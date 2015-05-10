using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using TNGames.Core.Cache;
using TNGames.Core;
using DM = TNGames.Core.Domain;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_PredictionGame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
                Page.Session[TNHelper.PredictionStarTimeKey] = DateTime.Now;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BizPredictionGameSettings biz = TNHelper.GetPredictionGameSettings();
            if (biz != null && biz.IsPaused)
                Page.Response.Redirect("/", true);

            // ko check số lần chơi trong ngày theo request mới
            //if (!TNHelper.IsValidToPlayPredictionGame())
            //{
            //    Utils.ShowMessage(lblMsg, "Bạn đã hết số lượt chơi game trong ngày. Mời bạn quay lại sau.");
            //    pnlUpdate.Visible = false;
            //    pnlStart.Visible = false;
            //}
        }

        protected PredictionGameUser PredictionGameUser
        {
            get
            {
                string key = hfCache.Value + "PredictionGameUser";
                return Cache[key] as PredictionGameUser;
            }
            set
            {
                string key = hfCache.Value + "PredictionGameUser";
                Cache[key] = value;
            }
        }

        protected void LoadData()
        {
            string key = string.Format("Prediction-{0}", Guid.NewGuid().ToString());
            hfCache.Value = key;

            // load setting 
            BizPredictionGameSettings settings = TNHelper.GetPredictionGameSettings();
            if (settings != null && settings.Timer > 0)
            {
                hfTimer.Value = settings.Timer.ToString();
                pnlQuestion.Attributes["style"] = "display: none";
            }

            // check user already play this game
            User user = Utils.GetCurrentUser();
            PredictionGameUser pgu = TNHelper.GetPredictionGameUserByGameId(settings.PredictionGameID, user.Id);
            prePlayedInfo.Visible = false;

            if (pgu != null)
            {
                PredictionGameUser = pgu;
                prePlayedInfo.Visible = true;
                divContainer.Attributes["class"] = "invisible " + divContainer.Attributes["class"];
            }

            // load radom question and save to cache
            PredictionGame pgame = TNHelper.GetCurrentPredictionGame();
            if (pgame != null && pgame.Predictionses.Count > 0)
            {

                Prediction prediction = pgame.Predictionses[0] as Prediction;
                LoadAnswerList(prediction);
                litQuestion.Text = prediction.PredictionName;
                litInfo.Text = string.Format("Bạn đang trả lời câu hỏi {0}/{1}", 1, pgame.Predictionses.Count);
                CMSCache.Insert(key, pgame);

                hfIndex.Value = "0";
                hfTotal.Value = pgame.Predictionses.Count.ToString();

                hfID.Value = prediction.Id.ToString();
            }
            else
            {
                Utils.ShowMessage(lblMsg, "Mời bạn quay lại sau, bạn vui lòng xem thông báo ở cột bên phải để biết thêm chi tiết.");
                divContainer.Visible = false;
                prePlayedInfo.Visible = false;
            }
        }

        protected void LoadAnswerList(Prediction prediction)
        {
            if (prediction != null)
            {
                radList.DataSource = prediction.PredictionAnswerses;
                radList.DataTextField = "AnswerText";
                radList.DataValueField = "Id";
                radList.DataBind();

                PredictionGameUser pgu = PredictionGameUser;
                if (pgu != null && pgu.PredictionGameUserDetailses != null)
                {
                    foreach (PredictionGameUserDetail detail in pgu.PredictionGameUserDetailses)
                    {
                        if (detail.Prediction != null && detail.Prediction.Id == prediction.Id)
                        {
                            if (detail.PredictionAnswer != null)
                            {
                                ListItem item = radList.Items.FindByValue(detail.PredictionAnswer.Id.ToString());
                                if (item != null)
                                    item.Selected = true;
                            }
                        }
                    }
                }
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
                PredictionGame pgame = CMSCache.Get(key) as PredictionGame;

                if (pgame != null && pgame.Predictionses.Count == total && index < total)
                {
                    Prediction prediction = pgame.Predictionses[index] as Prediction;
                    LoadAnswerList(prediction);
                    litQuestion.Text = prediction.PredictionName;
                    hfID.Value = prediction.Id.ToString();
                }
            }

            if (index + 1 == total)
            {
                btnNext.Text = "Xem kết quả";
                if (PredictionGameUser is PredictionGameUser)
                    btnNext.Text = "Cập nhật";

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

        private void SavePlayGame()
        {
            User curenttUser = Utils.GetCurrentUser();
            curenttUser = DomainManager.GetObject<User>(curenttUser.Id);

            string cacheKey = hfCache.Value;
            PredictionGame pgame = CMSCache.Get(cacheKey) as PredictionGame;

            if (curenttUser != null && pgame != null)
            {
                PredictionGameUser pgu = new PredictionGameUser();
                pgu.PlayDate = DateTime.Now;
                pgu.PredictionGame = pgame;
                pgu.User = curenttUser;

                TimeSpan? onsiteTime = null;
                if (Page.Session[TNHelper.PredictionStarTimeKey] is DateTime)
                {
                    onsiteTime = DateTime.Now - (DateTime)Page.Session[TNHelper.PredictionStarTimeKey];
                    pgu.Time = onsiteTime.Value.Seconds;
                }

                if (pgame != null && pgame.Predictionses.Count > 0)
                {
                    foreach (Prediction p in pgame.Predictionses.Cast<Prediction>().ToList())
                    {
                        int answerValue = GetUserAnswer(p);
                        Prediction prediction = DomainManager.GetObject<Prediction>(p.Id);

                        if (answerValue >= 0)
                        {
                            PredictionGameUserDetail detail = new PredictionGameUserDetail();
                            detail.PredictionGameUser = pgu;
                            detail.Prediction = prediction;
                            detail.PredictionAnswer = DomainManager.GetObject<PredictionAnswer>(answerValue);
                            pgu.PredictionGameUserDetailses.Add(detail);
                        }
                    }
                }

                if (PredictionGameUser is PredictionGameUser)
                {
                    if (pgu.PredictionGame != null)
                    {
                        UpdateAnswer(pgu);
                        TNHelper.LogAction(LogType.PredictionLog, string.Format("Cập nhận đáp án cho bộ đề <b>{0}</b>", pgu.PredictionGame.PredictionGameName));
                    }
                }
                else
                {
                    if (pgu.PredictionGame != null)
                    {
                        DomainManager.Insert(pgu);
                        TNHelper.LogAction(LogType.PredictionLog, "Chơi game thử tài dự đoán");
                    }
                }

                // remove all relate cache
                Utils.ResetCurrentUser();
                CMSCache.RemoveByPattern(cacheKey);
            }
        }

        private void UpdateAnswer(PredictionGameUser pgu)
        {
            PredictionGameUser pre = DomainManager.GetObject<PredictionGameUser>(PredictionGameUser.Id);
            if (pre != null)
            {
                foreach (PredictionGameUserDetail detail in pre.PredictionGameUserDetailses)
                {
                    if (detail.Prediction != null)
                    {
                        PredictionGameUserDetail tmp = pgu.PredictionGameUserDetailses.Cast<PredictionGameUserDetail>().Where(p => p.Prediction.Id == detail.Prediction.Id).FirstOrDefault();
                        if (tmp != null)
                        {
                            detail.PredictionAnswer = tmp.PredictionAnswer;
                        }
                    }
                }

                DomainManager.Update(pre);
            }
        }

        private int GetUserAnswer(Prediction p)
        {
            int answerValue = -1;
            string key = string.Format("UserAnswerList-{0}", hfCache.Value);
            Dictionary<int, int> qa = CMSCache.Get(key) as Dictionary<int, int>;
            if (qa != null && qa.Count > 0)
            {
                if (!qa.TryGetValue(p.Id, out answerValue))
                    answerValue = -1;
            }

            return answerValue;
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
            SavePlayGame();
            if (PredictionGameUser is PredictionGameUser)
                litSummry.Text = "Cập nhật đáp án cho trò chơi thử tài dự đoán thành công. Bạn vui lòng xem thông báo ở cột bên phải để biết thêm chi tiết";
            else
                litSummry.Text = "Cảm ơn bạn đã tham gia trò chơi thử tài dự đoán. Bạn vui lòng xem thông báo ở cột bên phải để biết thêm chi tiết";

            pnlQuestion.Visible = false;
            lblTimeout.Visible = false;
            pnlQuestion.Visible = false;
            btnSubmit.Visible = false;

            pnlSummary.Visible = true;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("/", true);
        }
    }
}