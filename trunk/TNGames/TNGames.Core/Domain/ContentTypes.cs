
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
    #region ContentType

    /// <summary>
    /// ContentType object for NHibernate mapped table 'ContentTypes'.
    /// </summary>
    public class ContentType
    {
        #region Member Variables

        protected int _id;
        protected string _contentTypeName;
        protected bool _isbanner;
        protected IList _contentses;

        #endregion

        #region Constructors

        public ContentType() { }

        public ContentType(string contentTypeName)
        {
            this._contentTypeName = contentTypeName;
        }

        #endregion

        #region Public Properties

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string ContentTypeName
        {
            get { return _contentTypeName; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for ContentTypeName", value, value.ToString());
                _contentTypeName = value;
            }
        }

        public virtual bool IsBanner
        {
            get { return _isbanner; }
            set
            {
                _isbanner = value;
            }
        }

        public virtual IList Contentses
        {
            get
            {
                if (_contentses == null)
                {
                    _contentses = new ArrayList();
                }
                return _contentses;
            }
            set { _contentses = value; }
        }


        #endregion
    }

    #endregion
}
