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
using Yupi.Emulator.Data.Base.Adapters.Enums;
using Yupi.Emulator.Data.Base.Clients.Interfaces;

namespace Yupi.Emulator.Data.Base.Adapters.Models
{
    public abstract class BaseQueryAdapter
    {
        protected IDatabaseClient Client;

        protected MySqlCommand Command;

        protected object BaseFetchCommand(FetchType fetchSelector)
        {
            if (!Client.IsAvailable())
                return null;

            try
            {
                Client?.GetConnectionHandler()?.SetFetching();

                return CommandTypeSelector(fetchSelector);
            }
            catch
            {
                Client?.GetConnectionHandler()?.SetOpened();

                return null;
            }
            finally
            {
                Client?.GetConnectionHandler()?.SetOpened();
            }
        }

        protected object BaseExecuteCommand(RunType runSelector)
        {
            if (!Client.IsAvailable())
                return null;

            try
            {
                Client?.GetConnectionHandler()?.SetExecuting();

                return CommandTypeSelector(runSelector);
            }
            catch
            {
                Client?.GetConnectionHandler()?.SetOpened();

                return null;
            }
            finally
            {
                Client.GetConnectionHandler().SetOpened();
            }
        }

        private object CommandTypeSelector(RunType runSelector)
        {
            try
            {
                switch (runSelector)
                {
                    case RunType.Insert:
                        Command?.ExecuteScalar();

                        return Command?.LastInsertedId;
                    case RunType.Normal:
                    case RunType.Fast:
                        return Command?.ExecuteNonQuery();
                    default:
                        return null;
                }
            }
            catch
            {
                Client?.GetConnectionHandler()?.SetOpened();

                return null;
            }
        }

        private object CommandTypeSelector(FetchType fetchSelector)
        {
            try
            {
                switch (fetchSelector)
                {
                    case FetchType.Integer:
                    case FetchType.String:
                        return Command?.ExecuteScalar();
                    case FetchType.Row:
                        DataSet dataSet = new DataSet();

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(Command))
                            dataAdapter.Fill(dataSet);

                        return dataSet;
                    case FetchType.Table:
                        DataTable dataTable = new DataTable();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(Command))
                            adapter.Fill(dataTable);

                        return dataTable;
                    default:
                        return null;
                }
            }
            catch
            {
                Client?.GetConnectionHandler()?.SetOpened();

                return null;
            }         
        }
    }
}