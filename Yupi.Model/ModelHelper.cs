#region Header

// ---------------------------------------------------------------------------------
// <copyright file="ModelHelper.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------

#endregion Header

namespace Yupi.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using FluentMigrator.Runner.Announcers;
    using FluentMigrator.Runner.Initialization;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;
    using NHibernate.Util;

    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;
    using Yupi.Util.Settings;

    public class ModelHelper
    {
        #region Methods

        public static ISessionFactory CreateFactory()
        {
            return CreateFactory (GetConfig ());
        }

        public static ISessionFactory CreateFactory(Configuration config)
        {
            return config.BuildSessionFactory ();
        }

        public static Configuration GetConfig()
        {
            return GetConfig (GetDatabaseConfig ());
        }

        public static Configuration GetConfig(IPersistenceConfigurer db)
        {
            var cfg = new ORMConfiguration ();

            return Fluently.Configure ()
                .Database (db)
                .Mappings (m =>
                     m.AutoMappings
                         .Add (AutoMap.AssemblyOf<ORMConfiguration> (cfg)
                                 .Conventions.Add<ReferenceConventions> ()
                                 .Conventions.Add<EnumTypeConvention> ()
                                 .Conventions.Add<IPAddressConvention> ()
                                 .Conventions.Add<VectorConvention> ()
                                 .Conventions.Add<CascadeConvention> ()
                                 .Conventions.Add<RequiredConvention> ()
                                 .IncludeBase<BaseItem> ()
                                 .IncludeBase<FloorItem> ()
                                 .IncludeBase<WallItem> ()
                                 .IncludeBase<CatalogPageLayout> ()
                                 .IncludeBase<NavigatorCategory> ()
                              ))
                           .ExposeConfiguration(BuildSchema)
                           .BuildConfiguration();
        }

        // TODO Proper initial data
        public static void Populate()
        {
            PopulateObject(
                new UserInfo() { Name = "User" },
                new UserInfo() { Name = "Admin", Rank = 9 }
            );

            IEnumerable<IPopulate> instances = typeof(ModelHelper).Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IPopulate)))
                .Select(t => Activator.CreateInstance(t, true) as IPopulate);

            foreach (IPopulate populate in instances)
            {
                populate.Populate();
            }
        }

        public static void PopulateObject<T>(params T[] data)
            where T : class
        {
            IRepository<T> Repository = DependencyFactory.Resolve<IRepository<T>>();

            if (!Repository.All().Any())
            {
                foreach (T obj in data)
                {
                    Repository.Save(obj);
                }
            }
        }

        private static void BuildSchema(Configuration config)
        {
            SchemaMetadataUpdater.QuoteTableAndColumns(config);
            RunMigrations ();
        }

        private static IPersistenceConfigurer GetDatabaseConfig()
        {
            switch ((DatabaseType)DatabaseSettings.Type) {
            case DatabaseType.MySQL:
                return GetMySql ();
            case DatabaseType.SQLite:
                return GetSQLite ();
            default:
                throw new InvalidDataException ("Invalid database type");
            }
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
            {
                return MonoSQLiteConfiguration.Standard
                    .UsingFile(DatabaseSettings.Name + ".sqlite").ShowSql();
            }
            else
            {
                return SQLiteConfiguration.Standard.UsingFile(DatabaseSettings.Name + ".sqlite").ShowSql();
            }
        }

        private static void RunMigrations ()
        {
            var announcer = new ConsoleAnnouncer () {
                ShowSql = true
            };
            var runner = new RunnerContext (announcer) {
                Connection = DatabaseSettings.BuildConnectionString (),
                Database = Enum.GetName (typeof (DatabaseType), (DatabaseType)DatabaseSettings.Type).ToLower (),
                Targets = new string [] { typeof(ModelHelper).Assembly.Location },

            };

            new TaskExecutor (runner).Execute ();
        }

        #endregion Methods
    }
}