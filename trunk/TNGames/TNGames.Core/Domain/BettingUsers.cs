
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region BettingUser

	/// <summary>
	/// BettingUser object for NHibernate mapped table 'BettingUsers'.
	/// </summary>
	public class BettingUser
		{
		#region Member Variables
		
		protected int _id;
		protected int _winPoint;
        protected int _time;
		protected DateTime _bettingDate;
		protected Betting _betting;
		protected User _user;
		protected IList _bettingUserDetailses;

		#endregion

		#region Constructors

		public BettingUser() { }

		public BettingUser( int winPoint, DateTime bettingDate, Betting betting, User user )
		{
			this._winPoint = winPoint;
			this._bettingDate = bettingDate;
			this._betting = betting;
			this._user = user;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual int WinPoint
		{
			get { return _winPoint; }
			set { _winPoint = value; }
		}

        public virtual int Time
        {
            get { return _time; }
            set { _time = value; }
        }

		public virtual DateTime BettingDate
		{
			get { return _bettingDate; }
			set { _bettingDate = value; }
		}

		public virtual Betting Betting
		{
			get { return _betting; }
			set { _betting = value; }
		}

		public virtual User User
		{
			get { return _user; }
			set { _user = value; }
		}

		public virtual IList BettingUserDetailses
		{
			get
			{
				if (_bettingUserDetailses==null)
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
