namespace Yupi.Model.Domain
{
    using System;

    using Yupi.Model.Domain;

    [Ignore]
    public class UserEntity : HumanEntity
    {
        #region Fields

        public Habbo User;

        #endregion Fields

        #region Constructors

        public UserEntity(Habbo user, Room room, int id)
            : base(room, id)
        {
            this.User = user;
        }

        #endregion Constructors

        #region Properties

        public override BaseInfo BaseInfo
        {
            get { return UserInfo; }
        }

        public override EntityType Type
        {
            get { return EntityType.User; }
        }

        public UserInfo UserInfo
        {
            get { return User.Info; }
        }

        #endregion Properties

        #region Methods

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

        public override void SetDance(Dance dance)
        {
            if (dance.ClubOnly && !this.User.Info.Subscription.IsValid())
            {
                return;
            }

            base.SetDance(dance);
        }

        #endregion Methods
    }
}