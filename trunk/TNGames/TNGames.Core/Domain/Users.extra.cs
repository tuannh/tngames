
using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using TNGames.Core.Helper;

namespace TNGames.Core.Domain
{
    #region User

    public partial class User
    {

        public virtual int Rank
        {
            get
            {
                return TNHelper.GetUserRank(Id);
            }
        }

        public virtual int TotalPoint
        {
            get
            {
                return Point + PointQuestion + PointPrediction;
            }
        }
    }

    #endregion
}
