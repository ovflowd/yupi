using System;
using System.Collections.Generic;
using Yupi.Protocol;
using Yupi.Model.Domain;
using System.Linq;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Util;
using Yupi.Net;
using Yupi.Messages;

namespace Yupi.Controller
{
	public class ClientManager
	{
		public IList<ISession<Habbo>> Connections { get; private set; }

		private RoomManager RoomManager;

		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger
			(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public ClientManager ()
		{
			Connections = new List<ISession<Habbo>> ();
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public bool IsOnline(UserInfo info) {
			return Connections.Any (x => x.UserData.Info == info);
		}

		public Habbo GetByInfo(UserInfo info) {
			return Connections.SingleOrDefault (x => x.UserData.Info == info)?.UserData;
		}

		public Habbo GetByUserId(int id) {
			return Connections.SingleOrDefault (x => x.UserData.Info.Id == id)?.UserData;
		}

		public void Disconnect(Habbo session, string reason) {
			RoomEntity entity = session.RoomEntity;

			if (entity != null) {
				RoomManager.RemoveUser (entity);
			}

			session.Session.Disconnect ();

			Logger.DebugFormat ("User disconnected [{0}] Reason: {1}", session.MachineId, reason);
		}

		public IEnumerable<Habbo> GetByPermission(string permission) {
			return Connections.Where (x => x.UserData.Info.HasPermission (permission)).Select (x => x.UserData);
		}

		public void AddClient(ISession<Habbo> session) {
			session.UserData = new Habbo (session, Router.Default);
			Connections.Add (session);
		}

		public void RemoveClient(ISession<Habbo> session) {
			Connections.Remove (session);
		}
	}
}

