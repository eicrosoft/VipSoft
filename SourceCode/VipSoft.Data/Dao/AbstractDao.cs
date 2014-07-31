// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractDao.cs" company=""VipSoft.com.cn"">
//    Author:Chen,Jun
//        QQ:47262947
//     Email:chenjun@VipSoft.com.cn
//    Create:25-Nov-2012
// </copyright> 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using VipSoft.Core.Cache;
using VipSoft.Core.Dao;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data.Config;

namespace VipSoft.Data.Dao
{
    public abstract class AbstractDao<T> : IDao<T>
    {
        protected ISessionFactory SessionFaction;

        private readonly IEntity _entity;

        protected AbstractDao()
        {
            _entity = (IEntity)Activator.CreateInstance(typeof(T));
            SessionFaction = Configuration.Configure.BuildSessionFactory();
        }


        public virtual int Add(IEntity entity)
        {
            int result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.Insert(entity);
            }
            return result;
        }


        public virtual int Delete(params int[] id)
        {
            int result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.Delete<T>(id);
            }
            return result;
        }

        public virtual int Delete(IEntity entity)
        {
            int result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.Delete(entity);
            }
            return result;
        }

        public virtual int Update(string sql)
        {
            int result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.ExecuteNonQuery<T>(sql, CommandType.Text);
            }
            return result;
        }

        public virtual int Update(IEntity entity)
        {
            int result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.Update(entity);
            }
            return result;
        }

        public virtual T Get(int id)
        {
            T result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.Get<T>(id);
            }
            return result;
        }

        public virtual T Get(IEntity entity)
        {
            T result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.Get<T>(entity);
            }
            return result;
        }

        public virtual T Get(Criteria criteria)
        {
            T result;
            using (ISession session = SessionFaction.OpenSession())
            {
                result = session.Get<T>(criteria);
            }
            return result;
        }


        public virtual List<T> GetList()
        {
            List<T> result;
            using (var session = SessionFaction.OpenSession())
            {
                result = session.Load<T>(_entity);
            }
            return result;
        }

        public List<T> GetList(Criteria criteria, params Order[] order)
        {
            List<T> result;
            using (var session = SessionFaction.OpenSession())
            {
                result = order != null && order.Length > 0
                             ? session.Load<T>(criteria, order[0])
                             : session.Load<T>(criteria);
            }
            return result;
        }



        public virtual List<T> GetList(string sql)
        {
            List<T> result;
            using (var session = SessionFaction.OpenSession())
            {
                result = session.Load<T>(sql, CommandType.Text);
            }
            return result;
        }

        public virtual List<T> GetList(IEntity entity)
        {
            List<T> result;
            using (var session = SessionFaction.OpenSession())
            {
                result = session.Load<T>(entity);
            }
            return result;
        }

        public virtual List<T> GetCacheList()
        {
            var key = typeof(List<T>).ToString();
            var obj = CacheHelper<List<T>>.GetCache(key);
            if (obj == null)
            {
                obj = GetList();
                CacheHelper<List<T>>.SetCache(key, obj);
            }
            return obj;
        }
    }
}
