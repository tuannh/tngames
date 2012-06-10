using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TNGames.Controls;

namespace TNGames.AdminCP
{
    public partial class Default : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string controlName = RouteData.Values["ControlName"] as string;
            controlName = string.IsNullOrEmpty(controlName) ? "DashBoard" : controlName;
            string path = string.Format("/Controls/Admin/{0}.ascx", controlName.Replace("-", ""));

            if (File.Exists(Page.Server.MapPath(path)))
            {
                Control ctrl = Page.LoadControl(path);
                if (ctrl != null)
                    holderContent.Controls.Add(ctrl);
            }
        }
    }
}