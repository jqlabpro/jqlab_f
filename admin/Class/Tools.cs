using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;

namespace icebear_v2.Class
{
    public class Tools
    {
        public static string link;

        private static string _link
        {
            get { return link; }
            set { link = value; }
        }

        public static string makelink(string link)
        {
            link = link.ToLower();
            link = link.Replace("İ", "i").Replace("ı", "i");
            link = link.Replace("Ğ", "g").Replace("ğ", "g");
            link = link.Replace("Ü", "u").Replace("ü", "u");
            link = link.Replace("Ş", "s").Replace("ş", "s");
            link = link.Replace("Ö", "o").Replace("ö", "o");
            link = link.Replace("Ç", "c").Replace("ç", "c");
            link = link.Replace("/", "-").Replace("\\", "-");
            link = link.Replace("'", "-").Replace("\"", "-");
            link = link.Replace("#", "-").Replace("$", "-");
            link = link.Replace("&", "-").Replace("{", "-");
            link = link.Replace("}", "-").Replace("?", "-");
            link = link.Replace("?", "-").Replace("!", "-");
            link = link.Replace("[", "-").Replace("]", "-");
            link = link.Replace("=", "-").Replace("*", "-");
            link = link.Replace("%", "-").Replace("½", "-");
            link = link.Replace("@", "-").Replace("^", "-");
            link = link.Replace(" ", "-").Replace(".", "-");
            link = link.Replace(":", "-").Replace("|", "-");

            link = link.Replace("  ", " ");
            return link;
        }

        public static string make_FileName(string link)
        {
            link = link.ToLower();
            link = link.Replace("İ", "i").Replace("ı", "i");
            link = link.Replace("Ğ", "g").Replace("ğ", "g");
            link = link.Replace("Ü", "u").Replace("ü", "u");
            link = link.Replace("Ş", "s").Replace("ş", "s");
            link = link.Replace("Ö", "o").Replace("ö", "o");
            link = link.Replace("Ç", "c").Replace("ç", "c");
            link = link.Replace("/", "_").Replace("\\", "_");
            link = link.Replace("'", "_").Replace("\"", "_");
            link = link.Replace("#", "_").Replace("$", "_");
            link = link.Replace("&", "_").Replace("{", "_");
            link = link.Replace("}", "_").Replace("?", "_");
            link = link.Replace("?", "_").Replace("!", "_");
            link = link.Replace("[", "_").Replace("]", "_");
            link = link.Replace("=", "_").Replace("*", "_");
            link = link.Replace("%", "_").Replace("½", "_");
            link = link.Replace("@", "_").Replace("^", "_");
            link = link.Replace(" ", "_");
            return link;
        }

        public static string make_FileName(string filename, string uzanti)
        {
            Random rgele = new Random();
            rgele.Next(11, 99);
            string fname = make_FileName(filename);
            fname = rgele.Next().ToString() + fname;
            return fname;
        }

        public static string FormatDate(DateTime? tarih, string bolme)
        {
            string desen = ConfigurationSettings.AppSettings["DateFormat"];

            string[] date = null;

            if (tarih.ToString().Contains("/"))
            {
                bolme = "/";
            }
            else if (tarih.ToString().Contains("."))
            {
                bolme = ".";
            }

            switch (bolme)
            {
                case ".":
                    {
                        if (tarih != null)
                            date = tarih.Value.ToShortDateString().Split('.');
                        break;
                    }
                case "/":
                    {
                        if (tarih != null)
                            date = tarih.Value.ToShortDateString().Split('/');
                        break;
                    }
            }

            if (tarih != null)
            {
                desen = desen.Replace("MM", date[0]);
                desen = desen.Replace("dd", date[1]);
                desen = desen.Replace("yyyy", date[2]);
            }
            else
            {
                desen = "";
            }

            return desen;
        }

        public static string StripHtml(string html)
        {
            html = Regex.Replace(html, "<[^>]*>", string.Empty);
            return html;
        }
    }
}