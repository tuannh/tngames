
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region UserLog

	/// <summary>
	/// UserLog object for NHibernate mapped table 'UserLogs'.
	/// </summary>
	public class UserLog
		{
		#region Member Variables
		
		protected int _id;
		protected string _logAction;
		protected DateTime _logDate;
		protected int _logType;
		protected User _user;

		#endregion

		#region Constructors

		public UserLog() { }

		public UserLog( string logAction, DateTime logDate, int logType, User user )
		{
			this._logAction = logAction;
			this._logDate = logDate;
			this._logType = logType;
			this._user = user;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string LogAction
		{
			get { return _logAction; }
			set
			{
				_logAction = value;
			}
		}

		public virtual DateTime LogDate
		{
			get { return _logDate; }
			set { _logDate = value; }
		}

		public virtual int LogType
		{
			get { return _logType; }
			set { _logType = value; }
		}

		public virtual User User
		{
			get { return _user; }
			set { _user = value; }
		}

       
		#endregion	
	}

	#endregion
}
