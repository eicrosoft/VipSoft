/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.0.51b-community-nt : Database - vipsoft_cms
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`vipsoft_cms` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;

USE `vipsoft_cms`;

/*Table structure for table `vipsoft_accezz` */

DROP TABLE IF EXISTS `vipsoft_accezz`;

CREATE TABLE `vipsoft_accezz` (
  `id` int(11) NOT NULL auto_increment,
  `src_id` int(11) default NULL,
  `level` int(11) default NULL,
  `description` varchar(500) collate utf8_unicode_ci default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_accezz` */

/*Table structure for table `vipsoft_article` */

DROP TABLE IF EXISTS `vipsoft_article`;

CREATE TABLE `vipsoft_article` (
  `id` int(11) NOT NULL auto_increment,
  `category_id` int(4) NOT NULL,
  `title` varchar(100) collate utf8_unicode_ci default NULL,
  `author` varchar(50) collate utf8_unicode_ci default NULL,
  `source` varchar(200) collate utf8_unicode_ci default NULL,
  `summary` varchar(1000) collate utf8_unicode_ci default NULL,
  `file_name` varchar(80) collate utf8_unicode_ci default NULL,
  `seo_title` varchar(500) collate utf8_unicode_ci default NULL,
  `seo_keywords` varchar(501) collate utf8_unicode_ci default NULL,
  `seo_description` varchar(502) collate utf8_unicode_ci default NULL,
  `link_url` varchar(100) collate utf8_unicode_ci default NULL,
  `content` text collate utf8_unicode_ci,
  `enable_link_url` tinyint(4) default NULL,
  `is_save_remote_pic` tinyint(4) default NULL,
  `agree_count` int(11) default NULL,
  `argue_count` int(11) default NULL,
  `visitation_count` int(11) default NULL,
  `attribute` varchar(50) collate utf8_unicode_ci default NULL,
  `sequence_rule` tinyint(4) default NULL,
  `status` int(11) default NULL,
  `create_date` datetime default NULL,
  `update_date` timestamp NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`),
  UNIQUE KEY `ID` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_article` */

insert  into `vipsoft_article`(id,category_id,title,author,source,summary,file_name,seo_title,seo_keywords,seo_description,link_url,content,enable_link_url,is_save_remote_pic,agree_count,argue_count,visitation_count,attribute,sequence_rule,status,create_date,update_date) values (1,0,'关于我们',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p><span style=\"color:#333333;font-family:helvetica, arial, freesans, clean, sans-serif;font-size:14px;line-height:22px;background-color:#ffffff;\"></span></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">VipSoft CMS（www.vipsoft.com.cn）是一个开源的CMS产品，面向软件开发者、程序爱好者，服务于个人、企业的网站，</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">VipSoft CMS 在不修改作者版权的情况下，可任意使用，有偿提供功能扩展服务。具体参考“<strong>授权服务</strong>” &nbsp;</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">本产品采用的技术框架为：ASP.NET MVC 3 + .Net Framework 4.0 + Spring.NET + NHibernate + MySQL</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">不为赚钱，只为技术交友，欢迎有兴趣的朋友一起<strong>加入我们的团队</strong></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">群：2686690 &nbsp; 苏州-阿军</p><p>2</p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-03-26 00:11:43','2013-11-06 23:37:22'),(2,5,'加入我们',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<h4 style=\"margin:0px 0px 15px;padding:0px;border:none;list-style-type:none;font-size:12px;line-height:1.2em;color:#414141;font-family:微软雅黑;background-color:#ffffff;\">VipSoft团队成员</h4><ul style=\"margin:0px;padding:0px;border:none;list-style:none;color:#414141;font-family:微软雅黑;font-size:14px;line-height:25.1875px;background-color:#ffffff;\"><li style=\"margin:0px;padding:0px;border:none;list-style:none;\"><p style=\"padding:0px;border:none;list-style-type:none;margin-top:0px;margin-bottom:0px;\"><strong style=\"margin:0px;padding:0px;\">上海-LVIN</strong></p><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\">个人介绍： UI设计师。</p><p style=\"padding:0px;border:none;list-style-type:none;margin-top:0px;margin-bottom:0px;\"><strong style=\"margin:0px;padding:0px;\">山东-疯子</strong></p><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\">个人介绍： 安全检测，服务器安全，.NET</p></li><li style=\"margin:0px;padding:0px;border:none;list-style:none;\"><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\">更多访问个人博客：<a href=\"http://rang.lu/\" style=\"text-decoration:none;color:#434343;\">http://rang.lu/</a></p></li><li style=\"margin:0px;padding:0px;border:none;list-style:none;\"><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\"><a href=\"http://rang.lu/\" style=\"text-decoration:none;color:#434343;\"></a><strong style=\"color:#414141;margin:0px;padding:0px;\">苏州-阿军</strong></p></li><li style=\"margin:0px;padding:0px;border:none;list-style:none;\"><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\">个人介绍： .NET,JAVA,Oracle</p><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\">个人爱好：写程序，双节棍，摄影；</p><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\"><strong style=\"color:#414141;margin:0px;padding:0px;\">宁波-小人物</strong><br /></p></li><li style=\"margin:0px;padding:0px;border:none;list-style:none;\"><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\">个人介绍：SEO优化师。</p><p class=\"graylink\" style=\"padding:0px;border:none;list-style-type:none;color:#868b6e;margin-top:0px;margin-bottom:10px;\">个人爱好：熟识PS运用技巧、FLASH AS、SEO优化、DIV+CSS；</p></li></ul><h4 style=\"margin:0px 0px 15px;padding:0px;border:none;list-style-type:none;font-size:12px;line-height:1.2em;color:#414141;font-family:微软雅黑;background-color:#ffffff;\"><br /></h4>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-03-26 00:11:43','2013-06-29 00:44:47'),(3,6,'授权服务',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>保留版权 源码可用在任何地方，否则请进行授权申请</p><p><br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-03-26 00:11:43','2013-05-19 00:38:23'),(5,8,'联系我们',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p><span style=\"color:#333333;font-family:helvetica, arial, freesans, clean, sans-serif;font-size:14px;line-height:22px;background-color:#ffffff;\"></span></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><span style=\"font-family:verdana;\"><span style=\"font-size:x-large;\"><strong><span style=\"font-size:20px;\">依软 VipSoft CMS</span></strong></span></span></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><br /></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><br /></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><strong><span style=\"font-size:larger;\"><span class=\"STYLE13\">办公地址</span></span></strong><span style=\"font-size:larger;\"><span class=\"STYLE13\">:苏州工业园区星湖街328号 创意产业园</span></span></p><p class=\"STYLE37\" style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><br /></p><p class=\"STYLE37\" style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><strong><span style=\"font-size:larger;\"><span class=\"STYLE54\">电话:</span></span></strong><span style=\"font-size:larger;\"><span class=\"STYLE54\">0512-xxxxxxxx</span></span></p><p class=\"STYLE37\" style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><strong><span style=\"font-size:larger;\"><span class=\"STYLE54\">传真:</span></span></strong><span style=\"font-size:larger;\"><span class=\"STYLE54\">0512-xxxxxxxx</span></span></p><p class=\"STYLE37\" style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><br /></p><p class=\"STYLE37\" style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><strong><span style=\"font-size:medium;\">QQ:</span></strong><span style=\"font-size:medium;\">47262947</span></p><p class=\"STYLE37\" style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><br /></p><p class=\"STYLE37\" style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-size:12px;line-height:18px;font-family:宋体, arial, verdana;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\"><strong><span style=\"font-size:medium;\">Q群:</span></strong><span style=\"font-size:medium;\">2686690</span></p><div><span style=\"font-size:medium;\"><br /></span></div><p><span style=\"color:#333333;font-family:helvetica, arial, freesans, clean, sans-serif;font-size:14px;line-height:22px;background-color:#ffffff;\"></span><br style=\"color:#333333;font-family:helvetica, arial, freesans, clean, sans-serif;font-size:14px;line-height:22px;background-color:#ffffff;\" /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-03-26 00:11:43','2013-04-05 20:15:28'),(6,0,'授权服务',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">VipSoft CMS（www.vipsoft.com.cn）是一个开源的CMS产品，面向软件开发者、程序爱好者，服务于个人、企业的网站，</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">VipSoft CMS 在不修改作者版权的情况下，可任意使用，有偿提供功能扩展服务。具体参考“<strong>授权服务</strong>” &nbsp;</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">本产品采用的技术框架为：ASP.NET MVC 3 + .Net Framework 4.0 + Spring.NET + NHibernate + MySQL</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">不为赚钱，只为技术交友，欢迎有兴趣的朋友一起<strong>加入我们的团队</strong></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">群：2686690 &nbsp; 苏州-阿军</p><p>2<br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-03-26 00:11:43','2013-11-06 23:37:52'),(7,2,'关于我们',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">VipSoft CMS（www.vipsoft.com.cn）是一个开源的CMS产品，面向软件开发者、程序爱好者，服务于个人、企业的网站，</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">VipSoft CMS 在不修改作者版权的情况下，可任意使用，有偿提供功能扩展服务。具体参考“<strong>授权服务</strong>” &nbsp;</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">本产品采用的技术框架为：ASP.NET MVC 3 + .Net Framework 4.0 + Spring.NET + NHibernate + MySQL</p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">不为赚钱，只为技术交友，欢迎有兴趣的朋友一起<strong>加入我们的团队</strong></p><p style=\"padding:0px;border:none;list-style-type:none;color:#434343;font-family:宋体, arial, verdana;font-size:12px;line-height:18px;background-color:#ffffff;margin-top:0px;margin-bottom:0px;\">群：2686690 &nbsp; 苏州-阿军</p><p><br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-03-26 00:11:43','2013-12-01 14:57:14'),(10,4,'案例',NULL,NULL,NULL,'/Uploads/20130406225253574.jpg',NULL,NULL,NULL,NULL,'<p>案例<br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-04-06 22:53:09','2013-04-06 22:53:09'),(11,4,'案例',NULL,NULL,NULL,'/Uploads/20130406225253574.jpg20130406225642866.jpg',NULL,NULL,NULL,NULL,'<p>案例<br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-04-06 22:56:44','2013-04-06 22:56:44'),(13,11,'首页联系我们',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>苏州工业园区星湖街328号 创意产业园</p><p>苏州-阿军</p><p>QQ:47262947</p><p>Q群:2686690</p><p>http://www.vipsoft.com.cn</p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-05-15 23:55:35','2014-05-10 14:29:15'),(14,10,'行业新闻-VipSoft CMS 依软开源系统',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>行业新闻-VipSoft CMS 依软开源系统<br /></p><p>苏州-阿军</p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-05-15 23:55:47','2013-05-15 23:55:47'),(15,12,'依软科技-网站建设，软件开发',NULL,'http://www.vipsoft.com.cn/Upload/eicrsoft.gif',NULL,NULL,NULL,NULL,NULL,'http://www.vipsoft.com.cn/',NULL,NULL,NULL,0,0,0,NULL,NULL,0,'2013-05-19 00:50:08','2013-05-19 00:56:55'),(16,9,'公司新闻',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>公司新闻<br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-11-17 20:39:24','2013-11-17 20:39:24'),(17,9,'asdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>sadf<br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-12-01 00:21:21','2013-12-01 00:21:21'),(18,9,'asdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>asdf<br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-12-01 00:21:37','2013-12-01 00:21:37'),(19,9,'将 artDialog 换成 layer,因为 artDialog 5.0 放弃了对 iframe的支持',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p><span style=\"font-family:微软雅黑;font-size:14px;\">将 artDialog 换成 layer,因为 artDialog 5.0 放弃了对 iframe的支持</span><br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-12-01 00:21:44','2013-12-01 00:29:09'),(23,9,'更换后台界面',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>将后台界面由 ligerUI 换民 bootstrap<br /></p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-12-01 00:27:41','2013-12-01 00:27:41'),(24,10,'某某',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'<p>村</p>',NULL,NULL,0,0,0,NULL,NULL,0,'2013-12-01 13:58:58','2013-12-01 13:58:58'),(25,12,'华园星城',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'http://www.215122.com',NULL,NULL,NULL,0,0,0,NULL,NULL,0,'2014-05-10 15:17:08','2014-05-10 15:17:08');

/*Table structure for table `vipsoft_category` */

DROP TABLE IF EXISTS `vipsoft_category`;

CREATE TABLE `vipsoft_category` (
  `id` int(11) NOT NULL auto_increment,
  `parent_id` int(4) NOT NULL default '0',
  `depth` int(4) NOT NULL default '0',
  `name` varchar(50) collate utf8_unicode_ci NOT NULL,
  `seo_title` varchar(500) collate utf8_unicode_ci default NULL,
  `seo_keywords` varchar(500) collate utf8_unicode_ci default NULL,
  `seo_description` varchar(500) collate utf8_unicode_ci default NULL,
  `url` varchar(200) collate utf8_unicode_ci default NULL,
  `sequence` int(4) NOT NULL default '0',
  `status` int(2) NOT NULL default '1',
  `category_type` varchar(20) collate utf8_unicode_ci default NULL,
  PRIMARY KEY  (`id`),
  UNIQUE KEY `ID` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_category` */

