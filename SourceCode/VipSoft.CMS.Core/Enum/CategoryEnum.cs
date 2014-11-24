// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryType.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:24-Mar-2013
// </copyright>
namespace VipSoft.CMS.Core.Enum
{
    /// <summary>
    /// 界面上的HTML元素，用来判断是否显示在界面上
    /// </summary>
    public enum CategoryType
    {
        Content = 1,
        List
    }

    public enum CategoryElement
    {
        ParentId = 0,
        Depth = 1,
        Name = 2,
        CategoryType = 3,
        Thumbnail = 4,          //缩略图
        Seo = 5,
        SeoKeywords = 6,
        SeoDescription = 7,
        Url = 8,
        Sequence = 9,
        Status = 10,
        DepthDescription = 11
    }
}
