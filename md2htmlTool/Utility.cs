using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md2htmlTool
{
    public static class Utility
    {
        public static string Multiple(this String str , int repeatCount)
        {
            if (String.IsNullOrEmpty(str) || repeatCount <= 0)
            {
                return "";
            }
            StringBuilder sbText = new StringBuilder();
            for (int i = 0; i < repeatCount; i++)
            {
                sbText.Append(str);
            }
            return sbText.ToString();
        }

        public static string GenerateMenuItem(this String title , int[] titleIndex)
        {
            StringBuilder sbText = new StringBuilder();
            int i = 0;
            while (titleIndex[i] != 0)
            {
                sbText.Append(titleIndex[i]);
                sbText.Append('.');
                i++;
            }
            string itemIndex = sbText.ToString().Substring(0 , sbText.Length - 1);
            sbText.Clear();
            sbText.AppendFormat("{0} {1}" , itemIndex , title);
            return sbText.ToString();
        }
    }
}
