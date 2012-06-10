using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNGames.Core
{
    public class BizQuestionGameSettings
    {
        public BizQuestionGameSettings()
        {
            Timer = 200;
            NumPlayPerDay = 1;
            QuestionGameID = 0;
            IsPaused = false;
            MaxDisplayItem = 10;
        }

        public int MaxDisplayItem
        {
            get;
            set;
        }

        public int Timer
        {
            get;
            set;
        }

        public int NumPlayPerDay
        {
            get;
            set;
        }

        public int QuestionGameID
        {
            get;
            set;
        }

        public bool IsPaused
        {
            get;
            set;
        }
    }
}
