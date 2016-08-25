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
using NHibernate.Util;

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
			db = GetSQLite ();
			return Fluently.Configure ()
				.Database (db)
				.Mappings (m =>
					m.AutoMappings
					.Add (AutoMap.AssemblyOf<ORMConfiguration> (cfg)
						.Conventions.Add<Conventions> ()
						.Conventions.Add<EnumTypeConvention> ()
						.Conventions.Add<IPAddressConvention> ()
						.Conventions.Add<VectorConvention> ()
						.Conventions.Add<CascadeConvention>()
						.IncludeBase<BaseItem> ()
						.IncludeBase<FloorItem> ()
						.IncludeBase<WallItem> ()
			))
				.ExposeConfiguration (BuildSchema)
				.BuildSessionFactory ();
		}

		private static IPersistenceConfigurer GetSQLite ()
		{
			if (MonoUtil.IsRunningOnMono ()) {
				return MonoSQLiteConfiguration.Standard
					.UsingFile (testFile).ShowSql ();
			} else {
				return SQLiteConfiguration.Standard.UsingFile (testFile).ShowSql ();
			}
		}

		private static void BuildSchema (Configuration config)
		{
			// TODO Use https://github.com/schambers/fluentmigrator/
			// @see http://stackoverflow.com/questions/5884359/fluent-nhibernate-create-database-schema-only-if-not-existing
			new SchemaUpdate (config).Execute (false, true);
		}

		// TODO Proper initial data
		public static void Populate ()
		{
			PopulateObject (
				new UserInfo () { UserName = "User" }, 
				new UserInfo () { UserName = "Admin", Rank = 9 }
			);

			PopulateObject (
				new OfficialNavigatorCategory () { Caption = "Test" }
			);

			PopulateObject (
				new FlatNavigatorCategory () { Caption = "Test2" }, 
				new FlatNavigatorCategory () { Caption = "Test1" }
			);
		}

		private static void PopulateObject<T> (params T[] data)
		{
			IRepository<T> Repository = DependencyFactory.Resolve<IRepository<T>> ();

			if (!Repository.All ().Any ()) {
				foreach (T obj in data) {
					Repository.Save (obj);
				}
			}
		}
	}
}

