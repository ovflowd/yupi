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

using Yupi.Data.Base.Exceptions;

namespace Yupi.Data.Base
{
    public class DatabaseServerDetails
    {
        private readonly string _databaseName;
        private readonly string _host;
        private readonly string _passWord;
        private readonly uint _port;
        private readonly string _user;

        public DatabaseServerDetails(string host, uint port, string userName, string passWord, string databaseName)
        {
            if (string.IsNullOrEmpty(host))
                throw new DatabaseException("No host was given");

            if (string.IsNullOrEmpty(userName))
                throw new DatabaseException("No username was given");

            if (string.IsNullOrEmpty(databaseName))
                throw new DatabaseException("No database name was given");

            _host = host;
            _port = port;
            _databaseName = databaseName;
            _user = userName;
            _passWord = string.IsNullOrEmpty(passWord) ? string.Empty : passWord;
        }

        public string GetDatabaseName() => _databaseName;

        public string GetHost() => _host;

        public string GetPassword() => _passWord;

        public uint GetPort() => _port;

        public string GetUserName() => _user;

        public override string ToString() => $"{_user}@{_host}";
    }
}