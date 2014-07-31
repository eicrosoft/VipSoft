using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VipSoft.Core.Utility
{
    public static class ConvertHandler
    {
        public static int ToInt32(object value)
        {
            var num = 0;
            if (value is int)
            {
                num = Convert.ToInt32(value);
            }
            return num;
        }

        public static string ToString(int[] values)
        {
            var result = new StringBuilder();
            var count = values.Length;
            if (count > 1)
            {

                for (var i = 0; i < count; i++) result.AppendFormat("{0},", values[i]);
            }
            else
            {
                result.Append(values[0]);
            }
            return result.ToString().TrimEnd(',');
        }
    }
}
