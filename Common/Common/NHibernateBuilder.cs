using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Common
{
    public static class NHibernateBuilder
    {
        public static ISessionFactory BuildDefaultSession(this Configuration cfg)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);

            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=DESKTOP-NTNHKPL;Database=RedisApp;Trusted_Connection=True;";

                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
            });

            return cfg.BuildSessionFactory();
        }

    }
}
