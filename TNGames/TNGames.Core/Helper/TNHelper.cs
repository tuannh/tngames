using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TNGames.Core.Cache;
using TNGames.Core.Domain;
using NHibernate;
using System.Data;

namespace TNGames.Core.Helper
{
    public class TNHelper
    {
        public const string DateFormat = "dd/MM/yyyy";
        public const string TimeFormat = "hh:mm tt";
        public const string DateTimeFormat = "dd/MM/yyyy hh:mm tt";

        public const string CaptchaKey = "CaptchaText";
        public const string LoginKey = "UserInfo";
        public const string QuestionStarTimeKey = "Question_StarTime";
        public const string PredictionStarTimeKey = "Prediction_StarTime";
        public const string BettingStarTimeKey = "Betting_StarTime";

        public static void RemoveCaches()
        {
            CMSCache.RemoveByPattern("GetSettings");
            CMSCache.RemoveByPattern("GetQuestionGameSettings");
            CMSCache.RemoveByPattern("GetPredictionGameSettings");
            CMSCache.RemoveByPattern("GetBettingGameSettings");
        }

        public static void RemoveRankingCaches()
        {
            CMSCache.RemoveByPattern("uxp_GetUserRank");
            CMSCache.RemoveByPattern("uxp_GetPredictionRank");
            CMSCache.RemoveByPattern("uxp_GetQuestionRank");
            CMSCache.RemoveByPattern("uxp_GetBettingRank");
            CMSCache.RemoveByPattern(TNHelper.LoginKey);
        }

        public static int GetIntValue(double num)
        {
            return (int)num;
        }

        public static double GetFloatValue(double num)
        {
            return num - (int)num;
        }

        #region Setings

        public static BizSettings GetSettings()
        {
            string key = "GetSettings";
            BizSettings obj = CMSCache.Get(key) as BizSettings;
            if (obj == null)
            {
                Setting dm = DomainManager.GetObject<Setting>(1);
                if (dm != null && !string.IsNullOrEmpty(dm.SettingValue))
                {
                    obj = Utils.DeserializeObject<BizSettings>(dm.SettingValue) as BizSettings;
                }

                if (obj == null)
                    obj = new BizSettings();

                CMSCache.Insert(key, obj);
            }

            return obj;
        }

        public static BizQuestionGameSettings GetQuestionGameSettings()
        {
            string key = "GetQuestionGameSettings";
            BizQuestionGameSettings obj = CMSCache.Get(key) as BizQuestionGameSettings;
            if (obj == null)
            {
                Setting dm = DomainManager.GetObject<Setting>(2);
                if (dm != null && !string.IsNullOrEmpty(dm.SettingValue))
                {
                    obj = Utils.DeserializeObject<BizQuestionGameSettings>(dm.SettingValue) as BizQuestionGameSettings;
                }

                if (obj == null)
                    obj = new BizQuestionGameSettings();

                CMSCache.Insert(key, obj);
            }

            return obj;
        }

        public static BizPredictionGameSettings GetPredictionGameSettings()
        {
            string key = "GetPredictionGameSettings";
            BizPredictionGameSettings obj = CMSCache.Get(key) as BizPredictionGameSettings;
            if (obj == null)
            {
                Setting dm = DomainManager.GetObject<Setting>(3);
                if (dm != null && !string.IsNullOrEmpty(dm.SettingValue))
                {
                    obj = Utils.DeserializeObject<BizPredictionGameSettings>(dm.SettingValue) as BizPredictionGameSettings;
                }

                if (obj == null)
                    obj = new BizPredictionGameSettings();

                CMSCache.Insert(key, obj);
            }

            return obj;
        }

        public static BizBettingGameSettings GetBettingGameSettings()
        {
            string key = "GetBettingGameSettings";
            BizBettingGameSettings obj = CMSCache.Get(key) as BizBettingGameSettings;
            if (obj == null)
            {
                Setting dm = DomainManager.GetObject<Setting>(4);
                if (dm != null && !string.IsNullOrEmpty(dm.SettingValue))
                {
                    obj = Utils.DeserializeObject<BizBettingGameSettings>(dm.SettingValue) as BizBettingGameSettings;
                }

                if (obj == null)
                    obj = new BizBettingGameSettings();

                CMSCache.Insert(key, obj);
            }

            return obj;
        }

        #endregion

        #region Betting game

