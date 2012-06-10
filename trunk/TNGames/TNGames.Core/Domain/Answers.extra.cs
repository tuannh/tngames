
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
    #region Answer

    /// <summary>
    /// Answer object for NHibernate mapped table 'Answers'.
    /// </summary>
    public partial class Answer
    {
        // used for archivement list
        public virtual bool IsUserAnswer
        {
            get;
            set;
        }
    }

    #endregion
}
