using System;
using Yupi.Net.SuperSocketImpl;
using System.Threading;

namespace Yupi.Net
{
	public class MyClass
	{
		public static void Main(string[] args) {
			Console.WriteLine ("STARTING");

			IServerSettings settings = new ServerSettings(){
				Port = 9000
			};

			IServer server = new SuperServer (settings);
			server.Start();

			while (true) {
				Thread.Sleep (1000);
			}
		}
	}
}

