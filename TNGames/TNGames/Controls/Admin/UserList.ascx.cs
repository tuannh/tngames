using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using TNGames.Core;

namespace TNGames.Controls.Admin
{
    public partial class UserList : System.Web.UI.UserControl
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
            Page.Form.DefaultButton = btnSearch.UniqueID;
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
            List<User> lst = TNHelper.GetAllFrontEndUsers();
            int totalRow = 0;
            if (lst != null)
            {
                totalRow = lst.Count;
                lst = lst.Skip((PageIndex - 1) * PageSize)
                         .Take(PageSize).ToList();
            }
            pager.ItemCount = totalRow;

            if (lst != null)
            {
                rptList.DataSource = lst;
                rptList.DataBind();
            }
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = int.Parse(e.CommandArgument.ToString());
                User obj = DomainManager.GetObject<User>(id);

                if (obj != null)
                {
                    DomainManager.Delete(obj);
                    lblMsg.Text = "Xóa thành công.";
                    LoadData();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string kw = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(kw))
            {
                List<User> lst = TNHelper.FindUser(kw);

                rptList.DataSource = lst;
                rptList.DataBind();

                if (lst != null)
                {
                    string msg = string.Format("<b>{0}</b> người chơi được tìm thấy.", lst.Count);
                    Utils.ShowMessage(lblMsg, msg);
                }
            }
            else
            {
                LoadData();
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                User user = e.Item.DataItem as User;
                Literal litGameBetting = e.Item.FindControl("litGameBetting") as Literal;
                Literal litGamePrediction = e.Item.FindControl("litGamePrediction") as Literal;
                Literal litGameQuestion = e.Item.FindControl("litGameQuestion") as Literal;

                if (litGameBetting != null && user.BettingUserses.Count > 0)
                {
                    litGameBetting.Text = string.Format("<a href='/admincp/user-games/{1}?type=0'>{0}</a><br/>", "Thử tài phân tích", user.Id);
                }

                if (litGamePrediction != null && user.PredictionGameUsers.Count > 0)
                {
                    litGamePrediction.Text = string.Format("<a href='/admincp/user-games/{1}?type=1'>{0}</a><br/>", "Thử tài dự đoán", user.Id);
                }

                if (litGameQuestion != null && user.QuestionUserses.Count > 0)
                {
                    litGameQuestion.Text = string.Format("<a href='/admincp/user-games/{1}?type=2'>{0}</a>", "Thử tài kiến thức", user.Id);
                }
            }

        }

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = currnetPageIndx;
            LoadData();
        }
    }
}