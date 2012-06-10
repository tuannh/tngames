
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region Answer

	/// <summary>
	/// Answer object for NHibernate mapped table 'Answers'.
	/// </summary>
	public partial class Answer
		{
		#region Member Variables
		
		protected int _id;
		protected string _answerText;
		protected bool _isCorrectAnswer;
		protected Question _question;
		protected IList _questionUserDetailses;

		#endregion

		#region Constructors

		public Answer() { }

		public Answer( string answerText, bool isCorrectAnswer, Question question )
		{
			this._answerText = answerText;
			this._isCorrectAnswer = isCorrectAnswer;
			this._question = question;
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

		public virtual Question Question
		{
			get { return _question; }
			set { _question = value; }
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
