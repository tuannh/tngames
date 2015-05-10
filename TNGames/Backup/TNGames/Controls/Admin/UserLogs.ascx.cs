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
    public partial class UserLogs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRepeater();
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

        protected void BindRepeater()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);
            litUserId.Text = id.ToString();

            List<UserLog> lst = TNHelper.GetUserLogs(id);
            int totalRow = 0;
            if (lst != null)
            {
                totalRow = lst.Count;
                lst = lst.Skip((PageIndex - 1) * PageSize)
                         .Take(PageSize).ToList();
            }

            rptList.DataSource = lst;
            rptList.DataBind();

            if (lst != null)
                pager.ItemCount = totalRow;
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litLogType = e.Item.FindControl("litLogType") as Literal;
                UserLog ul = e.Item.DataItem as UserLog;
                if (litLogType != null)
                {
                    if (ul.LogType == (int)LogType.BettingLog)
                        litLogType.Text = "Trò chơi phân tích trận đấu";
                    else if (ul.LogType == (int)LogType.PredictionLog)
                        litLogType.Text = "Trò chơi thử tài dự đoán";
                    else if (ul.LogType == (int)LogType.QuestionLog)
                        litLogType.Text = "Trò chơi thử tài kiến thức";
                    else
                        litLogType.Text = "Thông tin người chơi";
                }
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
                    index =  index > 0 ? index : 1;
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

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = currnetPageIndx;
            BindRepeater();
        }
    }
}