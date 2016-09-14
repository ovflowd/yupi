using System;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Util;
using System.Collections.Generic;

namespace Yupi.Controller
{
    public class ModerationTool
    {
        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;

        public virtual IList<SupportTicket> Tickets { get; private set; }
        public virtual IList<ModerationTemplate> Templates { get; private set; }
        public virtual IList<string> UserMessagePresets { get; private set; }
        public virtual IList<string> RoomMessagePresets { get; private set; }

        public ModerationTool()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();

            // TODO Load
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
    }
}