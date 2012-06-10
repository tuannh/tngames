
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
    #region Content

    /// <summary>
    /// Content object for NHibernate mapped table 'Contents'.
    /// </summary>
    public partial class Content
    {
        #region Member Variables

        protected int _id;
        protected string _contentText;
        protected bool _active;
        protected string _contentTitle;
        protected ContentType _contentType;

        #endregion

        #region Constructors

        public Content() { }

        public Content(string contentText, ContentType contentType)
        {
            this._contentText = contentText;
            this._contentType = contentType;
        }

        #endregion

        #region Public Properties

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string ContentText
        {
            get { return _contentText; }
            set
            {
                _contentText = value;
            }
        }

        public virtual ContentType ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public virtual bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public virtual string ContentTitle
        {
            get { return _contentTitle; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ContentTitle", value, value.ToString());
                _contentTitle = value;
            }
        }

        #endregion
    }

    #endregion
}
