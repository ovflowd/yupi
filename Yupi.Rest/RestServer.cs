using System;
using Nancy.Hosting.Self;

namespace Yupi.Rest
{
	public class RestServer : IDisposable
	{
		private NancyHost Server;

		public RestServer ()
		{
			// TODO Add to config
			HostConfiguration config = new HostConfiguration () {
				RewriteLocalhost = true,
				UrlReservations = new UrlReservations() {
					CreateAutomatically = true
				}
			};

			Server = new NancyHost (config, new Uri ("http://localhost:8080"));

		}

		public void Start ()
		{
			Server.Start ();
		}

		public void Dispose ()
		{
			Server.Dispose ();
			Server = null;
		}
	}
}

