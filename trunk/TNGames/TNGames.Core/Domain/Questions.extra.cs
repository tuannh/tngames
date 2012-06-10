
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace TNGames.Core.Domain
{
    #region Question

    /// <summary>
    /// Question object for NHibernate mapped table 'Questions'.
    /// </summary>
    public partial class Question
    {
        public virtual Answer CorrectAnswer
        {
            get
            {
                if (Answerses.Count > 0)
                {
                    foreach (Answer ans in Answerses)
                    {
                        if (ans.IsCorrectAnswer)
                            return ans;
                    }                
                }

                return null;
            }
        }
    }

    #endregion
}
