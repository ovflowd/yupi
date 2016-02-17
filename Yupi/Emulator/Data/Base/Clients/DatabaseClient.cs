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

using System.Data;
using MySql.Data.MySqlClient;
using Yupi.Emulator.Data.Base.Adapters;
using Yupi.Emulator.Data.Base.Adapters.Handlers;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Data.Base.Clients.Interfaces;

namespace Yupi.Emulator.Data.Base.Clients
{
    public class DatabaseClient : IDatabaseClient
    {
        protected readonly IQueryAdapter Adapter;

        protected readonly MySqlConnectionHandler ConnectionHandler;

        public DatabaseClient(string connectionStr)
        {
            ConnectionHandler = new MySqlConnectionHandler(connectionStr);

            Adapter = new NormalQueryAdapter(this);     
        }

        public void Disconnect()
        {
            lock (ConnectionHandler.GetConnection())
            {
                if (ConnectionHandler?.GetConnection()?.State == ConnectionState.Open)
                {
                    try
                    {
                        ConnectionHandler?.GetConnection()?.Close();
                    }
                    finally
                    {
                        ConnectionHandler?.SetClosed();
                    }
                }
            }
        }

        public void Connect()
        {
            lock (ConnectionHandler.GetConnection())
            {
                if (ConnectionHandler?.GetConnection()?.State == ConnectionState.Closed)
                {
                    try
                    {
                        ConnectionHandler?.GetConnection()?.Open();
                    }
                    finally
                    {
                        ConnectionHandler?.SetOpened();
                    }
                }
            }
        }

        public void Dispose()
        {
            Adapter?.Dispose();

            ConnectionHandler?.Dispose();
        }

        public IQueryAdapter GetQueryReactor() => Adapter;

        public bool IsAvailable() => GetConnectionHandler()?.GetState() == ConnectionState.Open;

        public MySqlCommand CreateCommand() => GetConnectionHandler()?.GetConnection()?.CreateCommand();

        public MySqlConnectionHandler GetConnectionHandler() => ConnectionHandler;

        public void ReportDone()
        {
            
        }
    }
}