using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VipSoft.Core.Entity;
using VipSoft.Data;
using VipSoft.Data.Config;
using VipSoft.Data.Persister;

namespace Demo
{
    public class UserService
    {
        public static readonly UserService Instance = new UserService();


        private readonly ISessionFactory sessionFaction;

        public UserService()
        {
            sessionFaction = Configuration.Configure.BuildSessionFactory();
        }


        public List<T> GetList<T>(IEntity model)
        {
            List<T> result;    
            using (ISession session = sessionFaction.OpenSession())
            {
                result = session.Load<T>(model);
                
                //string sql = "SELECT username,PASSWORD,orderno,customno FROM users u , stockinfo_zh s";
                //result = session.Load<T>(sql, CommandType.Text);
            }
            return result;
        }
    }


}