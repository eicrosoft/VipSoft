// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebHandler.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:28-Dec-2012
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VipSoft.WebUtility
{
    public class WebHandler
    {
        public static readonly char SpaceChar;

        static WebHandler()
        {
            SpaceChar = char.Parse(HttpUtility.HtmlDecode("&nbsp;"));
        }

        public static string SpaceString(int count)
        {
            return new string(SpaceChar, count);
        }
    }
}
