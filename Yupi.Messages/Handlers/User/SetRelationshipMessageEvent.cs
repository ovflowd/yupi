using System;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Controller;
using Yupi.Protocol;


namespace Yupi.Messages.User
{
    public class SetRelationshipMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;
        private MessengerController MessengerController;

        public SetRelationshipMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            MessengerController = DependencyFactory.Resolve<MessengerController>();
        }

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
    }
}