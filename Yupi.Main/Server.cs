using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Net;
using Yupi.Controller;
using Yupi.Protocol.Buffers;
using Yupi.Messages;

namespace Yupi.Main
{
	public class Server
	{
		private ModelHelper ModelHelper;
		private IServer<Habbo> TCPServer;
		private ClientManager ClientManager;
		private Router Router;

		public Server ()
		{
			ModelHelper = new ModelHelper ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
			Router = new Router ("", "");

			TCPServer = ServerFactory<Habbo>.CreateServer(0);
			/*
			TCPServer.OnConnectionOpened += ClientManager.AddClient; // TODO Connection security!
			TCPServer.OnConnectionClosed += ClientManager.RemoveClient;
			TCPServer.OnMessageReceived += (ISession<Habbo> session, byte[] body) => {

				//using(global::Yupi.Emulator.Messages.Buffers.SimpleClientMessageBuffer message = ClientMessageFactory.GetClientMessage()) {
				// TODO When using message pool the SimpleClientMessageBuffer becomes invalid (after several messages) -> DEBUG
				ClientMessage message = new ClientMessage();
				message.Setup(body);
				session.UserData.Session = session;
				Router.Handle(session, message);
			};*/
		}

		public void Run() {
			TCPServer.Start ();
		}
	}
}

