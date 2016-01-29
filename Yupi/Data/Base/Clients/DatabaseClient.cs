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

using System;
using System.ComponentModel.Design;
using System.Data;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Clients.Interfaces;

namespace Yupi.Data.Base.Clients
{
    public class DatabaseClient : IDatabaseClient
    {
        protected readonly IQueryAdapter Adapter;

        protected ConnectionState InternalState = ConnectionState.Closed;

        protected readonly MySqlConnection Connection;

        public DatabaseClient(string connectionStr)
        {
            Connection = new MySqlConnection(connectionStr);

            Adapter = new NormalQueryAdapter(this);
        }

        public void Disconnect()
        {
            if (Connection.State == ConnectionState.Open)
            {
                try
                {
                    Connection.Close();
                }
                finally
                {
                    SetInternalState(ConnectionState.Closed);
                }
            }  
        }

        public void Connect()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();
                }
                finally
                {
                    SetInternalState(ConnectionState.Open);
                }
            }        
        }

        public void Dispose()
        {
            Adapter.Dispose();

            Connection.Dispose();
        }

        public IQueryAdapter GetQueryReactor() => Adapter;

        public bool IsAvailable() => Connection.State == ConnectionState.Open;

        public MySqlCommand CreateCommand() => Connection.CreateCommand();

        public void SetInternalState(ConnectionState state) => InternalState = state;

        public ConnectionState GetInternalState()
        {
            if (InternalState == Connection.State)
                return InternalState;
            if (InternalState == ConnectionState.Executing || InternalState == ConnectionState.Fetching)
                return InternalState;
            return InternalState = Connection.State;
        }
    }
}