
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
    #region BettingRate

    /// <summary>
    /// BettingRate object for NHibernate mapped table 'BettingRates'.
    /// </summary>
    public partial class BettingRate
    {
        #region Member Variables

        protected int _id;
        protected double _homeRate;
        protected double _visitingRate;
        protected int _order;
        protected Betting _betting;
        protected IList _bettingUserDetailses;

        #endregion

        #region Constructors

        public BettingRate() { }

        public BettingRate(double homeRate, double visitingRate, Betting betting)
        {
            this._homeRate = homeRate;
            this._visitingRate = visitingRate;
            this._betting = betting;
        }

        #endregion

        #region Public Properties

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual double HomeRate
        {
            get { return _homeRate; }
            set { _homeRate = value; }
        }

        public virtual double VisitingRate
        {
            get { return _visitingRate; }
            set { _visitingRate = value; }
        }

        public virtual int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public virtual Betting Betting
        {
            get { return _betting; }
            set { _betting = value; }
        }

        public virtual IList BettingUserDetailses
        {
            get
            {
                if (_bettingUserDetailses == null)
                {
                    _bettingUserDetailses = new ArrayList();
                }
                return _bettingUserDetailses;
            }
            set { _bettingUserDetailses = value; }
        }


        #endregion
    }

    #endregion
}
