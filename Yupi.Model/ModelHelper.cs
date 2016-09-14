using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Util;
using Yupi.Util.Settings;

namespace Yupi.Model
{
    public class ModelHelper
    {
        public static ISessionFactory CreateFactory()
        {
            var cfg = new ORMConfiguration();

            IPersistenceConfigurer db;

            switch ((DatabaseType) DatabaseSettings.Type)
            {
                case DatabaseType.MySQL:
                    db = GetMySql();
                    break;
                case DatabaseType.SQLite:
                    db = GetSQLite();
                    break;
                default:
                    throw new InvalidDataException("Invalid database type");
            }


            return Fluently.Configure()
                .Database(db)
                .Mappings(m =>
                    m.AutoMappings
                        .Add(AutoMap.AssemblyOf<ORMConfiguration>(cfg)
                                .Conventions.Add<Conventions>()
                                .Conventions.Add<EnumTypeConvention>()
                                .Conventions.Add<IPAddressConvention>()
                                .Conventions.Add<VectorConvention>()
                                .Conventions.Add<CascadeConvention>()
                                .IncludeBase<BaseItem>()
                                .IncludeBase<FloorItem>()
                                .IncludeBase<WallItem>()
                        ))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static IPersistenceConfigurer GetMySql()
        {
            return MySQLConfiguration.Standard
                .ConnectionString(x =>
                        x.Server(DatabaseSettings.Host)
                            .Username(DatabaseSettings.Username)
                            .Password(DatabaseSettings.Password)
                            .Database(DatabaseSettings.Name)
                );
        }

        private static IPersistenceConfigurer GetSQLite()
        {
            if (MonoUtil.IsRunningOnMono())
                return MonoSQLiteConfiguration.Standard
                    .UsingFile(DatabaseSettings.Name + ".sqlite").ShowSql();
            return SQLiteConfiguration.Standard.UsingFile(DatabaseSettings.Name + ".sqlite").ShowSql();
        }

        private static void BuildSchema(Configuration config)
        {
            // TODO Use https://github.com/schambers/fluentmigrator/
            // @see http://stackoverflow.com/questions/5884359/fluent-nhibernate-create-database-schema-only-if-not-existing

            SchemaMetadataUpdater.QuoteTableAndColumns(config);
            new SchemaUpdate(config).Execute(false, true);
        }

        // TODO Proper initial data
        public static void Populate()
        {
            PopulateObject(
                new UserInfo {Name = "User"},
                new UserInfo {Name = "Admin", Rank = 9}
            );

            PopulateObject(
                new OfficialNavigatorCategory {Caption = "Test"}
            );

            PopulateObject(
                new FlatNavigatorCategory {Caption = "Test2"},
                new FlatNavigatorCategory {Caption = "Test1"}
            );
        }

        private static void PopulateObject<T>(params T[] data)
        {
            var Repository = DependencyFactory.Resolve<IRepository<T>>();

            if (!Repository.All().Any()) foreach (var obj in data) Repository.Save(obj);
        }
    }
}