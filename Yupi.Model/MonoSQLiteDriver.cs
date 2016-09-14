namespace Yupi.Model
{
    using System;

    public class MonoSQLiteDriver : NHibernate.Driver.ReflectionBasedDriver
    {
        #region Constructors

        public MonoSQLiteDriver()
            : base("Mono.Data.Sqlite",
                "Mono.Data.Sqlite",
                "Mono.Data.Sqlite.SqliteConnection",
                "Mono.Data.Sqlite.SqliteCommand")
        {
        }

        #endregion Constructors

        #region Properties

        public override string NamedPrefix
        {
            get { return "@"; }
        }

        public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }

        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        #endregion Properties
    }
}