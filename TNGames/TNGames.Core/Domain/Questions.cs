
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region Question

	/// <summary>
	/// Question object for NHibernate mapped table 'Questions'.
	/// </summary>
	public partial class Question
		{
		#region Member Variables
		
		protected int _id;
		protected string _questionName;
		protected bool _active;
		protected int _order;
		protected int _bonusPoint;
		protected DateTime? _createdDate;
		protected DateTime? _modifiedDate;
		protected QuestionGame _questionGame;
		protected IList _answerses;
		protected IList _questionUserDetailses;

		#endregion

		#region Constructors

		public Question() { }

		public Question( string questionName, bool active, int order, int bonusPoint, DateTime createdDate, DateTime modifiedDate, QuestionGame questionGame )
		{
			this._questionName = questionName;
			this._active = active;
			this._order = order;
			this._bonusPoint = bonusPoint;
			this._createdDate = createdDate;
			this._modifiedDate = modifiedDate;
			this._questionGame = questionGame;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string QuestionName
		{
			get { return _questionName; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for QuestionName", value, value.ToString());
				_questionName = value;
			}
		}

		public virtual bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public virtual int Order
		{
			get { return _order; }
			set { _order = value; }
		}

		public virtual int BonusPoint
		{
			get { return _bonusPoint; }
			set { _bonusPoint = value; }
		}

		public virtual DateTime? CreatedDate
		{
			get { return _createdDate; }
			set { _createdDate = value; }
		}

		public virtual DateTime? ModifiedDate
		{
			get { return _modifiedDate; }
			set { _modifiedDate = value; }
		}

		public virtual QuestionGame QuestionGame
		{
			get { return _questionGame; }
			set { _questionGame = value; }
		}

		public virtual IList Answerses
		{
			get
			{
				if (_answerses==null)
				{
					_answerses = new ArrayList();
				}
				return _answerses;
			}
			set { _answerses = value; }
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
