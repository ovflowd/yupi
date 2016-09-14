namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Messages;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Net;
    using Yupi.Protocol;
    using Yupi.Util;

    public class ClientManager
    {
        #region Fields

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RoomManager RoomManager;

        #endregion Fields

        #region Properties

        private IList<ISession<Habbo>> Connections
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public ClientManager()
        {
            Connections = new List<ISession<Habbo>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public void AddClient(ISession<Habbo> session)
        {
            // TODO Should be user specific
            session.UserData = new Habbo(session, Router.Default);

            lock (Connections)
            {
                Connections.Add(session);
            }
        }

        public void Disconnect(Habbo session, string reason)
        {
            RoomEntity entity = session.RoomEntity;

            if (entity != null)
            {
                RoomManager.RemoveUser(session);
            }

            session.Session.Disconnect();

            Logger.DebugFormat("User disconnected [{0}] Reason: {1}", session.MachineId, reason);
        }

        public Habbo GetByInfo(UserInfo info)
        {
            lock (Connections)
            {
                return Connections.SingleOrDefault(x => x.UserData.Info == info)?.UserData;
            }
        }

        public IEnumerable<Habbo> GetByPermission(string permission)
        {
            lock (Connections)
            {
                return Connections.Where(x => x.UserData.Info.HasPermission(permission)).Select(x => x.UserData);
            }
        }

        public Habbo GetByUserId(int id)
        {
            lock (Connections)
            {
                return Connections.SingleOrDefault(x => x.UserData.Info?.Id == id)?.UserData;
            }
        }

        public bool IsOnline(UserInfo info)
        {
            lock (Connections)
            {
                return Connections.Any(x => x.UserData.Info == info);
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

        #endregion Methods
    }
}