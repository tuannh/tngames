
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region PredictionGameUser

	/// <summary>
	/// PredictionGameUser object for NHibernate mapped table 'PredictionGameUser'.
	/// </summary>
	public class PredictionGameUser
		{
		#region Member Variables
		
		protected int _id;
		protected int _winPoint;
		protected int _time;
		protected DateTime _playDate;
		protected User _user;
		protected PredictionGame _predictionGame;
		protected IList _predictionGameUserDetailses;

		#endregion

		#region Constructors

		public PredictionGameUser() { }

		public PredictionGameUser( int winPoint, int time, DateTime playDate, User user, PredictionGame predictionGame )
		{
			this._winPoint = winPoint;
			this._time = time;
			this._playDate = playDate;
			this._user = user;
			this._predictionGame = predictionGame;
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

		public virtual DateTime PlayDate
		{
			get { return _playDate; }
			set { _playDate = value; }
		}

		public virtual User User
		{
			get { return _user; }
			set { _user = value; }
		}

		public virtual PredictionGame PredictionGame
		{
			get { return _predictionGame; }
			set { _predictionGame = value; }
		}

		public virtual IList PredictionGameUserDetailses
		{
			get
			{
				if (_predictionGameUserDetailses==null)
				{
					_predictionGameUserDetailses = new ArrayList();
				}
				return _predictionGameUserDetailses;
			}
			set { _predictionGameUserDetailses = value; }
		}

       
		#endregion	
	}

	#endregion
}
