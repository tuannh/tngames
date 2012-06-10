
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
    #region Betting

    /// <summary>
    /// Betting object for NHibernate mapped table 'Betting'.
    /// </summary>
    public partial class Betting
    {
        #region Member Variables

        protected int _id;
        protected string _bettingName;
        protected string _description;
        protected string _homeTeam;
        protected string _visitingTeam;
        protected int _homeGoalScore;
        protected int _visitingGoalScore;
        protected DateTime? _playDate;
        protected DateTime? _startDate;
        protected DateTime? _endDate;
        protected bool _isUpdateScore;
        protected bool _isCalculate;
        protected bool _active;
        protected DateTime _createdDate;
        protected DateTime? _modifiedDate;
        protected IList _bettingUserses;
        protected IList _bettingRateses;

        #endregion

        #region Constructors

        public Betting() { }

        public Betting(string bettingName, string homeTeam, string visitingTeam, int homeGoalScore, int visitingGoalScore, DateTime playDate, DateTime startDate, DateTime endDate, bool active, DateTime createdDate, DateTime modifiedDate)
        {
            this._bettingName = bettingName;
            this._homeTeam = homeTeam;
            this._visitingTeam = visitingTeam;
            this._homeGoalScore = homeGoalScore;
            this._visitingGoalScore = visitingGoalScore;
            this._playDate = playDate;
            this._startDate = startDate;
            this._endDate = endDate;
            this._active = active;
            this._createdDate = createdDate;
            this._modifiedDate = modifiedDate;
        }

        #endregion

        #region Public Properties

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string BettingName
        {
            get { return _bettingName; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for BettingName", value, value.ToString());
                _bettingName = value;
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


        public virtual string HomeTeam
        {
            get { return _homeTeam; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for HomeTeam", value, value.ToString());
                _homeTeam = value;
            }
        }

        public virtual string VisitingTeam
        {
            get { return _visitingTeam; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for VisitingTeam", value, value.ToString());
                _visitingTeam = value;
            }
        }

        public virtual int HomeGoalScore
        {
            get { return _homeGoalScore; }
            set { _homeGoalScore = value; }
        }

        public virtual int VisitingGoalScore
        {
            get { return _visitingGoalScore; }
            set { _visitingGoalScore = value; }
        }

        public virtual DateTime? PlayDate
        {
            get { return _playDate; }
            set { _playDate = value; }
        }

        public virtual DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public virtual bool IsUpdateScore
        {
            get { return _isUpdateScore; }
            set { _isUpdateScore = value; }
        }

        public virtual bool IsCalculate
        {
            get { return _isCalculate; }
            set { _isCalculate = value; }
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

        public virtual DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
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

        public virtual IList BettingRateses
        {
            get
            {
                if (_bettingRateses == null)
                {
                    _bettingRateses = new ArrayList();
                }
                return _bettingRateses;
            }
            set { _bettingRateses = value; }
        }


        #endregion
    }

    #endregion
}
