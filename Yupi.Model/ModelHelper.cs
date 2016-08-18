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
using Yupi.Model.Repository;

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
			new SchemaUpdate (config).Execute (false, true);
		}

		// TODO Proper initial data
		public static void Populate() {
			PopulateObject(new NavigatorCategory () { Caption = "Test" });
			PopulateObject (new FlatNavigatorCategory () { Caption = "Test2" });
		}

		private static void PopulateObject<T>(T data) {
			IRepository<T> Repository = DependencyFactory.Resolve<IRepository<T>> ();
			Repository.Save (data);
		}
	}
}

