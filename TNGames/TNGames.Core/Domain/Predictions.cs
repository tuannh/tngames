
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region Prediction

	/// <summary>
	/// Prediction object for NHibernate mapped table 'Predictions'.
	/// </summary>
	public class Prediction
		{
		#region Member Variables
		
		protected int _id;
		protected string _predictionName;
		protected bool _active;
		protected int _bonusPoint;
		protected int _order;
		protected DateTime? _createdDate;
		protected DateTime? _modifiedDate;
		protected PredictionGame _predictionGame;
		protected IList _predictionGameUserDetailses;
		protected IList _predictionAnswerses;

		#endregion

		#region Constructors

		public Prediction() { }

		public Prediction( string predictionName, bool active, int bonusPoint, int order, DateTime createdDate, DateTime modifiedDate, PredictionGame predictionGame )
		{
			this._predictionName = predictionName;
			this._active = active;
			this._bonusPoint = bonusPoint;
			this._order = order;
			this._createdDate = createdDate;
			this._modifiedDate = modifiedDate;
			this._predictionGame = predictionGame;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string PredictionName
		{
			get { return _predictionName; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for PredictionName", value, value.ToString());
				_predictionName = value;
			}
		}

		public virtual bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public virtual int BonusPoint
		{
			get { return _bonusPoint; }
			set { _bonusPoint = value; }
		}

		public virtual int Order
		{
			get { return _order; }
			set { _order = value; }
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

		public virtual IList PredictionAnswerses
		{
			get
			{
				if (_predictionAnswerses==null)
				{
					_predictionAnswerses = new ArrayList();
				}
				return _predictionAnswerses;
			}
			set { _predictionAnswerses = value; }
		}

       
		#endregion	
	}

	#endregion
}
