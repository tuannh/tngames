using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;
using TNGames.Core.Domain;
using TNGames.Core;
using System.Web.Routing;

namespace TNGames.Controls.FrontEnd
{
    public partial class ActiveAccount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string activeCode = Page.RouteData.Values["ActiveCode"] as string;
            if (!string.IsNullOrEmpty(activeCode))
            {
                User user = TNHelper.GetUserByActiveCode(activeCode);
                if (user != null)
                {
                    user.Active = true;
                    user.ActiveCode = string.Empty;
                    DomainManager.Update(user);
                    Utils.ShowMessage(lblMsg, "Tài khoản của bạn được kích hoạt thành công.\nBạn có thể đăng nhập tài khỏan của bạn <a href='/dang-nhap'>tại đây</a>");
                    TNHelper.LogAction(LogType.UserLog, "Kích hoạt tài khoản thành công");
                }
                else
                    lblMsg.Text = "Mã kích hoạt không đúng. Bạn hãy liên hệ với quản trị website để được trợ giúp.";

            }
        }
    }
}