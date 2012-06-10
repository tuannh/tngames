using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core;
using TNGames.Core.Helper;
using DM = TNGames.Core.Domain;

namespace TNGames.FrontEnd.User
{
    public partial class Submit : System.Web.UI.Page
    {
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region input data
                
                int bettingId = 0;
                if (Request.Form["id"] != null)
                    int.TryParse(Request.Form["id"], out bettingId);

                string team = "";
                if (Request.Form["team"] != null)
                    team = Request.Form["team"];

                int point = 0;
                if (Request.Form["point"] != null)
                    int.TryParse(Request.Form["point"], out point);

                int rateId = 0;
                if (Request.Form["rateId"] != null)
                    int.TryParse(Request.Form["rateId"], out rateId);

                #endregion

                if (bettingId == 0 || team == "" || point ==  0 || rateId == 0)
                {
                    msg = "Dữ liệu bạn nhập không hợp lệ";
                    Response.Write(string.Format("0, {0}", msg));
                    return;
                }

                if (!IsValidateBetting(bettingId))
                {
                    msg = "Đã hết thời gian đặt qui định.";
                    Response.Write(string.Format("0, {0}", msg));
                    return;
                }
                
                if (Save(bettingId, team, rateId, point))
                {
                    Response.Write("1, Lưu dữ liệu thành công");
                }
                else
                {
                    Response.Write(string.Format("0, {0}", msg));
                }
            }
        }

        protected bool Save(int bettingId, string team, int rateId, int point)
        {
            DM.Betting betting = DomainManager.GetObject<DM.Betting>(bettingId);
            if (betting == null)
            {
                msg = "";
                return false;
            }

            DM.User user = Utils.GetCurrentUser();
            if (user != null)
                user = DomainManager.GetObject<DM.User>(user.Id);

            if (user == null || (user != null && point > user.Point))
            {
                msg = "Số điểm phải nhỏ hơn hoặc bằng số điểm hiện có của bạn.";
                return false;
            }

            BettingUser bu = new BettingUser();
            bu.Betting = betting;
            bu.BettingDate = DateTime.Now;
            bu.User = user;

            BettingUserDetail bud = new BettingUserDetail();
            bud.BettingPoint = point;
            bud.BettingRate = DomainManager.GetObject<BettingRate>(rateId);
            bud.BettingUser = bu;

            string selected = betting.HomeTeam;
            if (string.Compare(team, "b", true) == 0)
                selected = betting.VisitingTeam;

            bud.SelectedTeam = selected;
            bu.BettingUserDetailses.Add(bud);

            DomainManager.Insert(bu);
            TNHelper.LogAction(LogType.BettingLog, string.Format("Chơi game thử tài phân tích trận đấu. Số điểm chơi: {0} điểm", point));

            user.Point -= point;
            DomainManager.Update(user);
            Utils.ResetCurrentUser();
            TNHelper.LogAction(LogType.BettingLog, "Cập nhật điểm số người chơi sau khi chơi game thử tài phân thích trận đấu");

            return true;
        }

        public bool IsValidateBetting(int bettingId)
        {
            List<DM.Betting> lst = TNHelper.GetAllActiveBetting();

            if (lst != null)
            {
                return lst.Exists(p => p.Id == bettingId);
            }

            return false;
        }
    }
}