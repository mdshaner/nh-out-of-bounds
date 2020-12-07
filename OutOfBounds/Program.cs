using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace OutOfBounds
{
    internal class Program
    {
        #region Private Methods

        private static Configuration CreateConfiguration(Assembly nhibernateAssembly)
        {
            var configuration = new Configuration();

            configuration.SetProperty(Environment.BatchSize, "0");
            configuration.SetProperty(Environment.ConnectionProvider,
                typeof(DriverConnectionProvider).AssemblyQualifiedName);
            configuration.SetProperty(Environment.ConnectionDriver, typeof(SqlClientDriver).AssemblyQualifiedName);
            configuration.SetProperty(Environment.Dialect, typeof(MsSql2012Dialect).AssemblyQualifiedName);
            configuration.AddAssembly(nhibernateAssembly);

            configuration.SetProperty(Environment.ConnectionString, "connection-string");
            configuration.SetProperty(Environment.ShowSql, "false");
            configuration.SetProperty(Environment.FormatSql, "false");

            return configuration;
        }

        private static void Main(string[] args)
        {
            var sessionFactory = CreateConfiguration(Assembly.GetExecutingAssembly()).BuildSessionFactory();
            new TestScenario(sessionFactory).Test();
        }

        #endregion
    }
}