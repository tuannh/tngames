using System;
using System.Web;
using System.Web.SessionState;
using TNGames.Core.Domain;
using TNGames.Core.Helper;

namespace TNGames.Controls
{
    public class Check : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            int p = 0;
            int.TryParse(context.Request.QueryString["p"], out p);
            string key = context.Request.QueryString["key"] ?? "point";
            User user = Utils.GetCurrentUser();

            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.ContentType = "text/plain";

            if (string.Compare(key, "point", true) == 0)
            {
                if (user != null)
                {
                    string str = string.Format("{0};{1}", user.TotalPoint.ToString("N0"), user.Point.ToString("N0"));
                    context.Response.Write(str);
                }
            }
            else if (string.Compare(key, "betting", true) == 0)
            {
                if (user != null && user.Point >= p)
                    context.Response.Write("1");
                else
                    context.Response.Write("0");

            }

            context.Response.End();
        }

        #endregion
    }
}
