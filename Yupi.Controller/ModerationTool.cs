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

        #region Constructors

        public ModerationTool()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();

            // TODO Load
        }

        #endregion Constructors

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