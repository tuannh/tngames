
using System;
using System.Collections;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;

namespace TNGames.Core.Domain
{
    public partial class Content
    {
        public virtual string ContentAlias
        {
            get
            {
                if (!string.IsNullOrEmpty(ContentTitle))
                {
                    string tmp = Utils.GenerateAlias(ContentTitle);
                    return Utils.TrimLeftText(tmp, 150, "");                    
                }

                return "no-alias";
            }
        }
    }
}
