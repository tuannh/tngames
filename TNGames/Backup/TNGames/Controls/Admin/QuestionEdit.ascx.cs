using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core;
using TNGames.Core.Domain;
using TNGames.Core.Helper;

namespace TNGames.Controls.Admin
{
    public partial class QuestionEdit : System.Web.UI.UserControl
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
            List<Answer> lstTmp = new List<Answer>();
            lstTmp.Add(new Answer());
            lstTmp.Add(new Answer());
            lstTmp.Add(new Answer());
            lstTmp.Add(new Answer());
            lstTmp.Add(new Answer());

            rptAnswers.DataSource = lstTmp;
            rptAnswers.DataBind();
        }

        protected void LoadEditData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            QuestionGame obj = DomainManager.GetObject<QuestionGame>(id);
            if (obj != null)
            {
                BizQuestionGameSettings biz = TNHelper.GetQuestionGameSettings();
                bool isPermit = true;
                if (biz != null)
                {
                    isPermit = biz.QuestionGameID == obj.Id ? false : true;
                }

                btnDelete.Enabled = isPermit;
                btnSave.Enabled = isPermit;
                trAdd.Visible = isPermit;

                txtQGName.Text = obj.QuestionGameName;
                radYes.Checked = obj.Active;
                radNo.Checked = !obj.Active;

                rptQuestion.DataSource = obj.Questionses;
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

            List<Question> lstQuestion = GetQuestionList();
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

                QuestionGame qgame = DomainManager.GetObject<QuestionGame>(id);
                if (qgame == null)
                {
                    qgame = new QuestionGame();
                    qgame.CreatedDate = DateTime.Now;
                    qgame.User = Utils.GetCurrentUser();
                }

                qgame.QuestionGameName = TextInputUtil.GetSafeInput(txtQGName.Text);
                qgame.Active = radYes.Checked;

                if (qgame.Id > 0)
                    DomainManager.Update(qgame);
                else
                    DomainManager.Insert(qgame);

                // qgame = DomainManager.GetObject<QuestionGame>(qgame.Id);

                if (lstQuestion != null)
                {
                    for (int i = 0; i < lstQuestion.Count; i++)
                    {
                        Question question = lstQuestion[i];
                        if (question.Id == 0)
                        {
                            DomainManager.Insert(question);
                            question.QuestionGame = qgame;
                            qgame.Questionses.Add(question);
                        }
                        else // update data
                        {
                            Question tmpQ = qgame.Questionses.Cast<Question>().Where(p => p.Id == question.Id).FirstOrDefault();
                            if (tmpQ != null)
                            {
                                tmpQ.QuestionName = question.QuestionName;
                                tmpQ.BonusPoint = question.BonusPoint;
                                foreach (Answer ans in question.Answerses.Cast<Answer>())
                                {
                                    Answer tmpA = tmpQ.Answerses.Cast<Answer>().Where(p => p.Id == ans.Id).FirstOrDefault();
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
                    msg = Page.Server.UrlEncode("Cập nhật bộ đề câu hỏi thành công");
                }
                else
                {
                    DomainManager.Insert(qgame);
                    msg = Page.Server.UrlEncode("Thêm bộ đề game trả lời câu hỏi thành công");
                }

                Page.Response.Redirect(string.Format("/admincp/question-list?msg={0}", msg), true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            QuestionGame obj = DomainManager.GetObject<QuestionGame>(id);
            if (obj != null)
            {
                DomainManager.Delete(obj);
                string msg = Page.Server.UrlEncode("Xóa bộ đề game trả lời câu hỏi thành công");
                Page.Response.Redirect(string.Format("/admincp/question-list?msg={0}", msg), true);
            }

        }

        protected void rptAnswers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Answer answer = e.Item.DataItem as Answer;

                RadioButton radAnswer = e.Item.FindControl("radAnswer") as RadioButton;
                TextBox txtAnswer = e.Item.FindControl("txtAnswer") as TextBox;
                HiddenField hfAnswerID = e.Item.FindControl("hfAnswerID") as HiddenField;
                RequiredFieldValidator rfv = e.Item.FindControl("rfv") as RequiredFieldValidator;

                if (rfv != null && e.Item.ItemIndex > 1)
                    rfv.Visible = false;

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

            Question question = new Question();
            question.QuestionName = TextInputUtil.GetSafeInput(txtQuestion.Text);
            question.BonusPoint = point;

            List<Answer> lstAnswer = GetAnswerList(rptAnswers);
            if (lstAnswer != null)
            {
                foreach (Answer answer in lstAnswer)
                {
                    answer.Question = question;
                    question.Answerses.Add(answer);
                }
            }

            List<Question> lst = GetQuestionList();
            if (lst != null)
                lst.Add(question);

            rptQuestion.DataSource = lst;
            rptQuestion.DataBind();

            txtQuestion.Text = string.Empty;
            txtBonus.Text = string.Empty;
            LoadDefaultData();
        }

        private List<Question> GetQuestionList()
        {
            List<Question> lst = new List<Question>();

            foreach (RepeaterItem item in rptQuestion.Items)
            {
                HiddenField hfID = item.FindControl("hfID") as HiddenField;
                TextBox litQuestionName = item.FindControl("txtQuestionName") as TextBox;
                TextBox litBonus = item.FindControl("txtBonus") as TextBox;
                Repeater subrptAnswer = item.FindControl("rptAnswer") as Repeater;

                Question question = new Question();
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
                    question.QuestionName = litQuestionName.Text;

                if (litBonus != null)
                {
                    int point = 0;
                    int.TryParse(litBonus.Text.Trim(), out point);
                    question.BonusPoint = point;
                }

                List<Answer> lstAnswer = GetAnswerList(subrptAnswer);
                if (lstAnswer != null)
                {
                    foreach (Answer answer in lstAnswer)
                    {
                        answer.Question = question;
                        question.Answerses.Add(answer);
                    }
                }

                lst.Add(question);
            }

            return lst;
        }

        private List<Answer> GetAnswerList(Repeater rpt)
        {
            List<Answer> lst = new List<Answer>();
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

                    Answer answer = new Answer();
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
                Question question = e.Item.DataItem as Question;
                Repeater rptAnswer = e.Item.FindControl("rptAnswer") as Repeater;
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;

                if (lnkDelete != null)
                {
                    string strId = Page.RouteData.Values["id"] as string;
                    int gameid = 0;
                    int.TryParse(strId, out gameid);
                    BizQuestionGameSettings biz = TNHelper.GetQuestionGameSettings();

                    if (biz != null && biz.QuestionGameID == gameid)
                    {
                        lnkDelete.Enabled = false;
                        lnkDelete.OnClientClick = "";
                    }
                }

                if (rptAnswer != null)
                {
                    rptAnswer.DataSource = question.Answerses;
                    rptAnswer.ItemDataBound += new RepeaterItemEventHandler(rptAnswer_ItemDataBound);
                    rptAnswer.DataBind();
                }
            }
        }

        void rptAnswer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Answer ans = e.Item.DataItem as Answer;
                Control trAnswer = e.Item.FindControl("trAnswer") as Control;

                if (ans.AnswerText.Trim().Length == 0 && trAnswer != null)
                    trAnswer.Visible = false;
            }
        }

        protected void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int index = -1;
                int.TryParse(e.CommandArgument.ToString(), out index);

                List<Question> lstQuestion = GetQuestionList();
                if (lstQuestion != null && lstQuestion.Count > index && index >= 0)
                {
                    Question question = lstQuestion[index];
                    if (question.Id > 0)
                        DomainManager.Delete(typeof(Question), question.Id);

                    lstQuestion.RemoveAt(index);

                    rptQuestion.DataSource = lstQuestion;
                    rptQuestion.DataBind();
                }
            }
        }
    }
}