using System;
using FluentNHibernate.Cfg;
using NHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg.Db;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;

namespace Yupi.Model
{
	public static class Test
	{
		private const string DbFile = "test.db";

		internal static void Main() {
			var cfg = new ORMConfiguration();


			using (ISessionFactory sessionFactory = Fluently.Configure ()
				.Database (SQLiteConfiguration.Standard
					.UsingFile (DbFile))
				.Mappings (m =>
					m.AutoMappings
					.Add (AutoMap.AssemblyOf<ORMConfiguration> (cfg)
						.Conventions.Add<Conventions> ()
			                                       )
			                                       )
				.ExposeConfiguration (BuildSchema)
				.BuildSessionFactory ()) {

				Console.ReadLine ();
			}

		}

		private static void BuildSchema(Configuration config)
		{
			// delete the existing db on each run
			if (File.Exists(DbFile))
				File.Delete(DbFile);

			// this NHibernate tool takes a configuration (with mapping info in)
			// and exports a database schema from it
			new SchemaExport(config)
				.Create(true, false);
		}
	}
}

