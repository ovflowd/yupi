/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using MySql.Data.MySqlClient;
using Yupi.Data.Base.Connections;
using Yupi.Data.Base.Exceptions;
using Yupi.Data.Base.Sessions.Interfaces;

namespace Yupi.Data.Base.Sessions
{
    public class TransactionQueryReactor : QueryAdapter, IQueryAdapter
    {
        private bool _finishedTransaction;

        private MySqlTransaction _transactionmysql;

        public TransactionQueryReactor(IDatabaseClient client) : base(client)
        {
            InitTransaction();
        }

        public void Dispose()
        {
            if (!_finishedTransaction)
                throw new TransactionException("The transaction needs to be completed by commit() or rollback() before you can dispose this item.");

            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                default: // MySQL
                    CommandMySql.Dispose();
                    break;
            }

            Client.ReportDone();
        }

        public void DoCommit()
        {
            try
            {
                switch (ConnectionManager.DatabaseConnectionType.ToLower())
                {
                    default: // MySQL
                        _transactionmysql.Commit();
                        break;
                }

                _finishedTransaction = true;
            }
            catch (MySqlException ex)
            {
                throw new TransactionException(ex.Message);
            }
        }

        public void DoRollBack()
        {
            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                default:
                    try
                    {
                        _transactionmysql.Rollback();

                        _finishedTransaction = true;
                    }
                    catch (MySqlException ex)
                    {
                        throw new TransactionException(ex.Message);
                    }
                    break;
            }
        }

        public bool GetAutoCommit() => false;

        private void InitTransaction()
        {
            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                default:
                    CommandMySql = Client.CreateNewCommandMySql();
                    _transactionmysql = Client.GetTransactionMySql();
                    CommandMySql.Transaction = _transactionmysql;
                    CommandMySql.Connection = _transactionmysql.Connection;
                    break;
            }

            _finishedTransaction = false;
        }
    }
}