insert  into `vipsoft_category`(id,parent_id,depth,name,seo_title,seo_keywords,seo_description,url,sequence,status,category_type) values (1,0,1,'首页',NULL,NULL,NULL,'/',0,1,NULL),(2,0,1,'关于我们',NULL,NULL,NULL,'/Article/Content/2',0,1,NULL),(3,0,1,'最新动态',NULL,NULL,NULL,'/Article/List/3',0,1,NULL),(4,0,1,'成功案例',NULL,NULL,NULL,'/Article/Pic/4',0,1,NULL),(5,0,1,'加入我们',NULL,NULL,NULL,'/Article/Content/5',0,1,NULL),(6,0,1,'授权服务',NULL,NULL,NULL,'/Article/Content/6',0,1,NULL),(7,0,1,'在线留言',NULL,NULL,NULL,'/FeedBack/Add/7',0,1,NULL),(8,0,1,'联系我们',NULL,NULL,NULL,'/Article/Content/8',0,1,NULL),(9,3,2,'公司新闻',NULL,NULL,NULL,'/Article/List/9',0,1,NULL),(10,3,2,'行业新闻',NULL,NULL,NULL,'/Article/List/10',0,1,NULL),(11,0,0,'联系我们',NULL,NULL,NULL,NULL,0,0,NULL),(12,0,0,'友情连接',NULL,NULL,NULL,NULL,0,0,NULL),(15,15,0,'A12','','',NULL,NULL,0,0,NULL),(21,10,3,'最新技术',NULL,NULL,NULL,NULL,0,0,NULL);

