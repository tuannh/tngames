
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region PredictionGameUserDetail

	/// <summary>
	/// PredictionGameUserDetail object for NHibernate mapped table 'PredictionGameUserDetails'.
	/// </summary>
	public class PredictionGameUserDetail
		{
		#region Member Variables
		
		protected int _id;
		protected PredictionGameUser _predictionGameUser;
		protected Prediction _prediction;
		protected PredictionAnswer _predictionAnswer;

		#endregion

		#region Constructors

		public PredictionGameUserDetail() { }

		public PredictionGameUserDetail( PredictionGameUser predictionGameUser, Prediction prediction, PredictionAnswer predictionAnswer )
		{
			this._predictionGameUser = predictionGameUser;
			this._prediction = prediction;
			this._predictionAnswer = predictionAnswer;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual PredictionGameUser PredictionGameUser
		{
			get { return _predictionGameUser; }
			set { _predictionGameUser = value; }
		}

		public virtual Prediction Prediction
		{
			get { return _prediction; }
			set { _prediction = value; }
		}

		public virtual PredictionAnswer PredictionAnswer
		{
			get { return _predictionAnswer; }
			set { _predictionAnswer = value; }
		}

       
		#endregion	
	}

	#endregion
}
