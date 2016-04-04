using System;
using System.Linq;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Net;

namespace Yupi.Messages
{
	public class Router
	{
		private static readonly log4net.ILog Logger = log4net.LogManager
			.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private Dictionary<short, IMessageHandler> Incoming;
		private Dictionary<Type, IComposer> Outgoing;

		private PacketLibrary library;

		public Router (string release, string configDir)
		{
			library = new PacketLibrary (release, configDir);
		}

		public T GetComposer<T>() {
			IComposer composer;
			Outgoing.TryGetValue (typeof(T), out composer);

			if (composer == null) {
				Logger.ErrorFormat ("Invalid composer {0}", typeof(T).Name);
			}

			return (T)composer;
		}

		public void Handle (ISession session, ClientMessage message) {
			IMessageHandler handler;
			Incoming.TryGetValue (message.Id, out handler);

			if (handler == null) {
				Logger.WarnFormat ("Unknown incoming message {0}", message.Id);
			} else {
				handler.HandleMessage (session, message);
			}
		}

		private void LoadHandlers() {
			Incoming = new Dictionary<short, IMessageHandler> ();

			IEnumerable<Type> handlers = GetImplementing <IMessageHandler>();

			foreach (Type handlerType in handlers) {
				IMessageHandler handler = (IMessageHandler)Activator.CreateInstance(handlerType);
				Incoming.Add(library.GetIncomingId(handler.Name), handler);
			}
		}

		private void LoadComposers() {
			Outgoing = new Dictionary<Type, IComposer> ();

			IEnumerable<Type> composers = GetImplementing <IComposer>();

			foreach (Type composerType in composers) {
				IComposer composer = (IComposer)Activator.CreateInstance(composerType);
				composer.SetId (library.GetOutgoingId (composerType.Name));
				Outgoing.Add(composerType, composer);
			}
		}

		private IEnumerable<Type> GetImplementing<T>() {
			return GetType ().Assembly.GetTypes ().Where (p => typeof(T).IsAssignableFrom (p));
		}
	}
}