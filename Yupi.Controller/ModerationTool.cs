// ---------------------------------------------------------------------------------
// <copyright file="ModerationTool.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class ModerationTool
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Properties

        public virtual IList<string> RoomMessagePresets
        {
            get; private set;
        }

        public virtual IList<ModerationTemplate> Templates
        {
            get; private set;
        }

        public virtual IList<SupportTicket> Tickets
        {
            get; private set;
        }

        public virtual IList<string> UserMessagePresets
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public ModerationTool()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();

            // TODO Load
        }

        #endregion Constructors

        #region Methods

        public void BanUser(int userId, int hours, string reason)
        {
            UserInfo user = UserRepository.FindBy(userId);
            BanUser(user, hours, reason);
        }

        public void BanUser(UserInfo user, int hours, string reason)
        {
            var session = ClientManager.GetByInfo(user);

            UserBan ban = new UserBan()
            {
                User = user,
                IP = user.LastIp,
                ExpiresAt = DateTime.Now.AddHours(hours),
                Reason = reason
            };

            if (session != null)
            {
                ban.IP = session.Session.RemoteAddress;
                ban.MachineId = session.MachineId;

                ClientManager.Disconnect(session, T._("Banned"));
            }

            user.Bans.Add(ban);
            UserRepository.Save(user);
        }

        public bool CanBan(UserInfo staff, int targetId)
        {
            UserInfo target = UserRepository.FindBy(targetId);

            if (target == null)
            {
                return false;
            }

            return CanBan(staff, target);
        }

        public bool CanBan(UserInfo staff, UserInfo target)
        {
            return staff.Rank > target.Rank;
        }

        #endregion Methods
    }
}