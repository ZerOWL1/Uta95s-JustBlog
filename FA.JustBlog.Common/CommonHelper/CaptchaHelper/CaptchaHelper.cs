using System.Net;
using Newtonsoft.Json.Linq;

namespace FA.JustBlog.Common.CommonHelper.CaptchaHelper
{
    public class CaptchaHelper
    {
        public static bool CheckCaptcha(string response)
        {
            //gg secretKey
            const string secretKey = "";

            var client = new WebClient();
            var rs = client.DownloadString(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={response}");
            var obj = JObject.Parse(rs);
            return (bool) obj.SelectToken("success");
        }
    }
}