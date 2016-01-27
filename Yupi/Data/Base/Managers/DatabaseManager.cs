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

using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Clients;

namespace Yupi.Data.Base.Managers
{
    public class DatabaseManager
    {
        private readonly List<DatabaseClient> _databaseClients;

        private MySqlConnectionStringBuilder _serverDetails;

        private void SetServerDetails(MySqlConnectionStringBuilder serverDetails)
        {
            _serverDetails = serverDetails;
        }

        public DatabaseManager(MySqlConnectionStringBuilder serverDetails)
        {
            SetServerDetails(serverDetails);

            _databaseClients = new List<DatabaseClient>((int)_serverDetails.MaximumPoolSize);
        }

        private DatabaseClient AddConnection(bool needReturn = false)
        {
            lock (_databaseClients)
            {
                if (_databaseClients.Count + 1 >= _databaseClients.Capacity ||
                    _databaseClients.Count >= _databaseClients.Capacity)
                    return null;

                if (needReturn)
                {
                    DatabaseClient client = new DatabaseClient(_serverDetails.ToString());

                    _databaseClients.Add(client);

                    return client;
                }

                _databaseClients.Add(new DatabaseClient(_serverDetails.ToString()));

                return null;
            }
        }

        private DatabaseClient ReturnConnectedConnection()
        {
            lock (_databaseClients)
            {
                if (_databaseClients.Any(c => c.IsAvailable()))
                    return _databaseClients.First(c => c.IsAvailable());

                if (_databaseClients.Count > 0)
                    _databaseClients.RemoveAll(c => !c.IsAvailable());                
            }

            return AddConnection(true);
        }

        public IQueryAdapter GetQueryReactor()
        {
            DatabaseClient client = ReturnConnectedConnection();

            client.Connect();

            return client.GetQueryReactor();
        }

        public void Destroy()
        {
            if (_databaseClients == null)
                return;

            lock (_databaseClients)
            {
                foreach (DatabaseClient current in _databaseClients)
                {
                    if (!current.IsAvailable())
                        current.Dispose();

                    current.Disconnect();
                }
            }

            lock (_databaseClients)
                _databaseClients.Clear();
        }
    }
}