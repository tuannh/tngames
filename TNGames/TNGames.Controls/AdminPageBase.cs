using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using TNGames.Core.Helper;
using DM = TNGames.Core.Domain;

namespace TNGames.Controls
{
    public class AdminPageBase : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DM.User user = Utils.GetCurrentUser();
            if (user == null || (user != null && !user.IsAdmin))
            {
                string url = Page.Request.RawUrl;
                Page.Response.Redirect(string.Format("/dang-nhap?returnurl={0}", Page.Server.UrlEncode(url)), true);
            }
        }
    }
}
