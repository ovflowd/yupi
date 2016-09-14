#region Header

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

#endregion Header

namespace Yupi.Net
{
    using System;

    public class ServerSettings : IServerSettings
    {
        #region Fields

        = 0;
        = 0;
        = 0;
        = 0;
        = 100;
        = 1000;
        = 4096;

        #endregion Fields

        #region Properties

        public int Backlog
        {
            get; set;
        }

        public int BufferSize
        {
            get; set;
        }

        public string IP
        {
            get; set;
        }

        public int MaxConnections
        {
            get; set;
        }

        public int MaxIOThreads
        {
            get; set;
        }

        public int MaxWorkingThreads
        {
            get; set;
        }

        public int MinIOThreads
        {
            get; set;
        }

        public int MinWorkingThreads
        {
            get; set;
        }

        public int Port
        {
            get; set;
        }

        #endregion Properties
    }
}