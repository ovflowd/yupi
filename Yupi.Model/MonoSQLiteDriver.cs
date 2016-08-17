using System;
using Mono.Data.Sqlite;

namespace Yupi.Model
{
	public class MonoSQLiteDriver : NHibernate.Driver.ReflectionBasedDriver  
	{  
		public MonoSQLiteDriver() 
			: base(
				"Mono.Data.Sqlite", 
				"Mono.Data.Sqlite", 
				"Mono.Data.Sqlite.SqliteConnection",  
				"Mono.Data.Sqlite.SqliteCommand")  
		{  
		}  

		public override bool UseNamedPrefixInParameter {  
			get {  
				return true;  
			}  
		}  

		public override bool UseNamedPrefixInSql {  
			get {  
				return true;  
			}  
		}  

		public override string NamedPrefix {  
			get {  
				return "@";  
			}  
		}  

		public override bool SupportsMultipleOpenReaders {  
			get {  
				return false;  
			}  
		}  
	}  
}

