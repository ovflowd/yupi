namespace Yupi.Messages.User
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol;

    public class SetRelationshipMessageEvent : AbstractHandler
    {
        #region Fields

        private MessengerController MessengerController;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public SetRelationshipMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            MessengerController = DependencyFactory.Resolve<MessengerController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int targetId = message.GetInteger();
            int type = message.GetInteger();

            Relationship relationship = session.Info.Relationships.FindByUser(targetId);

            if (relationship == default(Relationship))
            {
                relationship = new Relationship()
                {
                    Friend = UserRepository.FindBy(targetId),
                    Type = type
                };
                session.Info.Relationships.Relationships.Add(relationship);
            }
            else
            {
                relationship.Type = type;
            }

            UserRepository.Save(session.Info);

            MessengerController.UpdateUser(session.Info);
        }

        #endregion Methods
    }
}