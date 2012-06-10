
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region QuestionUserDetail

	/// <summary>
	/// QuestionUserDetail object for NHibernate mapped table 'QuestionUserDetails'.
	/// </summary>
	public class QuestionUserDetail
		{
		#region Member Variables
		
		protected int _id;
		protected QuestionUser _questionUser;
		protected Question _question;
		protected Answer _answer;

		#endregion

		#region Constructors

		public QuestionUserDetail() { }

		public QuestionUserDetail( QuestionUser questionUser, Question question, Answer answer )
		{
			this._questionUser = questionUser;
			this._question = question;
			this._answer = answer;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual QuestionUser QuestionUser
		{
			get { return _questionUser; }
			set { _questionUser = value; }
		}

		public virtual Question Question
		{
			get { return _question; }
			set { _question = value; }
		}

		public virtual Answer Answer
		{
			get { return _answer; }
			set { _answer = value; }
		}

       
		#endregion	
	}

	#endregion
}
