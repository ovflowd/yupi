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

using Yupi.Messages.Parsers;

namespace Yupi.Net.Packets
{
    /// <summary>
    ///     Class InitialPacketParser.
    /// </summary>
    public class InitialPacketParser : IDataParser
    {
        /// <summary>
        ///     Delegate with parameters
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="amountOfBytes">The amount of bytes.</param>
        public delegate void DualParamDelegate(byte[] packet, int amountOfBytes);

        /// <summary>
        ///     Delegate NoParamDelegate
        /// </summary>
        public delegate void NoParamDelegate();

        /// <summary>
        ///     Handles the packet data.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="amountOfBytes">The amount of bytes.</param>
        public void HandlePacketData(byte[] packet, int amountOfBytes)
        {
            if (Yupi.ShutdownStarted)
                return;

            if (packet[0] == 60 && PolicyRequest != null)
                PolicyRequest();

            else if (packet[0] != 67 || SwitchParserRequest == null)
                SwitchParserRequest?.Invoke(packet, amountOfBytes);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            PolicyRequest = null;
            SwitchParserRequest = null;
        }

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone() => new InitialPacketParser();

        /// <summary>
        ///     Occurs when [policy request].
        /// </summary>
        public event NoParamDelegate PolicyRequest;

        /// <summary>
        ///     Occurs when [switch parser request].
        /// </summary>
        public event DualParamDelegate SwitchParserRequest;
    }
}