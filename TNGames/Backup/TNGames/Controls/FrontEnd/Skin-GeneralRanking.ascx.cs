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
    public partial class Skin_GeneralRanking : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            BizSettings biz = TNHelper.GetSettings();
            int count = new BizSettings().HomeDisplayItem;
            if (biz != null)
                count = biz.HomeDisplayItem;

            List<User> lst = TNHelper.GetTopRankingUser(count);
            if (lst != null && lst.Count > 0)
            {
                rptList.DataSource = lst;
                rptList.DataBind();
            }
            else
            {
                rptList.Visible = false;
                Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu");
            }
        }
    }
}