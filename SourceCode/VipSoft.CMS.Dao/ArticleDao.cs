using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipSoft.CMS.Core.Dao;
using VipSoft.CMS.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data.Dao;

namespace VipSoft.CMS.Dao
{
    public class ArticleDao : AbstractDao<Article>, IArticleDao
    {
        public int UpdateVisitationCount(int articleId)
        {
            throw new NotImplementedException();
        }

        public int UpdateAgreeCount(int articleId)
        {
            throw new NotImplementedException();
        }

        public int UpdateArgueCount(int articleId)
        {
            throw new NotImplementedException();
        }

        public int BatchUpdateArticle(int[] articleId)
        {
            throw new NotImplementedException();
        }

       public IList<Article> GetList(int[] categoryIds)
       {
           var idStr = ConvertHandler.ToString(categoryIds);
           var sql = string.Format("SELECT * FROM vipsoft_article WHERE category_id in ({0}) ORDER BY update_date DESC,id DESC", idStr);
           return GetList(sql);
       }



        
    }
}
