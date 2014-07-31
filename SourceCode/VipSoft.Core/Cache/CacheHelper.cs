// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICache.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:5-Nov-2012
// </copyright>
// <summary>
//   CacheHelper
// </summary>
// ---

using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace VipSoft.Core.Cache
{
    public class CacheHelper<T>
    {

        #region ICache<T> Members

        public static T GetCache(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        public static T GetCache(string key, T t)
        {
            var cache = HttpContext.Current.Cache;
            if (cache[key] != null)
            {
                return (T)cache[key];
            }
            SetCache(key, t);
            return t;

        }

        public T GetCache(string key, Func<T> f)
        {
            var cache = HttpContext.Current.Cache;
            if (cache[key] != null)
            {
                return (T)cache[key];
            }
            T t = f();
            SetCache(key, t);
            return t;

        }

        public static void SetCache(string key, object obj)
        {
            HttpRuntime.Cache.Insert(key, obj);
        }           

        public static void SetCache(string key, T t)
        {
            HttpContext.Current.Cache.Add(key, t, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
        }

        public static void SetCache(string key, object obj, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            if (slidingExpiration < TimeSpan.Zero)
            {
                slidingExpiration = TimeSpan.Zero;
            }
            HttpRuntime.Cache.Insert(key, obj, null, absoluteExpiration, slidingExpiration);
        }

        public static void CleanAll()
        {
            var cache = HttpRuntime.Cache;
            IDictionaryEnumerator enumerator = cache.GetEnumerator();
            var list = new ArrayList();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key);
            }

            foreach (string str in list)
            {
                cache.Remove(str);
            }

        }

        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        #endregion
    }
}
