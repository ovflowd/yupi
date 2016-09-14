// ---------------------------------------------------------------------------------
// <copyright file="ClientManager.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
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