using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DM = TNGames.Core.Domain;
using TNGames.Core.Helper;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_NoticeDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            DM.Content content = TNHelper.GetContentObject(id);
            if (content != null)
            {
                litTitle.Text = content.ContentTitle;
                litContent.Text = content.ContentText;

                if (content.ContentType != null)
                {
                    List<DM.Content> lst = TNHelper.GetContentsByType(content.ContentType.Id);
                    if (lst != null && lst.Count > 0)
                    {
                        lst = lst.Where(p => p.Active && p.Id < content.Id)
                                 .OrderByDescending(p => p.Id)
                                 .Take(10)
                                 .ToList();
                    }

                    if (lst != null && lst.Count > 0)
                    {
                        rptList.DataSource = lst;
                        rptList.DataBind();
                    }
                }
            }
            else
            {
                Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu yêu cầu");
            }
        }
    }
}