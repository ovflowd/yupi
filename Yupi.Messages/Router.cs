using System;
using System.Linq;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Net;
using Yupi.Protocol;
using Yupi.Model.Domain;

namespace Yupi.Messages
{
	public class Router : Yupi.Protocol.IRouter
	{
		private static readonly log4net.ILog Logger = log4net.LogManager
			.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private Dictionary<short, AbstractHandler> Incoming;
		private Dictionary<Type, IComposer> Outgoing;

		private PacketLibrary library;
		private ServerMessagePool pool;

		public Router (string release, string configDir)
		{
			library = new PacketLibrary (release, configDir);
			pool = new ServerMessagePool ();
		}

		public T GetComposer<T>() {
			IComposer composer;
			Outgoing.TryGetValue (typeof(T), out composer);

			if (composer == null) {
				Logger.ErrorFormat ("Invalid composer {0}", typeof(T).Name);
			}

			return (T)composer;
		}

		public void Handle (Yupi.Protocol.ISession<Habbo> session, ClientMessage message) {
			AbstractHandler handler;
			Incoming.TryGetValue (message.Id, out handler);

			if (handler == null) {
				Logger.WarnFormat ("Unknown incoming message {0}", message.Id);
			} else {
				handler.HandleMessage (session, message, this);
			}
		}
		// TODO Fix handler names in *.incoming
		private void LoadHandlers() {
			Incoming = new Dictionary<short, AbstractHandler> ();

			IEnumerable<Type> handlers = GetImplementing <AbstractHandler>();

			foreach (Type handlerType in handlers) {
				AbstractHandler handler = (AbstractHandler)Activator.CreateInstance(handlerType);
				Incoming.Add(library.GetIncomingId(handler.GetType().Name), handler);
			}
		}

		private void LoadComposers() {
			Outgoing = new Dictionary<Type, IComposer> ();

			IEnumerable<Type> composers = GetImplementing <IComposer>();

			foreach (Type composerType in composers) {
				IComposer composer = (IComposer)Activator.CreateInstance(composerType);
				composer.Init (library.GetOutgoingId (composerType.Name), pool);
				Outgoing.Add(composerType, composer);
			}
		}

		private IEnumerable<Type> GetImplementing<T>() {
			return GetType ().Assembly.GetTypes ().Where (p => typeof(T).IsAssignableFrom (p));
		}
	}
}