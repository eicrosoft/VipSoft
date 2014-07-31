using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipSoft.Membership.Core.Dao;
using VipSoft.Membership.Core.Entity;
using VipSoft.Membership.Core.Service;

namespace VipSoft.Membership.Service
{
    public class MenuService : AbstractService, IMenuService
    {
        public IUserDao UserDao { get; set; }//= Wac.GetObject("UserDao") as IUserDao;
        public IMenuDao MenuDao { get; set; }// = Wac.GetObject("MenuDao") as IMenuDao;


        public Menu GetMenu(Menu menu)
        {
            var list = GetCacheAllMenuList();
            return list.Find(p => p.ID == menu.ID);
        }

        public IList<Menu> GetMenuList(Menu menu)
        {                                  
            return MenuDao.GetList(menu);
        }

        public List<Menu> GetCacheAllMenuList()
        {  
            //return MenuDao.GetCacheList();    
            return MenuDao.GetList();
        }
    }
}
