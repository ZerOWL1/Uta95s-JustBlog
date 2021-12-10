using System;

namespace FA.JustBlog.Common.CommonHelper.PasswordHelper
{
    public static class PasswordHelper
    {
        public static string GeneratePasswordHelper()
        {
            var rd = new Random();

            var specialArr = new[]
            {
                "#",
                "$",
                "@",
                "!",
                "%",
                "^",
                "&",
                "*",
                "."
            };

            var specialChar = specialArr[rd.Next(specialArr.Length)];

            var number = rd.Next(999999);
            return $"UTA95S{number:000000}{specialChar}";
        }
    }
}