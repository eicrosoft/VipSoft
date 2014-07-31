using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VipSoft.WebUtility
{
    public static class HtmlExtensions
    {
        #region Span
        public static MvcHtmlString Span(this HtmlHelper helper, string strContent)
        {
            return Span(helper, null, strContent, null);
        }

        public static MvcHtmlString Span(this HtmlHelper helper, string strContent, object htmlAttributes)
        {
            return Span(helper, null, strContent, htmlAttributes);
        }

        public static MvcHtmlString Span(this HtmlHelper helper, string strId, string strContent, object htmlAttributes)
        {
            var objects = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            var tagBuilder = new TagBuilder("span");
            if (string.IsNullOrEmpty(strId)) tagBuilder.MergeAttribute("id", strId);
            tagBuilder.SetInnerText(strContent);
            tagBuilder.MergeAttributes<string, object>(objects);
            return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }
        #endregion


        #region IcoLink
        public static MvcHtmlString IcoLink(this HtmlHelper htmlHelper, string linkText, string href, string className)
        {
            var builder = new TagBuilder("a");
            builder.InnerHtml = string.Format("<span><b class=\"{1}\">{0}</b></span>", linkText, className);
            builder.MergeAttribute("href", href);
            builder.MergeAttribute("class", "tools_btn");
            return builder.ToMvcHtmlString(TagRenderMode.Normal);
        }

        //public static MvcHtmlString IcoLink(this HtmlHelper htmlHelper, string linkText, string actionName, string className)
        //{
        //    return htmlHelper.IcoLink(linkText, actionName, null, null, className);
        //}

        public static MvcHtmlString IcoLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, string className)
        {
            return htmlHelper.IcoLink(linkText, actionName, null, new RouteValueDictionary(routeValues), className);
        }

        public static MvcHtmlString IcoLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, string className)
        {
            return IcoLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, null, null, null, routeValues, className, true);
        }

        private static MvcHtmlString IcoLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, string className, bool includeImplicitMvcValues)
        {
            string str = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = string.Format("<span><b class=\"{1}\">{0}</b></span>", linkText, className);
            builder.MergeAttribute("href", str);
            builder.MergeAttribute("class", "tools_btn");
            return builder.ToMvcHtmlString(TagRenderMode.Normal);
        }


        /// <summary>
        /// 带onclick事件的A标签
        /// </summary>
        /// <param name="linkText">显示名</param>
        /// <param name="onclick">事件方法</param>
        /// <param name="className">样式</param>
        /// <returns></returns>
        public static MvcHtmlString IcoClick(this HtmlHelper htmlHelper, string linkText, string onclick, string className)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = string.Format("<span><b class=\"{1}\">{0}</b></span>", linkText, className);
            IDictionary<string,string> dicAttribute=new Dictionary<string, string>();
            dicAttribute.Add("href","javascript:;");
            dicAttribute.Add("onclick", onclick);
            dicAttribute.Add("class", "tools_btn");
            builder.MergeAttributes(dicAttribute);  
            return builder.ToMvcHtmlString(TagRenderMode.Normal);
        }
        #endregion

        #region ActionLink

        public static MvcHtmlString VipSoftActionLink(this HtmlHelper htmlHelper, string linkText, string href, string className)
        {
            var builder = new TagBuilder("a");
            builder.InnerHtml = linkText;
            builder.MergeAttribute("href", href);
            builder.MergeAttribute("class", className);
            return builder.ToMvcHtmlString(TagRenderMode.Normal);
        }

        #endregion

        static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }

    }
}
