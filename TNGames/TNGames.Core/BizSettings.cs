using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNGames.Core
{
    public class BizSettings
    {
        public BizSettings()
        {
            SiteUrl = "http://games.thanhnien.com.vn";
            DefaultSender = "no-reply@thanhnien.com.vn";

            ActiveEmailSubject = "Kích hoạt tài khỏan";
            ActiveEmailTemplate = "Link kích hoạt $ActiveLink$";

            ResetEmailSubject = "Yêu cầu mật khẩu mới.";
            ResetEmailTemplate = "Mật khẩu mới của bạn $Password$";

            SmtpServer = "smtp.gmail.com";
            SmtpAuthentication = true;
            SmtpPort = 587;
            SmtpUsername = "quangthai04dtp3@gmail.com";
            SmtpPassword = "tuan1234567809";

            DefaultPoint = 100;
            HomeDisplayItem = 10;
        }

        public string SmtpServer
        {
            get;
            set;
        }

        public bool SmtpAuthentication
        {
            get;
            set;
        }

        public int SmtpPort
        {
            get;
            set;
        }

        public string SmtpUsername
        {
            get;
            set;
        }

        public string SmtpPassword
        {
            get;
            set;
        }

        public string ActiveEmailSubject
        {
            get;
            set;
        }

        public string ActiveEmailTemplate
        {
            get;
            set;
        }

        public string DefaultSender
        {
            get;
            set;
        }

        public string SiteUrl
        {
            get;
            set;
        }

        public string ResetEmailSubject
        {
            get;
            set;
        }

        public string ResetEmailTemplate
        {
            get;
            set;
        }

        public int DefaultPoint
        {
            get;
            set;
        }

        public int HomeDisplayItem
        {
            get;
            set;
        }
    }
}
