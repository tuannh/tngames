using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TNGames.Core
{
    public class TextInputUtil
    {
        public static string GetSafeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            Regex regx = new Regex(@"<script\b[^>]*>(.*?)<\/script>"); //<script.*?>.*?<\/script>
            return regx.Replace(input, "").Trim();
        }

        public static string GetSafeInput(string input, bool throwException)
        {
            string res = GetSafeInput(input);
            if (throwException && string.IsNullOrEmpty(res.Trim()))
            {
                throw new Exception("Input value is invalid!");
            }

            return res;
        }
    }
}
