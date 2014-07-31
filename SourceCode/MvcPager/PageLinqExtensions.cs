﻿/*
 ASP.NET MvcPager 分页组件
 Copyright:2009-2011 陕西省延安市吴起县 杨涛\Webdiyer (http://www.webdiyer.com)
 Source code released under Ms-PL license
 */

using System.Collections.Generic;
using System.Linq;

namespace Webdiyer.WebControls.Mvc
{
    public static class PageLinqExtensions
    {
        public static PagedList<T> ToPagedList<T>
            (
                this IQueryable<T> allItems,
                int pageIndex,
                int pageSize
            )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            var totalItemCount = allItems.Count();
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }


        public static PagedList<T> ToPagedList<T>
            (
                this IList<T> allItems,
                int pageIndex,
                int pageSize
            )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            var totalItemCount = allItems.Count();
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }
    }
}
