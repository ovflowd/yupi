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
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace Yupi.Net.SuperSocketImpl
{
	public class Session<T> : AppSession<Session<T>, RequestInfo>, ISession<T>
	{
		public System.Net.IPAddress RemoteAddress {
			get {
				return RemoteEndPoint.Address;
			}
		}


		public T UserData { get; set; }

		protected override void HandleException(Exception e)
		{
			Logger.Warn ("A networking error occured", e);
			Disconnect ();
		}

		public void Send (byte[] data)
		{
			Send (new ArraySegment<byte> (data));
		}

		public void Disconnect ()
		{
			base.Close ();
		}
	}
}

