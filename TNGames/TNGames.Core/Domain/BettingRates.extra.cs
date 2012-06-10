
using System;
using System.Collections;
using System.Web.UI.WebControls;
using TNGames.Core.Helper;

namespace TNGames.Core.Domain
{
    #region BettingRate

    /// <summary>
    /// BettingRate object for NHibernate mapped table 'BettingRates'.
    /// </summary>
    public partial class BettingRate
    {
        public virtual string HomeRateText
        {
            get
            {
                int n = TNHelper.GetIntValue(HomeRate);
                double m = TNHelper.GetFloatValue(HomeRate);

                if (n == 0 && m == 0)
                    return "0";
                else if (n > 0 && m == 0)
                    return n.ToString();
                else if (n == 0 && m > 0)
                {
                    switch (m.ToString())
                    {
                        case "0.25":
                            return "1/4";

                        case "0.5":
                            return "1/2";

                        case "0.75":
                            return "3/4";

                        default:
                            return "0";
                    }
                }
                else
                {
                    switch (m.ToString())
                    {
                        case "0.25":
                            return string.Format("{0} {1}", n, "1/4");

                        case "0.5":
                            return string.Format("{0} {1}", n, "1/2");

                        case "0.75":
                            return string.Format("{0} {1}", n, "3/4");

                        default:
                            return n.ToString();
                    }
                }
            }
        }

        public virtual string VisitingRateText
        {
            get
            {
                int n = TNHelper.GetIntValue(VisitingRate);
                double m = TNHelper.GetFloatValue(VisitingRate);

                if (n == 0 && m == 0)
                    return "0";
                else if (n > 0 && m == 0)
                    return n.ToString();
                else if (n == 0 && m > 0)
                {
                    switch (m.ToString())
                    {
                        case "0.25":
                            return "1/4";

                        case "0.5":
                            return "1/2";

                        case "0.75":
                            return "3/4";

                        default:
                            return "0";
                    }
                }
                else
                {
                    switch (m.ToString())
                    {
                        case "0.25":
                            return string.Format("{0} {1}", n, "1/4");

                        case "0.5":
                            return string.Format("{0} {1}", n, "1/2");

                        case "0.75":
                            return string.Format("{0} {1}", n, "3/4");

                        default:
                            return n.ToString();
                    }
                }
            }
        }
    }

    #endregion
}
