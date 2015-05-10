using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using TNGames.Core.Domain;
using TNGames.Core.Helper;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_News : System.Web.UI.UserControl
    {
        private int item = 3;
        public int DisplayItem
        {
            get { return item; }
            set { item = value; }
        }

        public bool IsNewsList
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNews();
            }
        }

        public void BindNews()
        {
            pnlNewsTop.Visible = false;
            pnlNewsList.Visible = false;

            List<New> lst = TNHelper.GetTopNews(DisplayItem);
            if (lst != null && lst.Count > 0)
            {
                if (!IsNewsList)
                {
                    rptNews.DataSource = lst;
                    rptNews.DataBind();
                    pnlNewsTop.Visible = true;
                }
                else
                {
                    rptNewsList.DataSource = lst;
                    rptNewsList.DataBind();
                    pnlNewsList.Visible = true;
                }
                
            }
            else
            {
                litMsg.Visible = true;
            }
        }

        protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (IsNewsList)
                {
                    New news = e.Item.DataItem as New;
                    Image imgPhoto = e.Item.FindControl("imgPhoto") as Image;
                    Literal litDesc = e.Item.FindControl("litDesc") as Literal;

                    if (imgPhoto != null)
                    {
                        if (!string.IsNullOrEmpty(news.Photo))
                            imgPhoto.ImageUrl = string.Format("/Userfiles/News/{0}", news.Photo);
                        else
                            imgPhoto.Visible = false;
                    }

                    if (litDesc != null)
                        litDesc.Text = news.Summary;
                }
            }
        }
    }
}