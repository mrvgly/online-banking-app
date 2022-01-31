using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GetirCase.Core.Utils
{
    public static class Helper
    {
        public static string PasswordHasher(string password)
        {
            var sha = new SHA1CryptoServiceProvider();

            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public static bool DateTimeChecker(DateTime startDate, DateTime endDate)
        {
            var result = DateTime.Compare(startDate, endDate);

            return result < 0;
        }

        public static DateTime ToDateTime(this string s, string format = "ddMMyyyy")
        {
            try
            {
                var r = DateTime.ParseExact(
                    s: s,
                    format: format,
                    provider: CultureInfo.GetCultureInfo("tr-TR"));

                return r;
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
        }

        public static bool IsValidJson(this string s)
        {
            bool response;

            try
            {
                JToken.Parse(s);
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }

            return response;
        }
    }
}