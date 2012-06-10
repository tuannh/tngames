
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region BettingUserDetail

	/// <summary>
	/// BettingUserDetail object for NHibernate mapped table 'BettingUserDetails'.
	/// </summary>
	public class BettingUserDetail
		{
		#region Member Variables
		
		protected int _id;
		protected int _bettingPoint;
        protected string _selectedTeam;
		protected BettingUser _bettingUser;
		protected BettingRate _bettingRate;

		#endregion

		#region Constructors

		public BettingUserDetail() { }

		public BettingUserDetail( int bettingPoint, BettingUser bettingUser, BettingRate bettingRate )
		{
			this._bettingPoint = bettingPoint;
			this._bettingUser = bettingUser;
			this._bettingRate = bettingRate;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual int BettingPoint
		{
			get { return _bettingPoint; }
			set { _bettingPoint = value; }
		}

        public virtual string SelectedTeam
        {
            get { return _selectedTeam; }
            set { _selectedTeam = value; }
        }

		public virtual BettingUser BettingUser
		{
			get { return _bettingUser; }
			set { _bettingUser = value; }
		}

		public virtual BettingRate BettingRate
		{
			get { return _bettingRate; }
			set { _bettingRate = value; }
		}

       
		#endregion	
	}

	#endregion
}
