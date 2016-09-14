using System;

namespace Yupi.Model.Domain
{
    [Ignore]
    public class UserEntity : HumanEntity
    {
        public Habbo User;

        public UserEntity(Habbo user, Room room, int id) : base(room, id)
        {
            User = user;
        }

        public UserInfo UserInfo
        {
            get { return User.Info; }
        }

        public override BaseInfo BaseInfo
        {
            get { return UserInfo; }
        }

        public override EntityType Type
        {
            get { return EntityType.User; }
        }

        public override void SetDance(Dance dance)
        {
            if (dance.ClubOnly && !User.Info.Subscription.IsValid()) return;

            base.SetDance(dance);
        }

        public override void HandleChatMessage(UserEntity user, Action<Habbo> sendTo)
        {
            if (!User.Info.MutedUsers.Contains(user.User.Info))
            {
                base.HandleChatMessage(user, sendTo);
                sendTo(User);
            }
        }

        public override void OnRoomExit()
        {
        }
    }
}