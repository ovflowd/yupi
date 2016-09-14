using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class SetRelationshipMessageEvent : AbstractHandler
    {
        private readonly MessengerController MessengerController;
        private readonly IRepository<UserInfo> UserRepository;

        public SetRelationshipMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            MessengerController = DependencyFactory.Resolve<MessengerController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var targetId = message.GetInteger();
            var type = message.GetInteger();

            var relationship = session.Info.Relationships.FindByUser(targetId);

            if (relationship == default(Relationship))
            {
                relationship = new Relationship
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