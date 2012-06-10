
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region QuestionUser

	/// <summary>
	/// QuestionUser object for NHibernate mapped table 'QuestionUsers'.
	/// </summary>
	public class QuestionUser
		{
		#region Member Variables
		
		protected int _id;
		protected int _winPoint;
		protected int _time;
		protected DateTime _playDate;
		protected User _user;
		protected QuestionGame _questionGame;
		protected IList _questionUserDetailses;

		#endregion

		#region Constructors

		public QuestionUser() { }

		public QuestionUser( int winPoint, int time, DateTime playDate, User user, QuestionGame questionGame )
		{
			this._winPoint = winPoint;
			this._time = time;
			this._playDate = playDate;
			this._user = user;
			this._questionGame = questionGame;
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

		public virtual QuestionGame QuestionGame
		{
			get { return _questionGame; }
			set { _questionGame = value; }
		}

		public virtual IList QuestionUserDetailses
		{
			get
			{
				if (_questionUserDetailses==null)
				{
					_questionUserDetailses = new ArrayList();
				}
				return _questionUserDetailses;
			}
			set { _questionUserDetailses = value; }
		}

       
		#endregion	
	}

	#endregion
}
