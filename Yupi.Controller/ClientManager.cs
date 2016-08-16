using System;
using System.Collections.Generic;
using Yupi.Protocol;
using Yupi.Model.Domain;
using System.Linq;
using Yupi.Model;
using Yupi.Model.Repository;

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

		public ISession<Habbo> GetByInfo(UserInfo info) {
			return Connections.SingleOrDefault (x => x.UserData.Info == info);
		}

		public ISession<Habbo> GetByUserId(int id) {
			return Connections.SingleOrDefault (x => x.UserData.Info.Id == id);
		}

		public void Disconnect(ISession<Habbo> session, string reason) {
			RoomEntity entity = session.UserData.RoomEntity;

			if (entity != null) {
				RoomManager.RemoveUser (entity);
			}

			session.Disconnect ();

			Logger.DebugFormat ("User disconnected [{0}] Reason: {1}", session.UserData.MachineId, reason);
		}

		public void AddClient(ISession<Habbo> session) {
			Connections.Add (session);
		}

		public void RemoveClient(ISession<Habbo> session) {
			Connections.Remove (session);
		}
	}
}

