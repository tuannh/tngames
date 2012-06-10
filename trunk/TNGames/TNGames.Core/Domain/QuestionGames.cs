
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region QuestionGame

	/// <summary>
	/// QuestionGame object for NHibernate mapped table 'QuestionGames'.
	/// </summary>
	public class QuestionGame
		{
		#region Member Variables
		
		protected int _id;
		protected string _questionGameName;
		protected DateTime? _createdDate;
		protected bool _active;
		protected User _user;
		protected IList _questionUserses;
		protected IList _questionses;

		#endregion

		#region Constructors

		public QuestionGame() { }

		public QuestionGame( string questionGameName, DateTime createdDate, bool active, User user )
		{
			this._questionGameName = questionGameName;
			this._createdDate = createdDate;
			this._active = active;
			this._user = user;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string QuestionGameName
		{
			get { return _questionGameName; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for QuestionGameName", value, value.ToString());
				_questionGameName = value;
			}
		}

		public virtual DateTime? CreatedDate
		{
			get { return _createdDate; }
			set { _createdDate = value; }
		}

		public virtual bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public virtual User User
		{
			get { return _user; }
			set { _user = value; }
		}

		public virtual IList QuestionUserses
		{
			get
			{
				if (_questionUserses==null)
				{
					_questionUserses = new ArrayList();
				}
				return _questionUserses;
			}
			set { _questionUserses = value; }
		}

		public virtual IList Questionses
		{
			get
			{
				if (_questionses==null)
				{
					_questionses = new ArrayList();
				}
				return _questionses;
			}
			set { _questionses = value; }
		}

       
		#endregion	
	}

	#endregion
}