/*Table structure for table `vipsoft_feedback` */

DROP TABLE IF EXISTS `vipsoft_feedback`;

CREATE TABLE `vipsoft_feedback` (
  `id` int(11) NOT NULL auto_increment,
  `title` varchar(50) collate utf8_unicode_ci NOT NULL,
  `name` varchar(50) collate utf8_unicode_ci default NULL,
  `content` varchar(500) collate utf8_unicode_ci default NULL,
  `tel` varchar(50) collate utf8_unicode_ci default NULL,
  `email` varchar(50) collate utf8_unicode_ci default NULL,
  `qq` varchar(20) collate utf8_unicode_ci default NULL,
  `create_date` datetime default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_feedback` */

insert  into `vipsoft_feedback`(id,title,name,content,tel,email,qq,create_date) values (1,'CMS功能不错','大虾','CMS功能不错','d','f','f','2014-03-26 00:24:39'),(2,'什么时候开发接口','应用者','什么时候开发接口','d','f','e','2014-03-26 00:24:39'),(3,'系统测试','Admin','测试内容','v','d','c','2013-03-26 00:24:39'),(4,'ad','fe','wfe','w','we','fe','2014-05-13 22:37:37'),(5,'a','b','ee','d','f','e','2014-05-13 22:38:11'),(6,'1','2','6','3','5','47262947','2014-05-13 22:47:28'),(7,'1','3','6','2','5','472629475','2014-05-13 22:48:58'),(8,'a','4','35','e','45','45','2014-05-13 22:51:31');

/*Table structure for table `vipsoft_menu` */

DROP TABLE IF EXISTS `vipsoft_menu`;

CREATE TABLE `vipsoft_menu` (
  `id` int(11) NOT NULL,
  `parent_id` int(4) default NULL,
  `depth` int(4) default NULL,
  `name` varchar(20) collate utf8_unicode_ci default NULL,
  `page_size` int(4) default '15',
  `category_id` int(4) default NULL,
  `category_type` int(4) default '0',
  `html_type` varchar(100) collate utf8_unicode_ci default NULL,
  `url` varchar(50) collate utf8_unicode_ci default '#',
  `status` int(4) default '1',
  `sequence` int(4) default '1',
  `create_date` datetime default NULL,
  `update_date` datetime default NULL,
  PRIMARY KEY  (`id`),
  UNIQUE KEY `ID` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_menu` */

insert  into `vipsoft_menu`(id,parent_id,depth,name,page_size,category_id,category_type,html_type,url,status,sequence,create_date,update_date) values (1,101,1,'关于我们',15,0,0,'','',1,1,NULL,NULL),(2,1,2,'关于我们',15,2,1,',10,','/VipSoft/Article/Add/2/2',1,1,NULL,NULL),(3,101,1,'最新动态',15,0,0,'','',1,1,NULL,NULL),(4,3,2,'最新动态',15,3,0,',0,1,2,3,4,5,6,7,8,9,10,','/VipSoft/Article/List/4/3',1,2,NULL,NULL),(5,101,1,'成功案例',15,0,0,'','',1,1,NULL,NULL),(6,5,2,'成功案例',15,4,0,',1,2,10,','/VipSoft/Article/List/6/4',1,1,NULL,NULL),(7,101,1,'加入我们',15,0,0,'','',1,1,NULL,NULL),(8,7,2,'加入我们',15,5,1,',10,','/VipSoft/Article/Add/8/5',1,1,NULL,NULL),(9,101,1,'授权服务',15,0,0,'','',1,1,NULL,NULL),(10,9,2,'授权服务',15,6,1,',10,','/VipSoft/Article/Add/10/6',1,1,NULL,NULL),(11,101,1,'在线留言',15,0,0,'','',1,1,NULL,NULL),(12,11,2,'在线留言',15,7,0,'','/VipSoft/FeedBack/List/12/7',1,1,NULL,NULL),(13,101,1,'联系我们',15,0,0,'','',1,1,NULL,NULL),(14,13,2,'联系我们',15,8,1,',10,','/VipSoft/Article/Add/14/8',1,1,NULL,NULL),(15,3,2,'分类管理',15,3,0,'','/VipSoft/Category/List/15/3',1,1,NULL,NULL),(16,101,1,'首页管理',15,NULL,0,NULL,NULL,1,1,NULL,NULL),(17,16,2,'联系我们',15,11,1,',10,','/VipSoft/Article/Add/17/11',1,1,NULL,NULL),(18,101,1,'友情链接',15,12,0,'','',1,1,NULL,NULL),(19,18,2,'链接管理',15,12,0,',1,3,9,','/VipSoft/Article/List/19/12',1,1,NULL,NULL),(101,0,0,'平台管理',15,0,0,'','',1,1,NULL,NULL),(201,0,0,'用户权限',15,NULL,0,NULL,'',1,1,NULL,NULL),(202,201,1,'用户管理',15,NULL,0,NULL,'',1,1,NULL,NULL),(203,202,2,'用户管理',15,NULL,0,NULL,'/VipSoft/User/List/',1,1,NULL,NULL),(204,201,1,'角色管理',15,NULL,0,NULL,'',1,1,NULL,NULL),(205,204,2,'角色管理',15,NULL,0,NULL,'/VipSoft/Role/LIST/',1,1,NULL,NULL),(206,201,1,'菜单管理',15,NULL,0,NULL,'',1,1,NULL,NULL),(207,206,2,'菜单设置',15,NULL,0,NULL,'/VipSoft/Menu/List/',1,1,NULL,NULL),(208,201,1,'权限管理',15,NULL,0,NULL,'',1,1,NULL,NULL),(209,208,2,'权限设置',15,NULL,0,NULL,'/VipSoft/Role/RoleAccess/',1,1,NULL,NULL),(210,NULL,NULL,NULL,15,NULL,0,NULL,'#',1,1,NULL,NULL),(301,NULL,NULL,'修改密码',15,NULL,0,NULL,'/VipSoft/User/ChangePassword',1,1,NULL,NULL);

/*Table structure for table `vipsoft_role` */

DROP TABLE IF EXISTS `vipsoft_role`;

CREATE TABLE `vipsoft_role` (
  `id` int(11) NOT NULL auto_increment,
  `code` varchar(50) collate utf8_unicode_ci NOT NULL,
  `name` varchar(50) collate utf8_unicode_ci NOT NULL,
  `description` varchar(500) collate utf8_unicode_ci default NULL,
  `status` int(2) NOT NULL,
  `create_date` datetime default NULL,
  `update_date` timestamp NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`),
  UNIQUE KEY `ID` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_role` */

insert  into `vipsoft_role`(id,code,name,description,status,create_date,update_date) values (2,'admin','管理员','有所有权限',1,NULL,NULL);

/*Table structure for table `vipsoft_role_access` */

DROP TABLE IF EXISTS `vipsoft_role_access`;

CREATE TABLE `vipsoft_role_access` (
  `role_code` varchar(50) collate utf8_unicode_ci NOT NULL,
  `access_id` int(11) NOT NULL,
  `org_code` varchar(50) collate utf8_unicode_ci default NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_role_access` */

/*Table structure for table `vipsoft_settings` */

DROP TABLE IF EXISTS `vipsoft_settings`;

CREATE TABLE `vipsoft_settings` (
  `id` int(11) NOT NULL auto_increment,
  `property_name` varchar(200) collate utf8_unicode_ci NOT NULL,
  `property_value` text collate utf8_unicode_ci,
  `org_code` varchar(50) collate utf8_unicode_ci default NULL,
  PRIMARY KEY  (`property_name`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_settings` */

insert  into `vipsoft_settings`(id,property_name,property_value,org_code) values (1,'SystemTitle','VipSoft CMS',NULL),(2,'SystemURL','ffff',NULL);

/*Table structure for table `vipsoft_user_role` */

DROP TABLE IF EXISTS `vipsoft_user_role`;

CREATE TABLE `vipsoft_user_role` (
  `user_name` varchar(50) collate utf8_unicode_ci NOT NULL,
  `role_code` varchar(50) collate utf8_unicode_ci NOT NULL,
  `org_code` varchar(50) collate utf8_unicode_ci default NULL,
  `type` int(2) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_user_role` */

/*Table structure for table `vipsoft_users` */

DROP TABLE IF EXISTS `vipsoft_users`;

CREATE TABLE `vipsoft_users` (
  `id` int(11) NOT NULL auto_increment,
  `user_name` varchar(20) collate utf8_unicode_ci NOT NULL,
  `password` varchar(20) collate utf8_unicode_ci NOT NULL,
  `real_name` varchar(20) collate utf8_unicode_ci default NULL,
  `nick_name` varchar(20) collate utf8_unicode_ci default NULL,
  `card_type` varchar(20) collate utf8_unicode_ci default NULL,
  `card_no` varchar(20) collate utf8_unicode_ci default NULL,
  `province` varchar(20) collate utf8_unicode_ci default NULL,
  `city` varchar(20) collate utf8_unicode_ci default NULL,
  `address` varchar(200) collate utf8_unicode_ci default NULL,
  `zip_code` varchar(6) collate utf8_unicode_ci default NULL,
  `tel` varchar(20) collate utf8_unicode_ci default NULL,
  `mobile` varchar(20) collate utf8_unicode_ci default NULL,
  `fax` varchar(20) collate utf8_unicode_ci default NULL,
  `email` varchar(100) collate utf8_unicode_ci default NULL,
  `qq` varchar(20) collate utf8_unicode_ci default NULL,
  `msn` varchar(100) collate utf8_unicode_ci default NULL,
  `question` varchar(100) collate utf8_unicode_ci default NULL,
  `answer` varchar(100) collate utf8_unicode_ci default NULL,
  `status` int(1) default '1',
  `create_date` datetime default NULL,
  `update_date` datetime default NULL,
  PRIMARY KEY  (`id`),
  UNIQUE KEY `ID` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `vipsoft_users` */

insert  into `vipsoft_users`(id,user_name,password,real_name,nick_name,card_type,card_no,province,city,address,zip_code,tel,mobile,fax,email,qq,msn,question,answer,status,create_date,update_date) values (10,'test','admin','测试','阿军',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL),(12,'VipSoft','******','苏州-阿军','苏州-阿军',NULL,'3212XXXXXXXX','江苏','苏州','苏州工业园区','215122','159********','159********','a','xx@vipsoft.com.cn','47262947',NULL,'A','B',1,'2013-04-24 23:53:15','0001-01-01 00:00:00');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