        public static DataTable GetBettingRankTable()
        {
            string key = "uxp_GetBettingRank";
            DataTable dt = CMSCache.Get(key) as DataTable;
            if (dt == null)
            {
                dt = new Database().GetDataTable("uxp_GetBettingRank", CommandType.StoredProcedure);
                if (dt != null)
                    CMSCache.Insert(key, dt);
            }

            return dt;
        }

        public static List<DataRow> GetTopBettingWinner(int num)
        {
            DataTable dt = GetBettingRankTable();
            int totalRow = 0;
            List<DataRow> lstRow = new List<DataRow>();
            if (dt != null)
            {
                totalRow = dt.Rows.Count;
                lstRow = dt.Rows.Cast<DataRow>()
                           .Take(num).ToList();

                //User user = Utils.GetCurrentUser();
                //if (user != null && lstRow != null)
                //{ 
                //    DataRow drUser = lstRow.Where(p=>p.Field<User
                //}
            }

            return lstRow;
        }

        public static List<DataRow> SearchBettingGame(int from, int to)
        {
            DataTable dt = GetBettingRankTable();
            List<DataRow> lst = new List<DataRow>();
            if (dt != null)
            {
                lst = dt.Rows.Cast<DataRow>().ToList() as List<DataRow>;
            }

            if (lst != null)
            {
                if (from > 0 && to > 0)
                {
                    lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) >= from && Convert.ToInt32(p.ItemArray[3]) <= to)
                             .OrderBy(p => p.ItemArray[1])
                             .ToList();
                }
                else
                {
                    if (from > 0 && to == 0)
                    {
                        lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) >= from)
                                .OrderBy(p => p.ItemArray[1])
                                .ToList();
                    }
                    else if (from == 0 && to > 0)
                    {
                        lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) <= to)
                                  .OrderBy(p => p.ItemArray[1])
                                  .ToList();
                    }
                }
            }

            return lst;
        }

        public static Betting GetActiveBettingById(int id)
        {
            Betting betting = null;
            List<Betting> lst = GetAllActiveBetting();
            if (lst != null)
                betting = lst.Where(p => p.Id == id).FirstOrDefault();

            return betting;
        }

        public static List<Betting> GetAllActiveBetting()
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select g from Betting g where g.Active = true and g.PlayDate > :dtnow";
            List<Betting> list = session.CreateQuery(query)
                                         .SetDateTime("dtnow", DateTime.Now)
                                         .SetCacheable(true)
                                         .List<Betting>() as List<Betting>;

            //if (list != null)
            //{
            //    list = list.Where(p => (p.StartDate.HasValue && p.EndDate.HasValue && p.StartDate.Value <= p.EndDate.Value) ||
            //                           (p.StartDate.HasValue && p.StartDate.Value <= DateTime.Now) ||
            //                           (p.EndDate.HasValue && DateTime.Now <= p.EndDate.Value))
            //                .ToList();
            //}

            if (list != null)
                list = list.Where(p => !p.IsBettedByCurrentUser() && p.IsCalculate == false && p.IsUpdateScore == false).ToList();

            return list;
        }

        public static void UpdateBettingResult(int bettingId)
        {
            Betting betting = DomainManager.GetObject<Betting>(bettingId);
            if (betting != null && betting.BettingUserses.Count > 0)
            {
                foreach (BettingUser bu in betting.BettingUserses)
                {
                    if (!betting.IsCalculate)
                    {
                        bu.WinPoint = GetBettingResult(betting, bu);
                        User obj = DomainManager.GetObject<User>(bu.User.Id);
                        if (obj != null)
                        {
                            if (bu.WinPoint == 0)
                                TNHelper.LogAction(bu.User, LogType.BettingLog, "Bạn đã thua hết số điểm đặt cược");
                            else
                            {
                                bu.User.Point += bu.WinPoint;
                                TNHelper.LogAction(bu.User, LogType.BettingLog, string.Format("Bạn được cộng {0} điểm vào tài khoản", bu.WinPoint));
                            }
                        }
                    }
                }

                // Cập nhật kế quả các cược cho tất cả user
                DomainManager.Update(betting);
            }
        }

        public static int GetBettingResult(Betting betting, BettingUser bu)
        {
            int point = 0;
            if (bu.BettingUserDetailses.Count > 0)
            {
                foreach (BettingUserDetail detail in bu.BettingUserDetailses)
                {
                    #region  Home là đội kèo trên, visiting là đội kèo dưới

                    if (detail.BettingRate.HomeRate == 0)
                    {
                        int n = TNHelper.GetIntValue(detail.BettingRate.VisitingRate);
                        double m = TNHelper.GetFloatValue(detail.BettingRate.VisitingRate);

                        // tỉ số hòa
                        if (betting.HomeGoalScore == betting.VisitingGoalScore)
                        {
                            if (n == 0)
                            {
                                if (m == 0) // không thắng thua trả lại số điểm
                                {
                                    point += detail.BettingPoint;
                                }
                                else if (m == 0.25f)
                                {
                                    // đội kèo dưới thắng nữa tiền
                                    if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                    {
                                        point += detail.BettingPoint + (detail.BettingPoint / 2);
                                    }
                                    else // đội kèo trên thua nữa tiền
                                    {
                                        point += (detail.BettingPoint / 2);
                                    }
                                }
                                else
                                {
                                    // đội kèo dưới thắng đủ tiền
                                    if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                    {
                                        point += (detail.BettingPoint * 2);
                                    }
                                }
                            }
                            else
                            {
                                // đội kèo dưới thắng đủ tiền
                                if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                    point += (detail.BettingPoint * 2);
                            }
                        }
                        // kèo trên thắng
                        else if (betting.HomeGoalScore > betting.VisitingGoalScore)
                        {
                            int x = betting.HomeGoalScore - betting.VisitingGoalScore;
                            if (x < n)
                            {
                                // kèo dưới thắng đủ tiền, kèo trên thua đủ tiền
                                if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                    point += (detail.BettingPoint * 2);
                            }
                            else if (x > n)
                            {
                                if (x - n == 1 && m == 0.75f)
                                {
                                    // kèo dưới thắng nữa tiền
                                    if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                        point += detail.BettingPoint + (detail.BettingPoint / 2);
                                    else
                                        // kèo trên thua nữa tiền
                                        point += (detail.BettingPoint / 2);
                                }
                                else
                                {
                                    // kèo trên thắng đủ tiền, kèo dưới thua đủ tiền
                                    if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                        point += (detail.BettingPoint * 2);
                                }
                            }
                            else // x == n
                            {
                                if (m == 0)
                                {
                                    // không thắng thua, trả lại số điểm
                                    point += detail.BettingPoint;
                                }
                                else if (m == 0.25f)
                                {
                                    // kèo dưới thắng nữa tiền
                                    if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                        point += detail.BettingPoint + (detail.BettingPoint / 2);
                                    else
                                        // kèo trên thua nữa tiền
                                        point += (detail.BettingPoint / 2);
                                }
                                else
                                {
                                    // kèo dứơi thắng đủ tiền, kèo trên thua đủ tiền
                                    if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                        point += (detail.BettingPoint * 2);
                                }
                            }
                        }
                        else // kèo dưới thắng
                        {
                            if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                point += (detail.BettingPoint * 2);
                        }
                    }

                    #endregion

                    #region Visiting là kèo trên, home là kèo dưới

                    else
                    {
                        int n = TNHelper.GetIntValue(detail.BettingRate.HomeRate);
                        double m = TNHelper.GetFloatValue(detail.BettingRate.HomeRate);

                        // tỉ số hòa
                        if (betting.HomeGoalScore == betting.VisitingGoalScore)
                        {
                            if (n == 0)
                            {
                                if (m == 0) // không thắng thua trả lại số điểm
                                {
                                    point += detail.BettingPoint;
                                }
                                else if (m == 0.25f)
                                {
                                    // đội kèo dưới thắng nữa tiền
                                    if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                    {
                                        point += detail.BettingPoint + (detail.BettingPoint / 2);
                                    }
                                    else // đội kèo trên thua nữa tiền
                                    {
                                        point += (detail.BettingPoint / 2);
                                    }
                                }
                                else
                                {
                                    // đội kèo dưới thắng đủ tiền
                                    if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                    {
                                        point += (detail.BettingPoint * 2);
                                    }
                                }
                            }
                            else
                            {
                                // đội kèo dưới thắng đủ tiền
                                if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                    point += (detail.BettingPoint * 2);
                            }
                        }
                        // kèo trên thắng
                        else if (betting.VisitingGoalScore > betting.HomeGoalScore)
                        {
                            int x = betting.VisitingGoalScore - betting.HomeGoalScore;
                            if (x < n)
                            {
                                // kèo dưới thắng đủ tiền, kèo trên thua đủ tiền
                                if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                    point += (detail.BettingPoint * 2);
                            }
                            else if (x > n)
                            {
                                if (x - n == 1 && m == 0.75f)
                                {
                                    // kèo dưới thắng nữa tiền
                                    if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                        point += detail.BettingPoint + (detail.BettingPoint / 2);
                                    else
                                        // kèo trên thua nữa tiền
                                        point += (detail.BettingPoint / 2);
                                }
                                else
                                {
                                    // kèo dưới thắng đủ tiền, kèo trên thua đủ tiền
                                    if (string.Compare(detail.SelectedTeam, betting.VisitingTeam, true) == 0)
                                        point += (detail.BettingPoint * 2);
                                }
                            }
                            else // x == n
                            {
                                if (m == 0)
                                {
                                    // không thắng thua, trả lại số điểm
                                    point += detail.BettingPoint;
                                }
                                else if (m == 0.25f)
                                {
                                    // kèo dưới thắng nữa tiền
                                    if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                        point += detail.BettingPoint + (detail.BettingPoint / 2);
                                    else
                                        // kèo trên thua nữa tiền
                                        point += (detail.BettingPoint / 2);
                                }
                                else
                                {
                                    // kèo dứơi thắng đủ tiền, kèo trên thua đủ tiền
                                    if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                        point += (detail.BettingPoint * 2);
                                }
                            }
                        }
                        else // kèo dưới thắng
                        {
                            // kèo dưới thắng đủ tiền, kèo trên thua đủ tiền
                            if (string.Compare(detail.SelectedTeam, betting.HomeTeam, true) == 0)
                                point += (detail.BettingPoint * 2);
                        }
                    }

                    #endregion
                }
            }

            return point;
        }

        #endregion

        #region Question

        public static QuestionGame GetCurrentQuestionGame()
        {
            BizQuestionGameSettings settings = GetQuestionGameSettings();
            if (settings != null)
            {
                ISession session = SessionFactory.GetCurrentSession();
                string query = "select g from QuestionGame g where g.Id = :id";
                QuestionGame qgame = session.CreateQuery(query)
                                             .SetInt32("id", settings.QuestionGameID)
                                             .SetCacheable(true)
                                             .UniqueResult<QuestionGame>();

                return qgame;
            }
            return null;
        }

        public static List<Question> GetAllActiveQuestion()
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select g from Question g where g.Active = true";
            List<Question> list = session.CreateQuery(query)
                                         .SetCacheable(true)
                                         .List<Question>() as List<Question>;
            return list;
        }

        public static bool IsValidToPlayQuestionGame()
        {
            bool result = true;
            int playCount = 1;

            User user = Utils.GetCurrentUser();
            BizQuestionGameSettings setting = GetQuestionGameSettings();
            if (setting != null)
                playCount = setting.NumPlayPerDay;

            //List<QuestionUser> lst = GetAllQuestionGameUserInToday();
            //if (lst != null && lst.Count > 0 && user != null)
            //{
            //    lst = lst.Where(p => p.User != null && p.User.Id == user.Id).ToList();

            //    if (lst == null || (lst != null && lst.Count < playCount))
            //        result = true;
            //    else
            //        result = false;
            //}

            if (user != null && user.QuestionUserses.Count > 0)
            {
                List<QuestionUser> lst = user.QuestionUserses.Cast<QuestionUser>()
                                                             .Where(p => p.QuestionGame != null &&
                                                                         p.QuestionGame.Id == setting.QuestionGameID)
                                                              .ToList();

                if (lst == null || (lst != null && lst.Count < playCount))
                    result = true;
                else
                    result = false;
            }

            return result;
        }

        public static List<QuestionUser> GetAllQuestionGameUserInToday()
        {
            List<QuestionUser> lst = GetAllQuestionUser();
            if (lst != null && lst.Count > 0)
            {
                lst = lst.Where(p => p.PlayDate.Day == DateTime.Now.Day &&
                                    p.PlayDate.Month == DateTime.Now.Month &&
                                    p.PlayDate.Year == DateTime.Now.Year)
                         .ToList();
            }

            return lst;
        }

        public static List<QuestionUser> GetAllQuestionUser()
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select g from QuestionUser g";
            List<QuestionUser> list = session.CreateQuery(query)
                                             .SetCacheable(true)
                                             .List<QuestionUser>() as List<QuestionUser>;

            return list;
        }

        public static DataTable GetQuestionRankTable()
        {
            string key = "uxp_GetQuestionRank";
            DataTable dt = CMSCache.Get(key) as DataTable;
            if (dt == null)
            {
                dt = new Database().GetDataTable("uxp_GetQuestionRank", CommandType.StoredProcedure);
                if (dt != null)
                    CMSCache.Insert(key, dt);
            }

            return dt;
        }

        public static List<DataRow> GetTopQuestionGameWinner(int num)
        {
            DataTable dt = GetQuestionRankTable();
            int totalRow = 0;
            List<DataRow> lstRow = new List<DataRow>();
            if (dt != null)
            {
                totalRow = dt.Rows.Count;
                lstRow = dt.Rows.Cast<DataRow>()
                           .Take(num).ToList();
            }

            return lstRow;
        }

        public class QuestionUserCompare : IEqualityComparer<QuestionUser>
        {

            #region IEqualityComparer<QuestionUser> Members

            public bool Equals(QuestionUser x, QuestionUser y)
            {
                if (x.User != null && y.User != null)
                    return x.User.Id == y.User.Id;

                return false;
            }

            public int GetHashCode(QuestionUser obj)
            {
                if (obj.User != null)
                    return obj.User.Id;

                return obj.GetHashCode();
            }

            #endregion
        }

        public static List<DataRow> SearchQuestionGame(int from, int to)
        {
            DataTable dt = GetQuestionRankTable();
            List<DataRow> lst = new List<DataRow>();
            if (dt != null)
            {
                lst = dt.Rows.Cast<DataRow>().ToList() as List<DataRow>;
            }

            if (lst != null)
            {
                if (from > 0 && to > 0)
                {
                    lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) >= from && Convert.ToInt32(p.ItemArray[3]) <= to)
                             .OrderBy(p => p.ItemArray[1])
                             .ToList();
                }
                else
                {
                    if (from > 0 && to == 0)
                    {
                        lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) >= from)
                                .OrderBy(p => p.ItemArray[1])
                                .ToList();
                    }
                    else if (from == 0 && to > 0)
                    {
                        lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) <= to)
                                  .OrderBy(p => p.ItemArray[1])
                                  .ToList();
                    }
                }
            }

            return lst;
        }

        #endregion

        #region Prediction game

        public static PredictionGameUser GetPredictionGameUserByGameId(int gameId, int userId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select g from PredictionGameUser g where g.PredictionGame.Id = :id and g.User.id = :userId";
            List<PredictionGameUser> list = session.CreateQuery(query)
                                                     .SetInt32("id", gameId)
                                                     .SetInt32("userId", userId)
                                                     .SetCacheable(true)
                                                     .List<PredictionGameUser>() as List<PredictionGameUser>;

            if (list != null && list.Count > 0)
            {
                list = list.OrderByDescending(p => p.PlayDate)
                           .ToList();

                return list[0];
            }

            return null;
        }

        public static void CalculatePredcitionGame(PredictionGame pgame)
        {
            if (pgame != null)
            {
                foreach (PredictionGameUser pgUser in pgame.PredictionGameUsers)
                {
                    User user = pgUser.User;
                    int bonus = 0;
                    if (user != null)
                    {
                        foreach (PredictionGameUserDetail pgudetail in pgUser.PredictionGameUserDetailses)
                        {
                            if (pgudetail.Prediction != null)
                            {
                                PredictionAnswer rightAnswer = pgudetail.Prediction.PredictionAnswerses.Cast<PredictionAnswer>().Where(p => p.IsCorrectAnswer).FirstOrDefault();
                                if (rightAnswer != null && pgudetail.PredictionAnswer != null && rightAnswer.Id == pgudetail.PredictionAnswer.Id)
                                    bonus += pgudetail.Prediction.BonusPoint;
                            }
                        }
                    }

                    user.Point += bonus;
                    user.PointPrediction += bonus;
                    pgUser.WinPoint = bonus;
                    TNHelper.LogAction(user, LogType.PredictionLog, string.Format("Cộng {0} điểm vào tổng điểm game dự đoán.<br/>Cộng {0} điểm vào tổng điểm game cá cược", bonus));
                }

                pgame.IsCalculate = true;
                DomainManager.Update(pgame);
            }
        }

        public static PredictionGame GetCurrentPredictionGame()
        {
            BizPredictionGameSettings settings = GetPredictionGameSettings();
            if (settings != null)
            {
                ISession session = SessionFactory.GetCurrentSession();
                string query = "select g from PredictionGame g where g.Id = :id and g.IsCalculate = false";
                PredictionGame qgame = session.CreateQuery(query)
                                             .SetInt32("id", settings.PredictionGameID)
                                             .SetCacheable(true)
                                             .UniqueResult<PredictionGame>();
                return qgame;
            }
            return null;
        }

        public static List<DataRow> SearchPreditonGame(int from, int to)
        {
            DataTable dt = TNHelper.GetPredictionRankTable();
            List<DataRow> lst = new List<DataRow>();
            if (dt != null)
            {
                lst = dt.Rows.Cast<DataRow>().ToList() as List<DataRow>;
            }

            if (lst != null)
            {
                if (from > 0 && to > 0)
                {
                    lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) >= from && Convert.ToInt32(p.ItemArray[3]) <= to)
                             .OrderBy(p => p.ItemArray[1])
                             .ToList();
                }
                else
                {
                    if (from > 0 && to == 0)
                    {
                        lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) >= from)
                                .OrderBy(p => p.ItemArray[1])
                                .ToList();
                    }
                    else if (from == 0 && to > 0)
                    {
                        lst = lst.Where(p => Convert.ToInt32(p.ItemArray[3]) <= to)
                                  .OrderBy(p => p.ItemArray[1])
                                  .ToList();
                    }
                }
            }

            return lst;
        }

        public static List<Prediction> GetAllActivePrediction()
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select g from Prediction g where g.Active = true";
            List<Prediction> list = session.CreateQuery(query)
                                         .SetCacheable(true)
                                         .List<Prediction>() as List<Prediction>;
            return list;
        }

        public static List<PredictionGameUser> GetAllPredictionGameUser()
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select g from PredictionGameUser g";
            List<PredictionGameUser> list = session.CreateQuery(query)
                                         .SetCacheable(true)
                                         .List<PredictionGameUser>() as List<PredictionGameUser>;

            return list;
        }

        public static List<PredictionGameUser> GetAllPredictionGameUserInToday()
        {
            List<PredictionGameUser> lst = GetAllPredictionGameUser();
            if (lst != null && lst.Count > 0)
            {
                lst = lst.Where(p => p.PlayDate.Day == DateTime.Now.Day &&
                                    p.PlayDate.Month == DateTime.Now.Month &&
                                    p.PlayDate.Year == DateTime.Now.Year)
                         .ToList();
            }

            return lst;
        }

        public static bool IsValidToPlayPredictionGame()
        {
            bool result = true;
            int playCount = 1;

            List<PredictionGameUser> lst = GetAllPredictionGameUserInToday();
            User user = Utils.GetCurrentUser();
            BizPredictionGameSettings setting = GetPredictionGameSettings();
            if (setting != null)
                playCount = setting.NumPlayPerDay;

            if (lst != null && lst.Count > 0 && user != null)
            {
                lst = lst.Where(p => p.User != null && p.User.Id == user.Id).ToList();

                if (lst == null || (lst != null && lst.Count < playCount))
                    result = true;
                else
                    result = false;
            }

            return result;
        }

        public static DataTable GetPredictionRankTable()
        {
            string key = "uxp_GetPredictionRank";
            DataTable dt = CMSCache.Get(key) as DataTable;
            if (dt == null)
            {
                dt = new Database().GetDataTable("uxp_GetPredictionRank", CommandType.StoredProcedure);
                if (dt != null)
                    CMSCache.Insert(key, dt);
            }

            return dt;
        }

        public static List<DataRow> GetTopPredictionWinner(int num)
        {
            DataTable dt = GetPredictionRankTable();
            int totalRow = 0;
            List<DataRow> lstRow = new List<DataRow>();
            if (dt != null)
            {
                totalRow = dt.Rows.Count;
                lstRow = dt.Rows.Cast<DataRow>()
                           .Take(num).ToList();
            }

            return lstRow;
        }

        public class PredictionGameUserCompare : IEqualityComparer<PredictionGameUser>
        {

            #region IEqualityComparer<PredictionGameUser> Members

            public bool Equals(PredictionGameUser x, PredictionGameUser y)
            {
                if (x.User != null && y.User != null)
                    return x.User.Id == y.User.Id;

                return false;
            }

            public int GetHashCode(PredictionGameUser obj)
            {
                if (obj.User != null)
                    return obj.User.Id;

                return obj.GetHashCode();
            }

            #endregion
        }

        #endregion

        #region User

        public static bool IsValidRegisterEmail(string email)
        {
            bool invalid = false;
            List<User> lstUsers = GetAllUsers();
            if (lstUsers != null)
            {
                invalid = lstUsers.Exists(p => string.Compare(p.Email, email, true) == 0);

            }

            return !invalid;
        }

        public static bool IsValidIdNumber(string idNumber)
        {
            bool invalid = false;
            List<User> lstUsers = GetAllUsers();
            if (lstUsers != null)
            {
                invalid = lstUsers.Exists(p => string.Compare(p.IDNumber, idNumber, true) == 0);
            }

            return !invalid;
        }

        public static List<User> GetAllUsers()
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select u from User u";
            List<User> lstUsers = session.CreateQuery(query)
                                         .SetCacheable(true)
                                         .List<User>() as List<User>;

            return lstUsers;
        }

        public static List<User> GetAllFrontEndUsers()
        {
            List<User> lstUsers = GetAllUsers();
            if (lstUsers != null)
            {
                lstUsers = lstUsers.Where(u => u.IsAdmin == false)
                                   .OrderBy(u => u.FullName)
                                   .ThenBy(u => u.DisplayName)
                                   .ThenByDescending(u => u.Point)
                                   .ToList();
            }
            return lstUsers;
        }

        public static List<User> GetTopRankingUser(int item)
        {
            List<User> lstUsers = GetAllFrontEndUsers();
            if (lstUsers != null)
            {
                lstUsers = lstUsers.Where(p => p.Active)
                                   .OrderByDescending(p => p.TotalPoint)
                                   .Take(item)
                                   .ToList();
            }

            return lstUsers;
        }

        public static List<User> FindUser(string keyword)
        {
            string kw = keyword.ToLower();
            List<User> lstUsers = GetAllFrontEndUsers();
            if (lstUsers != null)
            {
                lstUsers = lstUsers.Where(u => u.FullName.ToLower().Contains(kw) ||
                                               u.DisplayName.ToLower().Contains(kw) ||
                                               u.Email.ToLower().Contains(kw) ||
                                               u.IDNumber.Contains(kw) ||
                                               u.Address.ToLower().Contains(kw) ||
                                               u.Phone.Contains(kw) ||
                                               u.TotalPoint.ToString().Contains(kw))
                                   .OrderBy(u => u.FullName)
                                   .ThenBy(u => u.DisplayName)
                                   .ThenByDescending(u => u.Point)
                                   .ToList();
            }
            return lstUsers;
        }

        public static User GetUserByActiveCode(string activeCode)
        {
            List<User> lst = GetAllUsers();
            if (lst != null && lst.Count > 0)
                return lst.FirstOrDefault(u => string.Compare(u.ActiveCode, activeCode, true) == 0);

            return null;
        }

        public static User GetUserByEmail(string email)
        {
            List<User> lst = GetAllUsers();
            if (lst != null && lst.Count > 0)
                return lst.FirstOrDefault(u => string.Compare(u.Email, email, true) == 0);

            return null;
        }

        public static User GetUserById(int userId)
        {
            List<User> lst = GetAllUsers();
            if (lst != null && lst.Count > 0)
                return lst.FirstOrDefault(u => u.Id == userId);

            return null;
        }


        public static bool ResetUser(int userId)
        {
            bool result = false;
            User user = DomainManager.GetObject<User>(userId);
            if (user != null)
            {
                // reset default point
                user.Point = TNHelper.GetSettings().DefaultPoint;
                user.PointPrediction = 0;
                user.PointQuestion = 0;

                // remove betting game is not calculated
                List<BettingUser> removeBetting = new List<BettingUser>();
                foreach (BettingUser bu in user.BettingUserses)
                {
                    if (bu.Betting != null && !bu.Betting.IsCalculate)
                        removeBetting.Add(bu);
                }

                foreach (BettingUser bu in removeBetting)
                    user.BettingUserses.Remove(bu);

                // remove prediction game is not calculated
                List<PredictionGameUser> removePrediction = new List<PredictionGameUser>();
                foreach (PredictionGameUser pu in user.PredictionGameUsers)
                {
                    if (pu.PredictionGame != null && !pu.PredictionGame.IsCalculate)
                        removePrediction.Add(pu);
                }

                foreach (PredictionGameUser bu in removePrediction)
                    user.PredictionGameUsers.Remove(bu);

                DomainManager.Update(user);
                Utils.ResetCurrentUser();

                result = true;
            }

            return result;
        }

        public static int GetUserRank(int userId)
        {
            int rank = 0;
            string key = "uxp_GetUserRank";
            DataTable dt = CMSCache.Get(key) as DataTable;
            if (dt == null)
            {
                dt = new Database().GetDataTable("uxp_GetUserRank", CommandType.StoredProcedure);
                if (dt != null)
                    CMSCache.Insert(key, dt);
            }


            if (dt != null && dt.Rows.Count > 0)
            {
                dt.PrimaryKey = new DataColumn[] { dt.Columns["UserID"] };
                DataRow dr = dt.Rows.Find(userId);
                if (dr != null)
                {
                    rank = Convert.ToInt32(dr["RanK"]);
                }
            }

            return rank;
        }

        public static List<UserLog> GetUserLogs(int userId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string hquery = string.Format("select l from UserLog l where l.User.Id = {0} order by l.LogDate desc", userId);
            List<UserLog> lst = session.CreateQuery(hquery)
                                       .SetCacheable(true)
                                       .List<UserLog>() as List<UserLog>;

            return lst;
        }

        public static List<BettingUser> GetBettingUserGameInfo(int userId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string hquery = string.Format("select l from BettingUser l where l.User.Id = {0} order by l.BettingDate desc", userId);
            List<BettingUser> lst = session.CreateQuery(hquery)
                                       .SetCacheable(true)
                                       .List<BettingUser>() as List<BettingUser>;

            return lst;
        }

        public static List<PredictionGameUser> GetPredictionUserGameInfo(int userId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string hquery = string.Format("select l from PredictionGameUser l where l.User.Id = {0} order by l.PlayDate desc", userId);
            List<PredictionGameUser> lst = session.CreateQuery(hquery)
                                       .SetCacheable(true)
                                       .List<PredictionGameUser>() as List<PredictionGameUser>;

            return lst;
        }

        public static List<QuestionUser> GetQuestionUserGameInfo(int userId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string hquery = string.Format("select l from QuestionUser l where l.User.Id = {0} order by l.PlayDate desc", userId);
            List<QuestionUser> lst = session.CreateQuery(hquery)
                                       .SetCacheable(true)
                                       .List<QuestionUser>() as List<QuestionUser>;

            return lst;
        }

        #endregion

        #region News

        public static string GetNewsPhoto(string photoName)
        {
            string url = string.Format("/Userfiles/News/{0}", photoName);
            return url;
        }

        public static List<New> GetTopNews(int itemCount)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select u from New u where u.Active = 1 order by u.CreatedDate desc, u.NewsTitle";
            List<New> lst = session.CreateQuery(query)
                                         .SetCacheable(true)
                                         .SetMaxResults(itemCount)
                                         .List<New>() as List<New>;



            return lst;
        }

        public static List<New> GetRelateNew(int itemCount, DateTime dt)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select u from New u where u.Active = 1 and u.CreatedDate < :dt";
            List<New> lst = session.CreateQuery(query)
                                         .SetDateTime("dt", dt)
                                         .SetCacheable(true)
                                         .SetMaxResults(itemCount)
                                         .List<New>() as List<New>;
            return lst;
        }


        #endregion

        #region logs

        public static void LogAction(User user, LogType type, string content)
        {
            UserLog log = new UserLog();
            log.LogDate = DateTime.Now;
            log.User = user;
            log.LogType = (int)type;
            log.LogAction = content;
            DomainManager.Insert(log);
        }

        public static void LogAction(LogType type, string content)
        {
            UserLog log = new UserLog();
            log.LogDate = DateTime.Now;
            log.User = Utils.GetCurrentUser();
            log.LogType = (int)type;
            log.LogAction = content;
            DomainManager.Insert(log);
        }

        #endregion

        #region Content

        public static string GetContent(int contentTypeId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select c from Content c where c.ContentType.Id = :id";
            List<Content> lst = session.CreateQuery(query)
                                       .SetInt32("id", contentTypeId)
                                       .SetCacheable(true)
                                       .List<Content>() as List<Content>;

            if (lst != null && lst.Count > 0)
                return lst[0].ContentText;

            return string.Empty;
        }

        public static ContentType GetContentTypeById(int contentTypeId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select c from ContentType c where c.Id = :id";
            ContentType type = session.CreateQuery(query)
                                       .SetInt32("id", contentTypeId)
                                       .SetCacheable(true)
                                       .UniqueResult<ContentType>();

            return type;
        }

        public static Content GetContentObject(int id)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select c from Content c where c.Id = :id";
            List<Content> lst = session.CreateQuery(query)
                                       .SetInt32("id", id)
                                       .SetCacheable(true)
                                       .List<Content>() as List<Content>;

            if (lst != null && lst.Count > 0)
                return lst[0];

            return null;
        }

        public static List<Content> GetContentsByType(int typeId)
        {
            ISession session = SessionFactory.GetCurrentSession();
            string query = "select c from Content c where c.ContentType.Id = :id";
            List<Content> lst = session.CreateQuery(query)
                                       .SetInt32("id", typeId)
                                       .SetCacheable(true)
                                       .List<Content>() as List<Content>;

            return lst;
        }

        #endregion
    }
}
