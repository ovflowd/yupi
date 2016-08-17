using System;
using FluentNHibernate.Cfg.Db;
using Yupi.Util;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;
using Yupi.Model.Domain;
using NHibernate.Cfg;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace Yupi.Model
{
	public class ModelHelper
	{
		private const string testFile = "test.sqlite";

		public static ISessionFactory CreateFactory ()
		{
			var cfg = new ORMConfiguration ();

			IPersistenceConfigurer db;
			//db = MySQLConfiguration.Standard.ConnectionString(x => x.Server("localhost").Username("yupi").Password("changeme").Database("yupi"));
			db = GetSQLite();
			return Fluently.Configure ()
				.Database (db)
				.Mappings (m =>
					m.AutoMappings
					.Add (AutoMap.AssemblyOf<ORMConfiguration> (cfg)
						.Conventions.Add<Conventions> ()
						.Conventions.Add<EnumTypeConvention> ()
						.Conventions.Add<IPAddressConvention> ()
						.IncludeBase<BaseItem> ()
						.IncludeBase<FloorItem> ()
						.IncludeBase<WallItem> ()
			                                        ))
				.ExposeConfiguration (BuildSchema)
				.BuildSessionFactory ();
		}

		private static IPersistenceConfigurer GetSQLite() {
			if (MonoUtil.IsRunningOnMono ()) {
				return MonoSQLiteConfiguration.Standard
					.UsingFile(testFile).ShowSql();
			} else {
				return SQLiteConfiguration.Standard.UsingFile(testFile).ShowSql();
			}
		}

		private static void BuildSchema (Configuration config)
		{
			if (File.Exists (testFile)) {
				return;
			}
			// this NHibernate tool takes a configuration (with mapping info in)
			// and exports a database schema from it
			new SchemaExport (config)
				.Create (false, true);
		}
	}
}

