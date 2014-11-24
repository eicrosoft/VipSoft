// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleElement.cs" company="VipSoft.com.cn">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:14-Mar-2013
// </copyright>
namespace VipSoft.CMS.Core.Enum
{
    /// <summary>
    /// 界面上的HTML元素，用来判断是否显示在界面上
    /// </summary>
    public enum ArticleElement
    {
        CategoryId = 0,         //分类
        Title = 1,              //标题
        Author = 2,             //作者
        Source = 3,             //来源
        Summary = 4,            //导读
        FileName = 5,           //文件名
        SeoTitle = 6,           //SEO标题
        SeoKeywords = 7,        //SEO关键字
        SeoDescription = 8,     //SEO简介
        LinkUrl = 9,            //外部连接地址
        Content = 10,            //内容
        EnableLinkUrl = 11,      //是否启用外部连接
        IsSaveRemotePic = 12,    //是否保存远程图片
        AgreeCount = 13,         //赞成人数
        ArgueCount = 14,         //反对人数
        VisitationCount = 15,    //浏览次数
        Attribute = 16,          //特性
        SequenceRule = 17,       //排序规则
        Status = 18,             //状态
        CreateDate = 19,         //创建时间
        UpdateDate = 20          //修改时间
    }
}







