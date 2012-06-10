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
    public partial class PredictionEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadEditData();
            }
        }

        protected void LoadDefaultData()
        {
            List<PredictionAnswer> lstTmp = new List<PredictionAnswer>();
            lstTmp.Add(new PredictionAnswer());
            lstTmp.Add(new PredictionAnswer());
            lstTmp.Add(new PredictionAnswer());
            lstTmp.Add(new PredictionAnswer());
            lstTmp.Add(new PredictionAnswer());

            rptAnswers.DataSource = lstTmp;
            rptAnswers.DataBind();
        }

        protected void LoadEditData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            PredictionGame obj = DomainManager.GetObject<PredictionGame>(id);
            if (obj != null)
            {
                BizPredictionGameSettings biz = TNHelper.GetPredictionGameSettings();
                bool isPermit = true;
                if (biz != null)
                {
                    isPermit = biz.PredictionGameID == obj.Id ? false : true;
                    if (obj.PredictionGameUsers.Count > 0)
                        isPermit = false;
                }

                btnDelete.Enabled = isPermit;
                trAdd.Visible = isPermit;

                if (obj.IsCalculate)
                {
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    trAdd.Visible = false;
                }

                txtQGName.Text = obj.PredictionGameName;
                radYes.Checked = obj.Active;
                radNo.Checked = !obj.Active;

                txtQGName.Enabled = isPermit;
                radYes.Enabled = isPermit;
                radNo.Enabled = isPermit;

                rptQuestion.DataSource = obj.Predictionses;
                rptQuestion.DataBind();
            }
            else if (!string.IsNullOrEmpty(strId))
            {
                Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu của bộ đề bạn yêu cầu");
            }

            LoadDefaultData();
            btnDelete.Visible = (id > 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Valid data

            if (txtQGName.Text.Trim().Length == 0)
            {
                Utils.ShowMessage(lblMsg, "Tên bộ đè không thể rỗng");
                return;
            }

            List<Prediction> lstQuestion = GetQuestionList();
            if (lstQuestion == null || (lstQuestion != null && lstQuestion.Count == 0))
            {
                Utils.ShowMessage(lblMsg, "Bạn chưa nhập câu hỏi");
                return;
            }

            #endregion

            if (Page.IsValid)
            {
                string strId = Page.RouteData.Values["id"] as string;
                int id = 0;
                int.TryParse(strId, out id);

                PredictionGame qgame = DomainManager.GetObject<PredictionGame>(id);
                if (qgame == null)
                {
                    qgame = new PredictionGame();
                    qgame.CreatedDate = DateTime.Now;
                    qgame.User = Utils.GetCurrentUser();
                }

                qgame.PredictionGameName = TextInputUtil.GetSafeInput(txtQGName.Text);
                qgame.Active = radYes.Checked;
                qgame.IsUpdateAnswer = CheckUpdateAnswer(lstQuestion);
                if (qgame.Id > 0)
                    DomainManager.Update(qgame);
                else
                    DomainManager.Insert(qgame);

                if (lstQuestion != null)
                {
                    for (int i = 0; i < lstQuestion.Count; i++)
                    {
                        Prediction question = lstQuestion[i];
                        if (question.Id == 0)
                        {
                            // Prediction newPrediction = SavePrediction(question);
                            DomainManager.Insert(question);
                            question.PredictionGame = qgame;
                            qgame.Predictionses.Add(question);
                        }
                        else // update data
                        {
                            Prediction tmpQ = qgame.Predictionses.Cast<Prediction>().Where(p => p.Id == question.Id).FirstOrDefault();
                            if (tmpQ != null)
                            {
                                tmpQ.PredictionName = question.PredictionName;
                                tmpQ.BonusPoint = question.BonusPoint;
                                foreach (PredictionAnswer ans in question.PredictionAnswerses.Cast<PredictionAnswer>())
                                {
                                    PredictionAnswer tmpA = tmpQ.PredictionAnswerses.Cast<PredictionAnswer>().Where(p => p.Id == ans.Id).FirstOrDefault();
                                    if (tmpA != null)
                                    {
                                        tmpA.AnswerText = ans.AnswerText;
                                        tmpA.IsCorrectAnswer = ans.IsCorrectAnswer;
                                    }
                                }
                            }
                        }
                    }
                }

                string msg = string.Empty;
                if (qgame.Id > 0)
                {
                    DomainManager.Update(qgame);
                    msg = Page.Server.UrlEncode("Cập nhật bộ đề dự đoán thành công");
                }
                else
                {
                    DomainManager.Insert(qgame);
                    msg = Page.Server.UrlEncode("Thêm bộ dự đoán thành công");
                }

                Page.Response.Redirect(string.Format("/admincp/prediction-list?msg={0}", msg), true);
            }
        }

        private Prediction SavePrediction(Prediction question)
        {
            Prediction obj = new Prediction();
            obj.CreatedDate = DateTime.Now;
            obj.ModifiedDate = DateTime.Now;
            obj.PredictionName = question.PredictionName;
            obj.Active = question.Active;
            obj.BonusPoint = question.BonusPoint;
            foreach (PredictionAnswer answer in question.PredictionAnswerses)
            {
                PredictionAnswer ansObj = new PredictionAnswer();
                ansObj.AnswerText = answer.AnswerText;
                ansObj.IsCorrectAnswer = answer.IsCorrectAnswer;
                ansObj.Prediction = obj;
                obj.PredictionAnswerses.Add(ansObj);
            }

            DomainManager.Insert(obj);
            return obj;
        }

        private bool CheckUpdateAnswer(List<Prediction> lstQuestion)
        {
            if (lstQuestion != null)
            {
                foreach (Prediction prediction in lstQuestion)
                {
                    PredictionAnswer rightAnswer = prediction.PredictionAnswerses.Cast<PredictionAnswer>().Where(p => p.IsCorrectAnswer).FirstOrDefault();
                    if (rightAnswer == null)
                        return false;
                }
            }

            return true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            PredictionGame obj = DomainManager.GetObject<PredictionGame>(id);
            if (obj != null)
            {
                DomainManager.Delete(obj);
                string msg = Page.Server.UrlEncode("Xóa bộ đề game dự đoán thành công");
                Page.Response.Redirect(string.Format("/admincp/prediction-list?msg={0}", msg), true);
            }

        }

        protected void rptAnswers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PredictionAnswer answer = e.Item.DataItem as PredictionAnswer;

                RadioButton radAnswer = e.Item.FindControl("radAnswer") as RadioButton;
                TextBox txtAnswer = e.Item.FindControl("txtAnswer") as TextBox;
                HiddenField hfAnswerID = e.Item.FindControl("hfAnswerID") as HiddenField;

                if (radAnswer != null)
                    radAnswer.Checked = answer.IsCorrectAnswer;

                if (txtAnswer != null)
                    txtAnswer.Text = answer.AnswerText;

                if (hfAnswerID != null)
                    hfAnswerID.Value = answer.Id.ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int point = 0;
            int.TryParse(txtBonus.Text.Trim(), out point);

            Prediction question = new Prediction();
            question.PredictionName = TextInputUtil.GetSafeInput(txtQuestion.Text);
            question.BonusPoint = point;

            List<PredictionAnswer> lstAnswer = GetAnswerList(rptAnswers);
            if (lstAnswer != null)
            {
                foreach (PredictionAnswer answer in lstAnswer)
                {
                    answer.Prediction = question;
                    question.PredictionAnswerses.Add(answer);
                }
            }

            List<Prediction> lst = GetQuestionList();
            if (lst != null)
                lst.Add(question);

            rptQuestion.DataSource = lst;
            rptQuestion.DataBind();

            txtQuestion.Text = string.Empty;
            txtBonus.Text = string.Empty;
            LoadDefaultData();
        }

        private List<Prediction> GetQuestionList()
        {
            List<Prediction> lst = new List<Prediction>();

            foreach (RepeaterItem item in rptQuestion.Items)
            {
                HiddenField hfID = item.FindControl("hfID") as HiddenField;
                TextBox litQuestionName = item.FindControl("txtQuestionName") as TextBox;
                TextBox litBonus = item.FindControl("txtBonus") as TextBox;
                Repeater subrptAnswer = item.FindControl("rptAnswer") as Repeater;

                Prediction question = new Prediction();
                question.CreatedDate = DateTime.Now;
                question.ModifiedDate = DateTime.Now;
                question.Active = true;

                if (hfID != null)
                {
                    int id = 0;
                    int.TryParse(hfID.Value, out id);
                    question.Id = id;
                }

                if (litQuestionName != null)
                    question.PredictionName = litQuestionName.Text;

                if (litBonus != null)
                {
                    int point = 0;
                    int.TryParse(litBonus.Text.Trim(), out point);
                    question.BonusPoint = point;
                }

                List<PredictionAnswer> lstAnswer = GetAnswerList(subrptAnswer);
                if (lstAnswer != null)
                {
                    foreach (PredictionAnswer answer in lstAnswer)
                    {
                        answer.Prediction = question;
                        question.PredictionAnswerses.Add(answer);
                    }
                }

                lst.Add(question);
            }

            return lst;
        }

        private List<PredictionAnswer> GetAnswerList(Repeater rpt)
        {
            List<PredictionAnswer> lst = new List<PredictionAnswer>();
            if (rpt != null)
            {
                foreach (RepeaterItem item in rpt.Items)
                {
                    RadioButton radAnswer = item.FindControl("radAnswer") as RadioButton;
                    TextBox txtAnswer = item.FindControl("txtAnswer") as TextBox;
                    HiddenField hfAnswerID = item.FindControl("hfAnswerID") as HiddenField;

                    bool isCorrectAnswer = false;
                    string answerText = string.Empty;
                    int answerID = 0;

                    if (radAnswer != null)
                        isCorrectAnswer = radAnswer.Checked;

                    if (txtAnswer != null)
                        answerText = txtAnswer.Text;

                    if (hfAnswerID != null)
                        int.TryParse(hfAnswerID.Value, out answerID);

                    PredictionAnswer answer = new PredictionAnswer();
                    answer.Id = answerID;
                    answer.AnswerText = answerText;
                    answer.IsCorrectAnswer = isCorrectAnswer;

                    lst.Add(answer);
                }
            }

            return lst;
        }

        protected void rptQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string strId = Page.RouteData.Values["id"] as string;
                int gameid = 0;
                int.TryParse(strId, out gameid);

                Prediction question = e.Item.DataItem as Prediction;
                Repeater rptAnswer = e.Item.FindControl("rptAnswer") as Repeater;
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
                TextBox txtQuestionName = e.Item.FindControl("txtQuestionName") as TextBox;
                TextBox txtBonus = e.Item.FindControl("txtBonus") as TextBox;

                BizPredictionGameSettings biz = TNHelper.GetPredictionGameSettings();
                PredictionGame pgame = DomainManager.GetObject<PredictionGame>(gameid);
                if ((biz.PredictionGameID == gameid) || (pgame != null && pgame.PredictionGameUsers.Count > 0))
                {
                    if (txtQuestionName != null)
                        txtQuestionName.Enabled = false;

                    if (txtBonus != null)
                        txtBonus.Enabled = false;
                }

                if (lnkDelete != null)
                {
                    if ((biz != null && biz.PredictionGameID == gameid))
                    {
                        lnkDelete.Enabled = false;
                        lnkDelete.OnClientClick = "";
                    }
                    else
                    {
                        // những dự đoán có người chơi sẽ không đựoc xóa câu hỏi
                        if (pgame != null && pgame.PredictionGameUsers.Count > 0)
                        {
                            lnkDelete.Enabled = false;
                            lnkDelete.OnClientClick = "";
                        }
                    }
                }

                if (rptAnswer != null)
                {
                    rptAnswer.DataSource = question.PredictionAnswerses;
                    rptAnswer.DataBind();
                }
            }
        }

        protected void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int index = -1;
                int.TryParse(e.CommandArgument.ToString(), out index);

                List<Prediction> lstQuestion = GetQuestionList();
                if (lstQuestion != null && lstQuestion.Count > index && index >= 0)
                {
                    Prediction question = lstQuestion[index];
                    if (question.Id > 0)
                        DomainManager.Delete(typeof(Prediction), question.Id);

                    lstQuestion.RemoveAt(index);

                    rptQuestion.DataSource = lstQuestion;
                    rptQuestion.DataBind();
                }
            }
        }
    }
}