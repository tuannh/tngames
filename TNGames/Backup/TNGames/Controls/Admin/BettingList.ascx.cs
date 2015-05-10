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
    public partial class BettingList : System.Web.UI.UserControl
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

                return pagerList;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string msg = Page.Request.QueryString["msg"];
                if (!string.IsNullOrEmpty(msg))
                {
                    msg = Page.Server.UrlDecode(msg);
                    Utils.ShowMessage(lblMsg, msg);
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

            if (IsRanking)
            {
                DataTable dt = new Database().GetDataTable("uxp_GetBettingRank", CommandType.StoredProcedure);
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
                    Utils.ShowMessage(lblMsg2, "Không tìm thấy dữ liệu bảng xếp hạng");
                    rptRank.Visible = false;
                }
            }
            else
            {
                IList<Betting> lst = DomainManager.GetAll<Betting>();
                if (lst != null) { 
                    lst = lst.OrderBy(p => p.PlayDate)
                             .ThenBy(p=>p.BettingName)
                             .ToList();
                }

                int totalRow = 0;
                if (lst != null)
                {
                    totalRow = lst.Count;
                    lst = lst.Skip((PageIndex - 1) * PageSize)
                             .Take(PageSize).ToList();
                }
                pager.ItemCount = totalRow;
                pnlList.Visible = true;

                rptList.DataSource = lst;
                rptList.DataBind();

                if (totalRow == 0)
                {
                    Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu game thử tài phân tích trận đấu");
                    rptList.Visible = false;
                }
            }
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            Betting obj = DomainManager.GetObject<Betting>(id);
            BizBettingGameSettings setting = TNHelper.GetBettingGameSettings();

            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                if ((obj != null && obj.BettingUserses.Count == 0) || setting.AllowDelete)
                {
                    DomainManager.Delete(obj);
                    string msg = Page.Server.UrlEncode("Xóa trận đấu thành công.");
                    Page.Response.Redirect(string.Format("/admincp/betting-list?msg={0}", msg), true);
                }
                else
                {
                    string msg = Page.Server.UrlEncode("Bạn không thể xóa câu hỏi khi có người chơi game này");
                    Page.Response.Redirect(string.Format("/admincp/betting-list?msg={0}", msg), true);
                }
            }
            else if (string.Compare(e.CommandName, "active", true) == 0)
            {
                if (obj != null)
                {
                    obj.Active = !obj.Active;
                    DomainManager.Update(obj);
                    string msg = Page.Server.UrlEncode("Cập nhật trạng thái thành công");
                    Page.Response.Redirect(string.Format("/admincp/betting-list?msg={0}", msg), true);
                }
            }
            else if (string.Compare(e.CommandName, "calculate", true) == 0)
            {
                if (obj != null)
                {
                    TNHelper.UpdateBettingResult(obj.Id);
                    obj.IsCalculate = true;
                    DomainManager.Update(obj);
                    TNHelper.RemoveRankingCaches();

                    string msg = Page.Server.UrlEncode("Tính điểm cho người chơi thành công.");
                    Page.Response.Redirect(string.Format("/admincp/betting-list?msg={0}", msg), true);
                }
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Betting betting = e.Item.DataItem as Betting;
                Literal litUserCount = e.Item.FindControl("litUserCount") as Literal;
                Literal litScoreRate = e.Item.FindControl("litScoreRate") as Literal;

                HyperLink lnkEdit = e.Item.FindControl("lnkEdit") as HyperLink;
                HyperLink lnkScore = e.Item.FindControl("lnkScore") as HyperLink;
                LinkButton lnkActive = e.Item.FindControl("lnkActive") as LinkButton;
                LinkButton lnkPointCal = e.Item.FindControl("lnkPointCal") as LinkButton;
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;

                if (lnkDelete != null)
                    lnkDelete.Visible = !(betting.BettingUserses.Count > 0);

                if (lnkEdit != null)
                {
                    if (betting.BettingUserses.Count > 0)
                        lnkEdit.Text = "Chi tiết";
                }

                if (lnkScore != null)
                    lnkScore.Visible = !betting.IsCalculate;

                if (lnkPointCal != null)
                {
                    lnkPointCal.Visible = false;
                    if (betting.BettingUserses.Count > 0)
                    {
                        if (betting.IsUpdateScore && !betting.IsCalculate)
                            lnkPointCal.Visible = true;
                    }
                }

                if (litScoreRate != null)
                {
                    if (betting.IsUpdateScore)
                        litScoreRate.Text = string.Format("{0} : {1}", betting.HomeGoalScore, betting.VisitingGoalScore);
                    else
                        litScoreRate.Text = "Đang cập nhật";
                }

                if (litUserCount != null)
                    litUserCount.Text = betting.BettingUserses.Count.ToString("N0");


                BizBettingGameSettings setting = TNHelper.GetBettingGameSettings();
                if (setting.AllowDelete)
                {
                    if (lnkDelete != null)
                        lnkDelete.Visible = true;
                }

                if (lnkActive != null)
                {
                    lnkActive.Text = betting.Active ? "Hiển thị" : "Ẩn";
                    lnkActive.ToolTip = betting.Active ? "Click để ẩn trận đấu khỏi trang game phân tích trận đấu" : "Click để hiển thị trận đấu ở trang game phân tích trận đấu?";
                    lnkActive.OnClientClick = betting.Active ? "return confirm('Bạn thực sự muốn ẩn trận đấu này không?')" : "return confirm('Bạn thực sự muốn hiển thị trận đấu này không?')";
                }
            }
        }

        protected void rptRank_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void rptRank_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}