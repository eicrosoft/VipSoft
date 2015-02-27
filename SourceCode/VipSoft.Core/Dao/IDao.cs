// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDao.cs" company=""VipSoft.com.cn"">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:17-Dec-2012
// </copyright> 

using System.Collections.Generic;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;

namespace VipSoft.Core.Dao
{
    public interface IDao<T>
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entity">被添加的对象</param>
        /// <returns>1:成功，0：失败</returns>
        int Add(IEntity entity);

        /// <summary>
        /// 根据主健ID删除对象的值
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        int Delete(params object[] key);

        /// <summary>
        /// 根据 Conditaion 删除对象的值
        /// </summary>
        /// <param name="entity">Conditaion</param>
        /// <returns></returns>
        int Delete(IEntity entity);

        /// <summary>
        /// 根据主健Id修改对象的值
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        /// <returns></returns>
        int Update(IEntity entity);

        /// <summary>
        /// 根据主键获取对象 (T)
        /// </summary>
        /// <param name="key">主健Id</param>
        /// <returns>T</returns>
        T Get(object key);

        /// <summary>
        /// 根据 Criteria  获取对象
        /// </summary>
        /// <param name="criteria">条件</param>
        /// <returns></returns>
        T Get(IEntity entity);

        T Get(Criteria criteria);

        /// <summary>
        /// 得到所有的数据列表
        /// </summary>
        /// <returns>列表</returns>
        List<T> GetList();

        /// <summary>
        /// 根据 Conditaion 过虑列表数据
        /// </summary>
        /// <param name="entity">Conditaion</param>
        /// <returns></returns>
        List<T> GetList(IEntity entity);

        List<T> GetList(Criteria criteria, params Order[] order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="dataStart">从0开始</param>
        /// <param name="criteria"></param>
        /// <param name="totalItemCount">列表的总数</param>
        /// <param name="order"></param>
        /// <returns></returns>
        List<T> GetPageList(int pageSize, int dataStart, out int totalItemCount, Criteria criteria, params Order[] order);

        /// <summary>
        /// 得到所有数据的缓存
        /// </summary>
        /// <returns>列表</returns>
        List<T> GetCacheList();

        /// <summary>
        /// 得到第一行第一列的字段值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object GetScalar(string sql);
    }
}
