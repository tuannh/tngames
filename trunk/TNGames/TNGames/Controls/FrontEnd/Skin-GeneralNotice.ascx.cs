using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;
using DM = TNGames.Core.Domain;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_GeneralNotice : System.Web.UI.UserControl
    {
        public int ContentTypeId
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            List<DM.Content> lst = TNHelper.GetContentsByType(ContentTypeId);
            if (lst != null)
            {
                lst = lst.Where(p => p.Active)
                         .OrderByDescending(p => p.Id)
                         .ToList();
            }

            if (lst != null && lst.Count > 0)
            {
                rptList.DataSource = lst;
                rptList.DataBind();
            }
            else
            {
                Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu");
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DM.Content content = e.Item.DataItem as DM.Content;
                Literal litContent = e.Item.FindControl("litContent") as Literal;
                if (litContent != null)
                {
                    if (content.ContentType != null && content.ContentType.IsBanner)
                    {
                        litContent.Text = content.ContentText;
                    }
                    else
                    {
                        litContent.Text = string.Format("<a class='anotice' href='/thong-bao/{0}/{1}' title='{2}'>{2}</a>", content.Id, content.ContentAlias, content.ContentTitle);
                    }
                }
            }
        }
    }
}