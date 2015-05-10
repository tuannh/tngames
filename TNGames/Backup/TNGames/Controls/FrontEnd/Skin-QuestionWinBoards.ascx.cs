using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using System.Data;
using TNGames.Core;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_QuestionWinBoards : System.Web.UI.UserControl
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

        private void LoadData()
        {
            BizQuestionGameSettings setting = TNHelper.GetQuestionGameSettings();

            List<DataRow> lst = TNHelper.GetTopQuestionGameWinner(setting.MaxDisplayItem);
            rptList.DataSource = lst;
            rptList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int from = 0;
            int.TryParse(txtFrom.Text.Trim(), out from);

            int to = 0;
            int.TryParse(txtTo.Text.Trim(), out to);

            List<DataRow> lst = TNHelper.SearchQuestionGame(from, to);
            int totalRow = 0;
            if (lst != null)
            {
                totalRow = lst.Count;
                lst = lst.Skip((PageIndex - 1) * PageSize)
                         .Take(PageSize).ToList();
            }

            if (lst != null)
            {
                string msg = string.Format("{0} kết quả đựoc tìm thấy", totalRow);
                Utils.ShowMessage(litResult, msg);
            }

            pager.ItemCount = totalRow;
            rptSearch.DataSource = lst;
            rptSearch.DataBind();
        }

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = currnetPageIndx;
            btnSearch_Click(btnSearch, null);
        }
    }
}