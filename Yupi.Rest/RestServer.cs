namespace Yupi.Rest
{
    using System;

    using Nancy.Hosting.Self;

    public class RestServer : IDisposable
    {
        #region Fields

        private NancyHost Server;

        #endregion Fields

        #region Constructors

        public RestServer()
        {
            // TODO Add to config
            HostConfiguration config = new HostConfiguration()
            {
                RewriteLocalhost = true,
                UrlReservations = new UrlReservations()
                {
                    CreateAutomatically = true
                }
            };

            Server = new NancyHost(config, new Uri("http://localhost:8080"));
        }

        #endregion Constructors

        #region Methods

        public void Dispose()
        {
            Server.Dispose();
            Server = null;
        }

        public void Start()
        {
            Server.Start();
        }

        #endregion Methods
    }
}