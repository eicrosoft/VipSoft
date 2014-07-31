using System.Configuration;
using VipSoft.Core.Config;

namespace VipSoft.Data.Config
{
    [ConfigurationCollection(typeof(SessionElement))]
    public sealed class SessionElementCollection : ConfigurationElementCollection
    {
        public SessionElementCollection()
        {
            var session = (SessionElement)CreateNewElement();
            Add(session);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SessionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SessionElement)element).Name;
        }

        public SessionElement this[int index]
        {
            get
            {
                return (SessionElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        new public SessionElement this[string name]
        {
            get
            {
                return (SessionElement)BaseGet(name);
            }
        }

        public int IndexOf(SessionElement session)
        {
            return BaseIndexOf(session);
        }

        public void Add(SessionElement session)
        {
            BaseAdd(session);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(SessionElement session)
        {
            if (BaseIndexOf(session) >= 0)
            {
                BaseRemove(session.Name);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}