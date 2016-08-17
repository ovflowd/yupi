using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Net;
using Yupi.Controller;
using Yupi.Protocol.Buffers;
using Yupi.Messages;
using Yupi.Messages.User;
using Yupi.Messages.Achievements;
using log4net;
using Yupi.Util;
using log4net.Appender;
using Yupi.Model.Repository;

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
			// TODO Implement properly

			((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = log4net.Core.Level.Debug;
			((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);


			if (MonoUtil.IsRunningOnMono ()) {
				var appender = new log4net.Appender.ManagedColoredConsoleAppender();
				appender.Layout = new log4net.Layout.PatternLayout(@"%date %-5level %message%newline");
				appender.Threshold = log4net.Core.Level.Debug;
				appender.ActivateOptions();
				log4net.Config.BasicConfigurator.Configure(appender);
			} else {
				var appender = new log4net.Appender.ColoredConsoleAppender();
				appender.Layout = new log4net.Layout.PatternLayout(@"%date %-5level %message%newline");
				appender.Threshold = log4net.Core.Level.Debug;
				appender.ActivateOptions();
				log4net.Config.BasicConfigurator.Configure(appender);
			}
				
			ModelHelper = new ModelHelper ();
			// TODO Close Session & Multiple sessions!
			var factory = ModelHelper.CreateFactory ();
			var dbSession = factory.OpenSession ();

			DependencyFactory.RegisterInstance (dbSession);

			var repo = DependencyFactory.Resolve<Repository<UserInfo>> ();
			var info = new UserInfo () {
				UserName = "HelloWorld"
			};
			repo.Save (info);
			DependencyFactory.Resolve<SSOManager> ().GenerateTicket(info);


			// TODO Don't hardcode this stuff :)
			Router.Default = new Router ("PRODUCTION-201510201205-42435347", "../../../Config/", typeof(AchievementProgressMessageComposer).Assembly);

			ClientManager = DependencyFactory.Resolve<ClientManager> ();
			Router = Router.Default;

			TCPServer = ServerFactory<Habbo>.CreateServer(30000);

			TCPServer.OnConnectionOpened += ClientManager.AddClient; // TODO Connection security!
			TCPServer.OnConnectionClosed += ClientManager.RemoveClient;
			TCPServer.OnMessageReceived += (ISession<Habbo> session, byte[] body) => {

				//using(global::Yupi.Emulator.Messages.Buffers.SimpleClientMessageBuffer message = ClientMessageFactory.GetClientMessage()) {
				// TODO When using message pool the SimpleClientMessageBuffer becomes invalid (after several messages) -> DEBUG
				ClientMessage message = new ClientMessage();
				message.Setup(body);
				Router.Handle(session.UserData, message);
			};
		}

		public void Run() {
			TCPServer.Start ();
		}
	}
}

