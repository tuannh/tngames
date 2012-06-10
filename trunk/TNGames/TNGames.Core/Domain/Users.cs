
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
    #region User

    /// <summary>
    /// User object for NHibernate mapped table 'Users'.
    /// </summary>
    public partial class User
    {
        #region Member Variables

        protected int _id;
        protected string _displayName;
        protected string _password;
        protected string _fullName;
        protected string _email;
        protected string _iDNumber;
        protected DateTime? _birthday;
        protected string _phone;
        protected string _address;
        protected string _province;
        protected bool _active;
        protected string _activeCode;
        protected int _point;
        protected int _pointQuestion;
        protected int _pointPrediction;
        protected DateTime? _registerDate;
        protected DateTime? _lastLoginDate;
        protected bool _isAdmin;
        protected IList _predictionGameUsers;
        protected IList _userLogses;
        protected IList _bettingUserses;
        protected IList _questionGameses;
        protected IList _questionUserses;
        protected IList _predictionGameses;

        #endregion

        #region Constructors

        public User() { }

        public User(string displayName, string password, string fullName, string email, string iDNumber, DateTime birthday, string phone, string address, string province, bool active, string activeCode, int point, DateTime registerDate, DateTime lastLoginDate, bool isAdmin)
        {
            this._displayName = displayName;
            this._password = password;
            this._fullName = fullName;
            this._email = email;
            this._iDNumber = iDNumber;
            this._birthday = birthday;
            this._phone = phone;
            this._address = address;
            this._province = province;
            this._active = active;
            this._activeCode = activeCode;
            this._point = point;
            this._registerDate = registerDate;
            this._lastLoginDate = lastLoginDate;
            this._isAdmin = isAdmin;
        }

        #endregion

        #region Public Properties

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string DisplayName
        {
            get { return _displayName ?? string.Empty; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for DisplayName", value, value.ToString());
                _displayName = value;
            }
        }

        public virtual string Password
        {
            get { return _password; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for Password", value, value.ToString());
                _password = value;
            }
        }

        public virtual string FullName
        {
            get { return _fullName ?? string.Empty; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for FullName", value, value.ToString());
                _fullName = value;
            }
        }

        public virtual string Email
        {
            get { return _email ?? string.Empty; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
                _email = value;
            }
        }

        public virtual string IDNumber
        {
            get { return _iDNumber ?? string.Empty; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for IDNumber", value, value.ToString());
                _iDNumber = value;
            }
        }

        public virtual DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public virtual string Phone
        {
            get { return _phone ?? string.Empty; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Phone", value, value.ToString());
                _phone = value;
            }
        }

        public virtual string Address
        {
            get { return _address ?? string.Empty; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
                _address = value;
            }
        }

        public virtual string Province
        {
            get { return _province; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for Province", value, value.ToString());
                _province = value;
            }
        }

        public virtual bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public virtual string ActiveCode
        {
            get { return _activeCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ActiveCode", value, value.ToString());
                _activeCode = value;
            }
        }

        public virtual int PointQuestion
        {
            get { return _pointQuestion; }
            set { _pointQuestion = value; }
        }

        public virtual int PointPrediction
        {
            get { return _pointPrediction; }
            set { _pointPrediction = value; }
        }
   
        public virtual int Point
        {
            get { return _point; }
            set { _point = value; }
        }

        public virtual DateTime? RegisterDate
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        public virtual DateTime? LastLoginDate
        {
            get { return _lastLoginDate; }
            set { _lastLoginDate = value; }
        }

        public virtual bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        public virtual IList PredictionGameUsers
        {
            get
            {
                if (_predictionGameUsers == null)
                {
                    _predictionGameUsers = new ArrayList();
                }
                return _predictionGameUsers;
            }
            set { _predictionGameUsers = value; }
        }

        public virtual IList UserLogses
        {
            get
            {
                if (_userLogses == null)
                {
                    _userLogses = new ArrayList();
                }
                return _userLogses;
            }
            set { _userLogses = value; }
        }

        public virtual IList BettingUserses
        {
            get
            {
                if (_bettingUserses == null)
                {
                    _bettingUserses = new ArrayList();
                }
                return _bettingUserses;
            }
            set { _bettingUserses = value; }
        }

        public virtual IList QuestionGameses
        {
            get
            {
                if (_questionGameses == null)
                {
                    _questionGameses = new ArrayList();
                }
                return _questionGameses;
            }
            set { _questionGameses = value; }
        }

        public virtual IList QuestionUserses
        {
            get
            {
                if (_questionUserses == null)
                {
                    _questionUserses = new ArrayList();
                }
                return _questionUserses;
            }
            set { _questionUserses = value; }
        }

        public virtual IList PredictionGameses
        {
            get
            {
                if (_predictionGameses == null)
                {
                    _predictionGameses = new ArrayList();
                }
                return _predictionGameses;
            }
            set { _predictionGameses = value; }
        }


        #endregion
    }

    #endregion
}
