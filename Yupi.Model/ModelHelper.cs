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
		private const string DbFile = "test.db";

		public static ISessionFactory CreateFactory ()
		{
			var cfg = new ORMConfiguration ();

			IPersistenceConfigurer db;
			if (MonoUtil.IsRunningOnMono ()) {
				db = MonoSQLiteConfiguration.Standard
					.UsingFile (DbFile);
			} else {
				db = SQLiteConfiguration.Standard
					.UsingFile (DbFile);
			}

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

		private static void BuildSchema (Configuration config)
		{
			// delete the existing db on each run
			if (File.Exists (DbFile))
				File.Delete (DbFile);

			// this NHibernate tool takes a configuration (with mapping info in)
			// and exports a database schema from it
			new SchemaExport (config)
				.Create (true, false);
		}
	}
}

