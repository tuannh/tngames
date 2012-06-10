using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;
using TNGames.Core.Domain;
using TNGames.Core;
using System.Data;

namespace TNGames.Controls.Admin
{
    public partial class QuestionList : System.Web.UI.UserControl
    {
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

        public Pager pager
        {
            get
            {
                if (IsRanking)
                    return pagerRank;
                else if (IsDetail)
                    return pagerList;

                return pagerQG;
            }
        }

        public bool IsRanking
        {
            get
            {
                bool rank = false;
                if (Page.Request.QueryString["isr"] != null)
                    bool.TryParse(Page.Request.QueryString["isr"], out rank);

                return rank;
            }
        }

        public bool IsDetail
        {
            get
            {
                bool detail = false;
                if (Page.Request.QueryString["isd"] != null)
                    bool.TryParse(Page.Request.QueryString["isd"], out detail);

                return detail;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string msg = Page.Request.QueryString["msg"];
                if (!string.IsNullOrEmpty(msg))
                {
                    msg = Page.Server.UrlDecode(msg);

                    if (IsRanking)
                        Utils.ShowMessage(lblMsg2, msg);
                    else if (IsDetail)
                        Utils.ShowMessage(lblMsg, msg);
                    else
                        Utils.ShowMessage(lblMsgQG, msg);
                }

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

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = currnetPageIndx;
            LoadData();
        }

        private void LoadData()
        {
            pnlList.Visible = false;
            pnlRank.Visible = false;
            pnlQuestionGame.Visible = false;

            if (IsRanking)
            {
                DataTable dt = new Database().GetDataTable("uxp_GetQuestionRank", CommandType.StoredProcedure);
                int totalRow = 0;
                List<DataRow> lstRow = new List<DataRow>();
                if (dt != null)
                {
                    totalRow = dt.Rows.Count;
                    lstRow = dt.Rows.Cast<DataRow>()
                                             .Skip((PageIndex - 1) * PageSize)
                               .Take(PageSize).ToList();
                }

                rptRank.DataSource = lstRow;
                rptRank.DataBind();

                pager.ItemCount = totalRow;
                pnlRank.Visible = true;

                if (totalRow == 0)
                {
                    Utils.ShowMessage(lblMsg2, "Không tìm thấy dữ liệu bảng xếp hạng game trả lời câu hỏi");
                    rptRank.Visible = false;
                }
            }
            else if (IsDetail)
            {
                string strId = Page.RouteData.Values["id"] as string;
                int id = 0;
                int.TryParse(strId, out id);
                QuestionGame qgame = DomainManager.GetObject<QuestionGame>(id);
                if (qgame != null)
                {
                    litQGName.Text = qgame.QuestionGameName;
                    List<Question> lst = new List<Question>();
                    int totalRow = 0;
                    if (qgame.Questionses != null)
                    {
                        totalRow = qgame.Questionses.Count;
                        lst = qgame.Questionses.Cast<Question>()
                                   .Skip((PageIndex - 1) * PageSize)
                                   .Take(PageSize)
                                   .ToList();
                    }

                    pager.ItemCount = totalRow;
                    pnlList.Visible = true;

                    rptList.DataSource = lst;
                    rptList.DataBind();

                    if (totalRow == 0)
                    {
                        Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu game trả lời câu hỏi");
                        rptList.Visible = false;
                    }
                }
            }
            else
            {
                IList<QuestionGame> lst = DomainManager.GetAll<QuestionGame>();
                if (lst != null)
                    lst = lst.OrderBy(p => p.QuestionGameName)
                             .ToList();

                int totalRow = 0;
                if (lst != null)
                {
                    totalRow = lst.Count;
                    lst = lst.Skip((PageIndex - 1) * PageSize)
                             .Take(PageSize).ToList();
                }
                pager.ItemCount = totalRow;
                pnlQuestionGame.Visible = true;

                rptQG.DataSource = lst;
                rptQG.DataBind();

                if (totalRow == 0)
                {
                    Utils.ShowMessage(lblMsg, "Không tìm thấy dữ bộ đề game câu hỏi");
                    rptList.Visible = false;
                }
            }

        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Question question = e.Item.DataItem as Question;
                Repeater rptAnswer = e.Item.FindControl("rptAnswer") as Repeater;

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
                Answer answer = e.Item.DataItem as Answer;
                Literal litAnswerText = e.Item.FindControl("litAnswerText") as Literal;

                if (litAnswerText != null)
                {
                    if (answer.IsCorrectAnswer)
                        litAnswerText.Text = string.Format("<b style='color: #F00;'>{0}</b>", answer.AnswerText);
                    else
                        litAnswerText.Text = answer.AnswerText;
                }
            }
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Question obj = DomainManager.GetObject<Question>(id);



                if (obj != null && obj.QuestionUserDetailses.Count == 0)
                {
                    DomainManager.Delete(obj);
                    string msg = Page.Server.UrlEncode("Xóa câu hỏi thành công");
                    Page.Response.Redirect(string.Format("/admincp/question-list?msg={0}", msg), true);
                }
            }
        }

        protected void rptRank_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void rptRank_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rptQG_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = 0;
                int.TryParse(e.CommandArgument.ToString(), out id);

                QuestionGame qgame = DomainManager.GetObject<QuestionGame>(id);
                BizQuestionGameSettings biz = TNHelper.GetQuestionGameSettings();
                if (qgame != null)
                {
                    if (biz.QuestionGameID != qgame.Id)
                    {
                        DomainManager.Delete(qgame);
                        Utils.ShowMessage(lblMsgQG, "Xóa bộ đề game trả lời câu hỏi thành công");
                        LoadData();
                    }
                    else
                    {
                        Utils.ShowMessage(lblMsgQG, "Bộ đề này đang được sử dụng trong cấu hình game trả lời câu hỏi. Bạn không thể xóa bộ đề này.");
                    }
                }
            }
        }

        protected void rptQG_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
               e.Item.ItemType == ListItemType.AlternatingItem)
            {
                QuestionGame qgame = e.Item.DataItem as QuestionGame;
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
                if (lnkDelete != null)
                {
                    BizQuestionGameSettings biz = TNHelper.GetQuestionGameSettings();
                    if (biz != null && biz.QuestionGameID == qgame.Id)
                    {
                        lnkDelete.OnClientClick = "aler('Bạn khổng thế xóa bộ đề vì nó đang được cấu hình cho game trả lời câu hỏi'); return false";
                    }
                }
            }
        }
    }
}