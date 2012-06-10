
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
	#region New

	/// <summary>
	/// New object for NHibernate mapped table 'News'.
	/// </summary>
	public class New
		{
		#region Member Variables
		
		protected int _id;
		protected string _newsTitle;
		protected string _newsAlias;
		protected string _summary;
		protected string _newsContent;
		protected string _photo;
        protected bool _active;
		protected DateTime _createdDate;
		protected DateTime _modifedDate;
		protected Category _category;

		#endregion

		#region Constructors

		public New() { }

		public New( string newsTitle, string newsAlias, string summary, string newsContent, string photo, DateTime createdDate, DateTime modifedDate, Category category )
		{
			this._newsTitle = newsTitle;
			this._newsAlias = newsAlias;
			this._summary = summary;
			this._newsContent = newsContent;
			this._photo = photo;
			this._createdDate = createdDate;
			this._modifedDate = modifedDate;
			this._category = category;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string NewsTitle
		{
			get { return _newsTitle; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for NewsTitle", value, value.ToString());
				_newsTitle = value;
			}
		}

		public virtual string NewsAlias
		{
			get { return _newsAlias; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for NewsAlias", value, value.ToString());
				_newsAlias = value;
			}
		}

		public virtual string Summary
		{
			get { return _summary; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Summary", value, value.ToString());
				_summary = value;
			}
		}

		public virtual string NewsContent
		{
			get { return _newsContent; }
			set
			{
				_newsContent = value;
			}
		}

		public virtual string Photo
		{
			get { return _photo; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for Photo", value, value.ToString());
				_photo = value;
			}
		}

        public virtual bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

		public virtual DateTime CreatedDate
		{
			get { return _createdDate; }
			set { _createdDate = value; }
		}

		public virtual DateTime ModifedDate
		{
			get { return _modifedDate; }
			set { _modifedDate = value; }
		}

		public virtual Category Category
		{
			get { return _category; }
			set { _category = value; }
		}

       
		#endregion	
	}

	#endregion
}
