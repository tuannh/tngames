using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using TNGames.Core;

namespace TNGames.Controls.FrontEnd
{
    public partial class Skin_GameList : System.Web.UI.UserControl
    {
        protected bool IsPausedBetting = false;
        protected bool IsPausedQuestion = false;
        protected bool IsPausedPrediction = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BizBettingGameSettings betting = TNHelper.GetBettingGameSettings();
                if (betting != null)
                    IsPausedBetting = betting.IsPaused;

                BizQuestionGameSettings question = TNHelper.GetQuestionGameSettings();
                if (question != null)
                    IsPausedQuestion = question.IsPaused;


                BizPredictionGameSettings prediction = TNHelper.GetPredictionGameSettings();
                if (prediction != null)
                    IsPausedPrediction = prediction.IsPaused;
            }
        }
    }
}