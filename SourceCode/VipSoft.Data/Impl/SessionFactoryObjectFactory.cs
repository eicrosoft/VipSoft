using System.Collections.Generic;

namespace VipSoft.Data.Impl
{
    /// <summary>
    /// Resolves <see cref="ISessionFactory"/> lookups and deserialization.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is used heavily be Deserialization.  Currently a SessionFactory is not really serialized. 
    /// All that is serialized is it's name and uid.  During Deserializaiton the serialized SessionFactory
    /// is converted to the one contained in this object.  So if you are serializing across AppDomains
    /// you should make sure that "name" is specified for the SessionFactory in the hbm.xml file and that the
    /// other AppDomain has a configured SessionFactory with the same name.  If
    /// you are serializing in the same AppDomain then there will be no problem because the uid will
    /// be in this object.
    /// </para>
    /// </remarks>
    public static class SessionFactoryObjectFactory
    {

        // in h2.0.3 these use a class called "FastHashMap"
        private static readonly IDictionary<string, ISessionFactory> Instances = new Dictionary<string, ISessionFactory>();
        private static readonly IDictionary<string, ISessionFactory> NamedInstances = new Dictionary<string, ISessionFactory>();

        /// <summary>
        /// Adds an Instance of the SessionFactory to the local "cache".
        /// </summary>
        /// <param name="uid">The identifier of the ISessionFactory.</param>
        /// <param name="name">The name of the ISessionFactory.</param>
        /// <param name="instance">The ISessionFactory.</param>
        public static void AddInstance(string uid, string name, ISessionFactory instance)
        {
            Instances[uid] = instance;
            if (!string.IsNullOrEmpty(name)) NamedInstances[name] = instance;
        }

        /// <summary>
        /// Removes the Instance of the SessionFactory from the local "cache".
        /// </summary>
        /// <param name="uid">The identifier of the ISessionFactory.</param>
        /// <param name="name">The name of the ISessionFactory.</param>
        public static void RemoveInstance(string uid, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                NamedInstances.Remove(name);
            }
            Instances.Remove(uid);
        }

        /// <summary>
        /// Returns a Named Instance of the SessionFactory from the local "cache" identified by name.
        /// </summary>
        /// <param name="name">The name of the ISessionFactory.</param>
        /// <returns>An instantiated ISessionFactory.</returns>
        public static ISessionFactory GetNamedInstance(string name)
        {
            ISessionFactory factory;
            NamedInstances.TryGetValue(name, out factory);
            return factory;
        }

        /// <summary>
        /// Returns an Instance of the SessionFactory from the local "cache" identified by UUID.
        /// </summary>
        /// <param name="uid">The identifier of the ISessionFactory.</param>
        /// <returns>An instantiated ISessionFactory.</returns>
        public static ISessionFactory GetInstance(string uid)
        {
            ISessionFactory factory;
            Instances.TryGetValue(uid, out factory);
            return factory;
        }
    }
}