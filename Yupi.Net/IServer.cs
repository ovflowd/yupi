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

namespace Yupi.Net
{
    public delegate void MessageReceived<T>(ISession<T> session, byte[] body);

    public delegate void ConnectionOpened<T>(ISession<T> session);

    public delegate void ConnectionClosed<T>(ISession<T> session);

    public interface IServer<T>
    {
        event MessageReceived<T> OnMessageReceived;
        event ConnectionOpened<T> OnConnectionOpened;
        event ConnectionClosed<T> OnConnectionClosed;

        /// <summary>
        ///     Starts this server instance.
        /// </summary>
        /// <returns>
        ///     return true if start successfull, else false
        /// </returns>
        bool Start();

        /// <summary>
        ///     Stops this server instance.
        /// </summary>
        void Stop();
    }
}