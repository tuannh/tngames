using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNGames.Core
{
    public class BizBettingGameSettings
    {
        public BizBettingGameSettings()
        {
            IsPaused = false;
            MaxDisplayItem = 10;
            AllowDelete = false;
        }

        public bool AllowDelete
        {
            get;
            set;
        }

        public bool IsPaused
        {
            get;
            set;
        }

        public int MaxDisplayItem
        {
            get;
            set;
        }
    }
}
