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
using System.Data;
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

        public DatabaseManager(MySqlConnectionStringBuilder serverDetails)
        {
            SetServerDetails(serverDetails);

            _databaseClients = new List<DatabaseClient>((int) _serverDetails.MaximumPoolSize);
        }

        public MySqlConnectionStringBuilder GetConnectionStringBuilder() => _serverDetails;

        private void SetServerDetails(MySqlConnectionStringBuilder serverDetails) => _serverDetails = serverDetails;

        private DatabaseClient AddConnection(bool needReturn = false)
        {
            lock (_databaseClients)
            {
                if (_databaseClients.Count + 1 >= _databaseClients.Capacity)
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

        private void RemoveUnusedConnections(bool removeAllCalled = false)
        {
            if (removeAllCalled)
            {
                lock (_databaseClients)
                {
                    foreach (DatabaseClient databaseClient in _databaseClients)
                    {
                        lock (databaseClient)
                        {
                            try
                            {
                                if (databaseClient == null)
                                    continue;

                                if (databaseClient.IsAvailable())
                                    databaseClient?.Disconnect();

                                databaseClient?.Dispose();

                                _databaseClients.Remove(databaseClient);
                            }
                            catch
                            {
                                _databaseClients.Remove(databaseClient);
                            }
                        }
                    }
                }
            }
            else
            {
                lock (_databaseClients)
                {
                    if (_databaseClients.Count > 0)
                    {
                        foreach (DatabaseClient databaseClient in _databaseClients.Where(c => !c.IsAvailable()))
                        {
                            lock (databaseClient)
                            {
                                try
                                {
                                    if (databaseClient == null)
                                        continue;

                                    if (databaseClient.IsAvailable())
                                        databaseClient?.Disconnect();

                                    databaseClient?.Dispose();

                                    _databaseClients.Remove(databaseClient);
                                }
                                catch
                                {
                                    _databaseClients.Remove(databaseClient);
                                }
                            }
                        }
                            
                        _databaseClients.RemoveAll(c => !c.IsAvailable());
                    }
                }
            }
        }

        private DatabaseClient ReturnConnectedConnection()
        {
            lock (_databaseClients)
            {
                if (_databaseClients.Any(c => c.IsAvailable() && c.GetConnectionHandler()?.GetState() != ConnectionState.Executing))
                    return _databaseClients.First(c => c.IsAvailable() && c.GetConnectionHandler()?.GetState() != ConnectionState.Executing);

                RemoveUnusedConnections();

                return AddConnection(true);
            }  
        }

        public IQueryAdapter GetQueryReactor()
        {
            DatabaseClient client = ReturnConnectedConnection();

            lock (client)
            {
                client.Connect();

                return client.GetQueryReactor();
            }
        }

        public void Destroy()
        {
            if (_databaseClients == null)
                return;

            RemoveUnusedConnections(true);

            lock (_databaseClients)
                _databaseClients.Clear();
        }
    }
}