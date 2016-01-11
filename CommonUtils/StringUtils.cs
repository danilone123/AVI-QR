    using System;
    using System.Collections.Generic;
    using System.Text;

namespace CommonUtils
{
    public class StringUtils
    {
        public static string EscapeSQL(string str)
        {
            if (str == null)
            {
                str = "";
            }
            return (str.Replace("'", "''"));
        }

        public static string EscapeHTML(string str)
        {
            if (str == null)
            {
                str = "";
            }
            str = str.Replace("'", "&#39;");
            return (str.Replace("\"", "&quot;"));
        }

        public static string EscapeJScript(string str)
        {
            if (str == null)
            {
                str = "";
            }
            str = str.Replace("\"", "\\\"");
            return (str.Replace("'", "\\'"));
        }

        public static string EscapeCharacter(string str)
        {
            if (str == null)
            {
                str = "";
            }
            return (str.Replace("\"", "&#34;"));
        }
    }
}


