
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region Setting

	/// <summary>
	/// Setting object for NHibernate mapped table 'Settings'.
	/// </summary>
	public class Setting
		{
		#region Member Variables
		
		protected int _id;
		protected string _settingValue;

		#endregion

		#region Constructors

		public Setting() { }

		public Setting( string settingValue )
		{
			this._settingValue = settingValue;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string SettingValue
		{
			get { return _settingValue; }
			set
			{
				_settingValue = value;
			}
		}

       
		#endregion	
	}

	#endregion
}
