using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FA.JustBlog.Common.CommonHelper.UrlHelper
{
    public class UrlHelper
    {
        public static string FriendlyUrl(string title)
        {
            if (title == null) return "";

            const int maxLength = 80;
            var len = title.Length;
            var prevDash = false;
            var sb = new StringBuilder(len);

            for (var i = 0; i < len; i++)
            {
                var c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevDash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevDash = false;
                }
                else switch (c)
                {
                    case ' ':
                    case ',':
                    case '/':
                    case '\\':
                    case '-':
                    case '_':
                    case '=':
                    {
                        if (!prevDash && sb.Length > 0)
                        {
                            sb.Append('-');
                            prevDash = true;
                        }

                        break;
                    }
                    case '.':
                        sb.Append("dot");
                        break;
                    case '#':
                        sb.Append("sharp");
                        break;
                    default:
                    {
                        if ((int)c >= 128)
                        {
                            var prevLength = sb.Length;
                            sb.Append(RemapInternationalCharToAscii(c));
                            if (prevLength != sb.Length) prevDash = false;
                        }

                        break;
                    }
                }
                if (i == maxLength) break;
            }

            return prevDash ? sb.ToString().Substring(0, sb.Length - 1) : sb.ToString();
        }

        private static string RemapInternationalCharToAscii(char c)
        {
            var s = c.ToString().ToLowerInvariant();

            var mappings = new Dictionary<string, string>
        {
            { "a", "àåáâäãåąảạắặằẳăấầậẩ"},
            { "c", "çćčĉ" },
            { "d", "đ" },
            { "e", "èéêëęẹẻếềệể" },
            { "g", "ğĝ" },
            { "h", "ĥ" },
            { "i", "ìíîïıịỉ" },
            { "j", "ĵ" },
            { "l", "ł" },
            { "n", "ñń" },
            { "o", "òóôõöøőðơớợờỡốồộổọỏ"},
            { "r", "ř" },
            { "s", "śşšŝ" },
            { "ss", "ß" },
            { "th", "Þ" },
            { "u", "ùúûüŭůũủụ" },
            { "y", "ýÿỹỷỵỳ" },
            { "z", "żźž" }
        };

            foreach (var mapping in mappings.Where(mapping => mapping.Value.Contains(s)))
            {
                return mapping.Key;
            }

            return string.Empty;
        }
    }
}