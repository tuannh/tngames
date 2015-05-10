using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using TNGames.Core;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_NewsDetail : System.Web.UI.UserControl
    {
        int _item = 5;
        public int DisplayRelateItem
        {
            get { return _item; }
            set { _item = value; }
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
            string tmpId = Page.RouteData.Values["newsid"] as string;
            int id = 0;
            int.TryParse(tmpId, out id);

            New objNew = DomainManager.GetObject<New>(id);
            if (objNew != null)
            {
                litTitle.Text = objNew.NewsTitle;
                litPostDate.Text = objNew.CreatedDate.ToString(TNHelper.DateTimeFormat);
                litSummary.Text = objNew.Summary;
                litNewContent.Text = objNew.NewsContent;

                imgPhoto.Visible = false;
                if (!string.IsNullOrEmpty(objNew.Photo)) { 
                    imgPhoto.ImageUrl = "/Userfiles/News/" + objNew.Photo;
                    imgPhoto.Visible = true;
                }

                List<New> lst = TNHelper.GetRelateNew(DisplayRelateItem, objNew.CreatedDate);
                if (lst != null && lst.Count > 0)
                {
                    rptNews.DataSource = lst;
                    rptNews.DataBind();
                }
            }
        }
    }
}