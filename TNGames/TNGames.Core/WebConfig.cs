using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web;

namespace TNGames.Core
{
    public static class WebConfig
    {
        public static string NHibernateConfigFile
        {
            get
            {
                string result = WebConfigurationManager.AppSettings["nHibernateConfigFile"];
                if (string.IsNullOrEmpty(result))
                    return string.Empty;

                return HttpContext.Current.Server.MapPath(result);
            }
        }       
    }
}
