using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Net.Mail;
using TNGames.Core.Helper;
using System.Xml.Serialization;
using System.Xml;
using TNGames.Core.Domain;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using TNGames.Core.Cache;

namespace TNGames.Core.Helper
{
    public class Utils
    {
        #region String utilities
        /// <summary>
        /// Trims the left text.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <param name="ReadMoreText">The read more text.</param>
        /// <returns></returns>
        public static string TrimLeftText(string input, int length, string ReadMoreText)
        {
            if (input.Length <= ReadMoreText.Length + length)
                return input;
            else
                return input.Substring(0, length) + ReadMoreText;
        }

        /// <summary>
        /// Trims the left text and add ... for the missing text
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string TrimLeftText(string input, int length)
        {
            return TrimLeftText(input, length, "...");
        }

        /// <summary>
        /// Trims the left text, add the ... for missing text. Keep only first 25 characters
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string TrimLeftText(string input)
        {
            return TrimLeftText(input, 25);
        }
        public static string TrimLeftTextRemoveTag(string input, int length)
        {
            return TrimLeftText(CleanAllHtmlTag(input).Trim(), length);
        }

        public static string CleanAllHtmlTag(string inputString)
        {
            Regex regexObj = new Regex("</?([A-Z][A-Z0-9]*)[^>]*/?>|<!--.*-->", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regexObj.Replace(inputString, string.Empty);
        }

        /// <summary>
        /// Gets the alias format regular expression.
        /// </summary>
        /// <value>The alias format regular expression.</value>
        public static string AliasFormatRegularExpression
        {
            get
            {
                return "[a-zA-Z0-9][a-zA-Z0-9-_]+";
            }
        }


        /// <summary>
        /// Checks the alias is correct format for entire string.
        /// </summary>
        /// <param name="InputText">The input text.</param>
        /// <returns></returns>
        public static bool CheckAliasFormat(string InputText)
        {
            return CheckAliasFormat(InputText, true);
        }

        /// <summary>
        /// Checks the alias is correct format.
        /// </summary>
        /// <param name="InputText">The input text.</param>
        /// <param name="RequiredMatchEntiredString">if set to <c>true</c> validate on entire string, other, check on partial string.</param>
        /// <returns></returns>
        public static bool CheckAliasFormat(string InputText, bool RequiredMatchEntiredString)
        {
            bool IsValidFormat = false;
            string AliasRegEx = AliasFormatRegularExpression;

            // If you want to check the regular expression must match entired string, 
            // add \A at beginning and \Z at the end of expression
            // otherwise, the expression will return true if there is at least one partial match.
            if (RequiredMatchEntiredString)
                AliasRegEx = string.Format(@"\A{0}\Z", AliasRegEx);

            try
            {
                IsValidFormat = Regex.IsMatch(InputText, AliasRegEx);
            }
            catch (ArgumentException ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

            return IsValidFormat;
        }

        /// <summary>
        /// Generates the alias for input text.
        /// </summary>
        /// <param name="InputText">The input text.</param>
        /// <returns>Return the string that meets alias format. If input string contains only invalid characters, returns the empty string</returns>
        public static string GenerateAlias(string InputText)
        {
            string strAlias = string.Empty;

            var unicodeChars = "áéíóúñüÁÉÍÓÚÑæøåÆØÅåäöÅÄÖáàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            var replaceChars = "aeiounuAEIOUNaoaAOAaaoAAOaaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";

            try
            {
                int index = -1;

                while ((index = InputText.IndexOfAny(unicodeChars.ToCharArray())) != -1)
                {
                    int index2 = unicodeChars.IndexOf(InputText[index]);
                    InputText = InputText.Replace(InputText[index], replaceChars[index2]);
                }

                // Remove invalid character
                Regex ValidatorRegEx = new Regex(@"(^[^a-zA-Z0-9]+)|([^a-zA-Z0-9-_\s])");
                strAlias = ValidatorRegEx.Replace(InputText, " ");

                //Replace all space with _
                ValidatorRegEx = new Regex(@"\s+[-_]*");
                strAlias = ValidatorRegEx.Replace(strAlias, "-");
                strAlias = strAlias.Replace("--", "-");

                strAlias = strAlias.Trim(' ', '_', '-');
            }
            catch (ArgumentException ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

            return strAlias;
        }
        /// <summary>
        /// Generates the alias for input text.
        /// </summary>
        /// <param name="InputText">The input text.</param>
        /// <param name="ToLowerCase">Convert input text to lower case.</param>
        /// <returns>Return the string that meets alias format. If input string contains only invalid characters, returns the empty string</returns>
        public static string GenerateAlias(string InputText, bool ToLowerCase)
        {
            var result = GenerateAlias(InputText);
            return ToLowerCase ? result.ToLower() : result;
        }

        /// <summary>
        /// Resolves the client URL from Server relative path (Start with ~/ by the real path).
        /// </summary>
        /// <param name="RelativePath">The relative path.</param>
        /// <returns></returns>
        public static string ResolveClientUrl(string RelativePath)
        {
            if (RelativePath.StartsWith("~/"))
            {
                return GetApplicationPath() + RelativePath.Substring(2);
            }
            else
                return RelativePath;
        }


        /// <summary>
        /// Gets the application virtual path (include Slash at the end, example: /, /CatenoCMS/).
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationPath()
        {
            string AppPath = System.Web.HttpRuntime.AppDomainAppVirtualPath;
            if (!AppPath.EndsWith("/"))
                AppPath += "/";
            return AppPath;
        }

        public static string GetApplicationPhysicalPath()
        {
            string AppPath = System.Web.HttpRuntime.AppDomainAppPath;
            if (!AppPath.EndsWith("/"))
                AppPath += "/";
            return AppPath;
        }

        public static string MapPath(string RelativeFilePath)
        {
            if (!RelativeFilePath.StartsWith("~/"))
                throw new ArgumentException("Globals.MapPath(string RelativeFilePath) supports only relative path, begins with ~/. Argument is: " + RelativeFilePath);
            string result = GetApplicationPhysicalPath() + RelativeFilePath.Substring(2);

            result = Regex.Replace(result, "(/)+", @"\");
            result = Regex.Replace(result, @"\\{2,}", Path.DirectorySeparatorChar.ToString());
            return result;
        }

        public static bool CheckWebFileExisted(string RelativeFilePath)
        {
            return System.IO.File.Exists(MapPath(RelativeFilePath));
        }
        public static string CheckMissing(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "<font color=\"#E97C15\"> *Missing Resource </font>";
            else
                return s;
        }

        /// <summary>
        /// Compares the date.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>Return 0 if x and y have the same date. Return 1 if X is larger (in the future) else, return -1</returns>
        public static int CompareDate(DateTime? x, DateTime? y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return 1;
            if (y == null)
                return -1;
            if (x.Value.Year > y.Value.Year)
                return 1;
            if (x.Value.Year < y.Value.Year)
                return -1;
            //same year
            if (x.Value.DayOfYear > y.Value.DayOfYear)
                return 1;
            if (x.Value.DayOfYear < y.Value.DayOfYear)
                return -1;
            return 0;
        }
        #endregion

        #region User/Password

        public static void ResetCurrentUser()
        {
            TNHelper.RemoveRankingCaches();
        }

        public static User GetCurrentUser()
        {
            User user = null;
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                string key = string.Format("{0}-SessionId:{1}", TNHelper.LoginKey, ctx.Session.SessionID);
                // user = ctx.Session[TNHelper.LoginKey] as User;
                user = CMSCache.Get(key) as User;
                if (user != null)
                    return user;

                if (ctx.User != null && !string.IsNullOrEmpty(ctx.User.Identity.Name))
                {
                    user = TNHelper.GetUserByEmail(ctx.User.Identity.Name);
                    if (user != null)
                        CMSCache.Insert(key, user);
                    else
                    {
                        // ko tìm thấy thông tin user, redirect về tragn login
                        ctx.Response.Redirect("/dang-nhap?logout=true", true);
                    }
                }
            }

            return user;
        }

        public static string EncodePassword(string originalPassword)
        {
            if (!string.IsNullOrEmpty(originalPassword))
            {
                //Declarations
                Byte[] originalBytes;
                Byte[] encodedBytes;
                MD5 md5;

                //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
                md5 = new MD5CryptoServiceProvider();
                originalBytes = ASCIIEncoding.ASCII.GetBytes(originalPassword);
                encodedBytes = md5.ComputeHash(originalBytes);

                //Convert encoded bytes back to a 'readable' string
                string pass = BitConverter.ToString(encodedBytes);
                return pass.Replace("-", "").ToLower();
            }

            return string.Empty;
        }

        public static string GetNewPassword()
        {
            string pass = Guid.NewGuid().ToString();
            return pass.Replace("-", "").Substring(0, 10);
        }

        public static string GenerateNewActiveCode()
        {
            return Guid.NewGuid().ToString();
        }

        #endregion

        #region Date

        public static DateTime? GetDate(string strDate)
        {
            try
            {
                IFormatProvider format = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
                DateTime? date = !string.IsNullOrEmpty(strDate) ? new DateTime?(
                                        DateTime.ParseExact(strDate, "dd/MM/yyyy", format)) : null;
                return date;

            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Email

        public static bool SendEmail(string from, string to, string subject, string body)
        {
            string server = TNHelper.GetSettings().SmtpServer;
            string username = TNHelper.GetSettings().SmtpUsername;
            string password = TNHelper.GetSettings().SmtpPassword;
            bool authentication = TNHelper.GetSettings().SmtpAuthentication;
            int port = TNHelper.GetSettings().SmtpPort;

            MailMessage msg = new MailMessage();
            string[] addresses = to.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            msg.To.Clear();
            Regex regr = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            foreach (string adr in addresses)
            {
                if (regr.IsMatch(adr.Trim()))
                    msg.To.Add(adr);
            }

            msg.From = new MailAddress(from);
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.SubjectEncoding = Encoding.UTF8;

            SmtpClient mailClient = new SmtpClient();

            if (authentication)
            {
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential(username, password);
                mailClient.Credentials = credential;

                if (username.ToLower().Contains("gmail.com"))
                    mailClient.EnableSsl = true;
            }

            mailClient.Host = server;
            mailClient.Port = port;

            try
            {
                mailClient.Send(msg);
                return true;
            }
            catch (Exception exp)
            {
                System.Diagnostics.Trace.Write(exp.ToString());
                return false;
            }
        }

        public static string ResolveMessage(string template, User user)
        {
            string result = template;
            if (user != null && !string.IsNullOrEmpty(template))
            {
                string activeLink = string.Format("/kich-hoat-tai-khoan/{0}", user.ActiveCode);
                string siteUrl = TNHelper.GetSettings().SiteUrl;
                if (siteUrl.EndsWith("/"))
                    siteUrl = siteUrl.Substring(0, siteUrl.Length - 1);

                activeLink = string.Format("{0}{1}", siteUrl, activeLink);

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Replace("$SiteUrl$", TNHelper.GetSettings().SiteUrl);
                    result = result.Replace("$Username$", user.DisplayName);
                    result = result.Replace("$Password$", user.Password);
                    result = result.Replace("$ActiveLink$", activeLink);
                    result = result.Replace("$AcitveCode$", user.ActiveCode);
                }
            }
            return result;
        }

        #endregion

        #region SerializeObject/DeserializeObject

        public static string SerializeObject<T>(object pObject)
        {
            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                return XmlizedString;
            }
            catch (Exception e)
            {
#if DEBUG
                throw e;
#endif
                System.Diagnostics.Trace.Write(e.ToString());
            }
            return string.Empty;
        }

        public static object DeserializeObject<T>(string pXmlizedString)
        {
            try
            {
                if (!string.IsNullOrEmpty(pXmlizedString))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    return xs.Deserialize(memoryStream);
                }
            }
            catch (Exception exp)
            {
#if DEBUG
                throw exp;
#endif
                System.Diagnostics.Trace.Write(exp.ToString());
            }


            return null;
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        #endregion

        public static void ShowMessage(Control ctrl, string msg)
        {
            bool error = false;
            if (!string.IsNullOrEmpty(msg))
            {
                if (msg.ToLower().IndexOf("không") >= 0)
                    error = true;
            }

            if (ctrl is Label)
            {
                (ctrl as Label).Text = msg;
                (ctrl as Label).CssClass = error ? "error" : "success";
            }
            else if (ctrl is Literal)
                (ctrl as Literal).Text = msg;


        }
    }
}
