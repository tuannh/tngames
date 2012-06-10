
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region Category

	/// <summary>
	/// Category object for NHibernate mapped table 'Categories'.
	/// </summary>
	public class Category
		{
		#region Member Variables
		
		protected int _id;
		protected string _categoryAlias;
		protected string _categoryName;
		protected string _description;
		protected IList _newses;

		#endregion

		#region Constructors

		public Category() { }

		public Category( string categoryAlias, string categoryName, string description )
		{
			this._categoryAlias = categoryAlias;
			this._categoryName = categoryName;
			this._description = description;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string CategoryAlias
		{
			get { return _categoryAlias; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for CategoryAlias", value, value.ToString());
				_categoryAlias = value;
			}
		}

		public virtual string CategoryName
		{
			get { return _categoryName; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for CategoryName", value, value.ToString());
				_categoryName = value;
			}
		}

		public virtual string Description
		{
			get { return _description; }
			set
			{
				_description = value;
			}
		}

		public virtual IList Newses
		{
			get
			{
				if (_newses==null)
				{
					_newses = new ArrayList();
				}
				return _newses;
			}
			set { _newses = value; }
		}

       
		#endregion	
	}

	#endregion
}
