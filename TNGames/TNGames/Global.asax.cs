using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace TNGames
{
    public class Global : System.Web.HttpApplication
    {

        void RegisterRoutes(RouteCollection routes)
        {
            // using vs 2010
            #region admin

            routes.MapPageRoute(
                "AdminEditLink",      // Route name
                "admincp/{ControlName}/{id}",      // Route URL
                "~/admincp/default.aspx" // Web page to handle route
           );

            routes.MapPageRoute(
                 "AdminLink",
                 "admincp/{ControlName}",
                 "~/admincp/default.aspx"
            );

            #endregion

            #region guest

            routes.MapPageRoute(
              "Feedback",
              "gop-y-phan-hoi",
              "~/frontend/feedback.aspx"
           );

            routes.MapPageRoute(
               "ActiveLink",
               "kich-hoat-tai-khoan/{ActiveCode}",
               "~/frontend/active.aspx"
            );

            routes.MapPageRoute(
              "forgotpassword",
              "quen-mat-khau",
              "~/frontend/forgotpassword.aspx"
           );

            routes.MapPageRoute(
             "RegisterLink",
             "dang-ky",
             "~/frontend/register.aspx"
            );

            routes.MapPageRoute(
           "login",
           "dang-nhap",
           "~/frontend/login.aspx"
          );

            routes.MapPageRoute(
            "Contact",
            "lien-he",
            "~/frontend/contact.aspx"
            );

            routes.MapPageRoute(
             "newsdetail",
             "tin-tuc/{newsid}/{newsalias}",
             "~/frontend/NewsDetail.aspx"
            );

            routes.MapPageRoute(
            "news",
            "tin-tuc/",
            "~/frontend/news.aspx"
           );

            routes.MapPageRoute(
         "noticedetail",
         "thong-bao/{id}/{alias}",
         "~/frontend/notice.aspx"
        );

            routes.MapPageRoute(
             "default",
             "trang-tro-choi",
             "~/default.aspx"
            );

            #endregion

            #region user

            routes.MapPageRoute(
            "ProfileLink",
            "tai-khoan",
            "~/frontend/user/account.aspx"
           );

            routes.MapPageRoute(
       "AchievementType",
       "thanh-tich/{type}",
       "~/frontend/user/Achievement.aspx"
      );

            routes.MapPageRoute(
          "Achievement",
          "thanh-tich",
          "~/frontend/user/Achievement.aspx"
         );

            routes.MapPageRoute(
               "prediction",
               "tro-choi/thu-tai-du-doan",
               "~/frontend/user/prediction.aspx"
              );

            routes.MapPageRoute(
            "question",
            "tro-choi/thu-tai-kien-thuc",
            "~/frontend/user/question.aspx"
           );

            routes.MapPageRoute(
               "betting",
               "tro-choi/thu-tai-phan-tich-tran-dau/{BettingId}",
               "~/frontend/user/betting.aspx"
              );

            routes.MapPageRoute(
               "bettingList",
               "tro-choi/thu-tai-phan-tich-tran-dau",
               "~/frontend/user/betting.aspx"
              );

            routes.MapPageRoute(
             "BettingSubmit",
             "tro-choi/dat-cuoc",
             "~/frontend/user/Submit.aspx"
            );
            #endregion
        }

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
