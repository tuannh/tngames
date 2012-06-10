
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region PredictionGame

	/// <summary>
	/// PredictionGame object for NHibernate mapped table 'PredictionGames'.
	/// </summary>
	public class PredictionGame
		{
		#region Member Variables
		
		protected int _id;
		protected string _predictionGameName;
		protected DateTime? _createdDate;
		protected DateTime? _modifiedDate;
		protected bool _active;
		protected bool _isUpdateAnswer;
		protected bool _isCalculate;
		protected User _user;
		protected IList _predictionGameUsers;
		protected IList _predictionses;

		#endregion

		#region Constructors

		public PredictionGame() { }

		public PredictionGame( string predictionGameName, DateTime createdDate, DateTime modifiedDate, bool active, bool isUpdateAnswer, bool isCalculate, User user )
		{
			this._predictionGameName = predictionGameName;
			this._createdDate = createdDate;
			this._modifiedDate = modifiedDate;
			this._active = active;
			this._isUpdateAnswer = isUpdateAnswer;
			this._isCalculate = isCalculate;
			this._user = user;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string PredictionGameName
		{
			get { return _predictionGameName; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for PredictionGameName", value, value.ToString());
				_predictionGameName = value;
			}
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

		public virtual bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public virtual bool IsUpdateAnswer
		{
			get { return _isUpdateAnswer; }
			set { _isUpdateAnswer = value; }
		}

		public virtual bool IsCalculate
		{
			get { return _isCalculate; }
			set { _isCalculate = value; }
		}

		public virtual User User
		{
			get { return _user; }
			set { _user = value; }
		}

		public virtual IList PredictionGameUsers
		{
			get
			{
				if (_predictionGameUsers==null)
				{
					_predictionGameUsers = new ArrayList();
				}
				return _predictionGameUsers;
			}
			set { _predictionGameUsers = value; }
		}

		public virtual IList Predictionses
		{
			get
			{
				if (_predictionses==null)
				{
					_predictionses = new ArrayList();
				}
				return _predictionses;
			}
			set { _predictionses = value; }
		}

       
		#endregion	
	}

	#endregion
}
