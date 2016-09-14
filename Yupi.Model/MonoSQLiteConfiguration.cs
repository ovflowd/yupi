using System;
using FluentNHibernate.Cfg.Db;
using NHibernate.Dialect;

namespace Yupi.Model
{
	public class MonoSQLiteConfiguration : PersistenceConfiguration<MonoSQLiteConfiguration>
	{
		public static MonoSQLiteConfiguration Standard
		{
			get { return new MonoSQLiteConfiguration(); }
		}

		public MonoSQLiteConfiguration()
		{
			Driver<MonoSQLiteDriver>();
			Dialect<SQLiteDialect>();
			Raw("query.substitutions", "true=1;false=0");
		}

		public MonoSQLiteConfiguration InMemory()
		{
			Raw("connection.release_mode", "on_close");
			return ConnectionString(c => c
				.Is("Data Source=:memory:;Version=3;New=True;"));

		}

		public MonoSQLiteConfiguration UsingFile(string fileName)
		{
			return ConnectionString(c => c
				.Is(string.Format("Data Source={0};Version=3;New=True;", fileName)));
		}

		public MonoSQLiteConfiguration UsingFileWithPassword(string fileName, string password)
		{
			return ConnectionString(c => c
				.Is(string.Format("Data Source={0};Version=3;New=True;Password={1};", fileName, password)));
		}
	}
}

