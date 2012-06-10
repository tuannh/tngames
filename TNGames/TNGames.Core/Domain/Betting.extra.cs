
using System;
using System.Collections;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;

namespace TNGames.Core.Domain
{
    #region Betting

    /// <summary>
    /// Betting object for NHibernate mapped table 'Betting'.
    /// </summary>
    public partial class Betting
    {
        public virtual bool IsBettedByCurrentUser()
        {
            User user = Utils.GetCurrentUser();
            if (user != null)
            {
                foreach (BettingUser bu in BettingUserses)
                {
                    if (bu.User != null && bu.User.Id == user.Id)
                        return true;
                }
            }

            return false;
        }
    }

    #endregion
}
