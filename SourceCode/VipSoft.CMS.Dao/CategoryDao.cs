// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryDao.cs" company=""VipSoft.com.cn"">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:25-Nov-2012
// </copyright>    

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using VipSoft.CMS.Core.Dao;
using VipSoft.CMS.Core.Entity;
using VipSoft.Data;
using VipSoft.Data.Dao;

namespace VipSoft.CMS.Dao
{
    public class CategoryDao : AbstractDao<Category>, ICategoryDao
    {


        public int SetCategorySequence(int categoryId, int sequence)
        {
            return 1;
        }

        public int BatchSetCategory(int[] categoryId, int values)
        {
            return 1;
        }

        public List<Category> GetBrotherNode(int id)
        {
            List<Category> result;
            using (var session = SessionFaction.OpenSession())
            {                
                var parameter = session.GetParameters<Category>("id");
                parameter[0].Value = id;
                var sql = string.Format(@"SELECT * FROM vipsoft_category A WHERE EXISTS (SELECT '1' FROM vipsoft_category B WHERE A.parent_id=B.parent_id AND A.STATUS=1 AND B.id={0})", parameter[0].ParameterName);
                result = session.Load<Category>(sql, parameter);
            }
            return result;
        }
    }
}
