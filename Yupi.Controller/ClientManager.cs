using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yupi.Messages;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Net;

namespace Yupi.Controller
{
    public class ClientManager
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        private readonly RoomManager RoomManager;

        public ClientManager()
        {
            Connections = new List<ISession<Habbo>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        private IList<ISession<Habbo>> Connections { get; }

        public bool IsOnline(UserInfo info)
        {
            lock (Connections)
            {
                return Connections.Any(x => x.UserData.Info == info);
            }
        }

        public Habbo GetByInfo(UserInfo info)
        {
            lock (Connections)
            {
                return Connections.SingleOrDefault(x => x.UserData.Info == info)?.UserData;
            }
        }

        public Habbo GetByUserId(int id)
        {
            lock (Connections)
            {
                return Connections.SingleOrDefault(x => x.UserData.Info?.Id == id)?.UserData;
            }
        }

        public void Disconnect(Habbo session, string reason)
        {
            RoomEntity entity = session.RoomEntity;

            if (entity != null) RoomManager.RemoveUser(session);

            session.Session.Disconnect();

            Logger.DebugFormat("User disconnected [{0}] Reason: {1}", session.MachineId, reason);
        }

        public IEnumerable<Habbo> GetByPermission(string permission)
        {
            lock (Connections)
            {
                return Connections.Where(x => x.UserData.Info.HasPermission(permission)).Select(x => x.UserData);
            }
        }

        public void AddClient(ISession<Habbo> session)
        {
            // TODO Should be user specific
            session.UserData = new Habbo(session, Router.Default);

            lock (Connections)
            {
                Connections.Add(session);
            }
        }

        public void RemoveClient(ISession<Habbo> session)
        {
            lock (Connections)
            {
                Connections.Remove(session);
            }
            Disconnect(session.UserData, "Socket closed");
        }
    }
}