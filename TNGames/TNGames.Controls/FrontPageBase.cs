using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using TNGames.Core.Helper;
using DM = TNGames.Core.Domain;

namespace TNGames.Controls
{
    public class FrontPageBase : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DM.User user = Utils.GetCurrentUser();
            if (user == null)
            { 
                Page.Response.Redirect("/Login", true);
            }
        }
    }
}
