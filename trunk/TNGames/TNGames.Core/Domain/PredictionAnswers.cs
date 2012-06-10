
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region PredictionAnswer

	/// <summary>
	/// PredictionAnswer object for NHibernate mapped table 'PredictionAnswers'.
	/// </summary>
	public partial class PredictionAnswer
		{
		#region Member Variables
		
		protected int _id;
		protected string _answerText;
		protected bool _isCorrectAnswer;
		protected Prediction _prediction;
		protected IList _predictionGameUserDetailses;

		#endregion

		#region Constructors

		public PredictionAnswer() { }

		public PredictionAnswer( string answerText, bool isCorrectAnswer, Prediction prediction )
		{
			this._answerText = answerText;
			this._isCorrectAnswer = isCorrectAnswer;
			this._prediction = prediction;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string AnswerText
		{
			get { return _answerText; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for AnswerText", value, value.ToString());
				_answerText = value;
			}
		}

		public virtual bool IsCorrectAnswer
		{
			get { return _isCorrectAnswer; }
			set { _isCorrectAnswer = value; }
		}

		public virtual Prediction Prediction
		{
			get { return _prediction; }
			set { _prediction = value; }
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
