using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;
using TNGames.Core;
using TNGames.Core.Domain;
using System.Data;

namespace TNGames.Controls.Admin
{
    public partial class PredictionList : System.Web.UI.UserControl
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
                DataTable dt = new Database().GetDataTable("uxp_GetPredictionRank", CommandType.StoredProcedure);
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
                    Utils.ShowMessage(lblMsg2, "Không tìm thấy dữ liệu bảng xếp hạng game dự đoán");
                    rptRank.Visible = false;
                }
            }
            else if (IsDetail)
            {
                string strId = Page.RouteData.Values["id"] as string;
                int id = 0;
                int.TryParse(strId, out id);
                PredictionGame qgame = DomainManager.GetObject<PredictionGame>(id);
                List<Prediction> lst = new List<Prediction>();
                int totalRow = 0;

                if (qgame != null)
                {
                    litQGName.Text = qgame.PredictionGameName;
                    if (qgame.Predictionses != null)
                    {
                        totalRow = qgame.Predictionses.Count;
                        lst = qgame.Predictionses.Cast<Prediction>()
                                   .Skip((PageIndex - 1) * PageSize)
                                   .Take(PageSize)
                                   .ToList();
                    }
                }

                rptList.DataSource = lst;
                rptList.DataBind();

                pager.ItemCount = totalRow;
                pnlList.Visible = true;

                if (totalRow == 0)
                {
                    Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu câu hỏi của game dự đoán");
                    rptList.Visible = false;
                }
            }
            else
            {
                IList<PredictionGame> lst = DomainManager.GetAll<PredictionGame>();
                if (lst != null)
                    lst = lst.OrderBy(p => p.PredictionGameName)
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
                    Utils.ShowMessage(lblMsg, "Không tìm thấy dữ bộ đề game dự đoán");
                    rptList.Visible = false;
                }
            }
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Prediction obj = DomainManager.GetObject<Prediction>(id);

                if (obj != null && obj.PredictionGameUserDetailses.Count == 0)
                {
                    DomainManager.Delete(obj);
                    string msg = Page.Server.UrlEncode("Xóa dự đoán thành công");
                    Page.Response.Redirect(string.Format("/admincp/prediction-list?msg={0}", msg), true);
                }
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Prediction prediction = e.Item.DataItem as Prediction;
                Repeater rptAnswer = e.Item.FindControl("rptAnswer") as Repeater;

                if (rptAnswer != null)
                {
                    rptAnswer.DataSource = prediction.PredictionAnswerses;
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
                PredictionAnswer answer = e.Item.DataItem as PredictionAnswer;
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


        protected void rptQG_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = 0;
                int.TryParse(e.CommandArgument.ToString(), out id);

                PredictionGame qgame = DomainManager.GetObject<PredictionGame>(id);
                BizPredictionGameSettings biz = TNHelper.GetPredictionGameSettings();
                if (qgame != null)
                {
                    if (biz.PredictionGameID != qgame.Id)
                    {
                        DomainManager.Delete(qgame);
                        Utils.ShowMessage(lblMsgQG, "Xóa bộ đề dự đoán thành công");
                        LoadData();
                    }
                    else
                    {
                        Utils.ShowMessage(lblMsgQG, "Bộ đề này đang được sử dụng trong cấu hình game dự đoán. Bạn không thể xóa.");
                    }
                }
            }
            else if (string.Compare(e.CommandName, "calculate", true) == 0)
            {
                int id = 0;
                int.TryParse(e.CommandArgument.ToString(), out id);

                PredictionGame qgame = DomainManager.GetObject<PredictionGame>(id);
                BizPredictionGameSettings biz = TNHelper.GetPredictionGameSettings();
                if (qgame != null)// && qgame.IsUpdateAnswer)
                {
                    TNHelper.CalculatePredcitionGame(qgame);
                    TNHelper.RemoveRankingCaches();
                    Utils.ShowMessage(lblMsgQG, "Tính điểm cho người chơi thành chơi thành công");
                    LoadData();
                }
                else
                {
                    Utils.ShowMessage(lblMsgQG, "Bạn chưa cập nhật trả lời cho tất cả câu hỏi dự đoán. Hãy cập nhật tất cả câu trả lời trước khi tính điểm");
                }
            }
        }

        protected void rptQG_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
               e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PredictionGame qgame = e.Item.DataItem as PredictionGame;
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
                LinkButton lnkCalculate = e.Item.FindControl("lnkCalculate") as LinkButton;

                BizPredictionGameSettings biz = TNHelper.GetPredictionGameSettings();
                if (lnkDelete != null)
                {
                    if (biz != null && biz.PredictionGameID == qgame.Id)
                    {
                        lnkDelete.OnClientClick = "aler('Bạn khổng thế xóa bộ đề vì nó đang được cấu hình cho game dự đoán'); return false";
                    }
                }

                if (lnkCalculate != null)
                {
                    lnkCalculate.Visible = false;
                    if (biz != null && !qgame.IsCalculate)
                        lnkCalculate.Visible = true;
                }
            }
        }
    }
}