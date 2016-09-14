using System.Linq;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class RelationshipMessageComposer : Contracts.RelationshipMessageComposer
    {
        public override void Compose(ISender session, UserInfo habbo)
        {
            // TODO Refactor

            var num = habbo.Relationships.Relationships.Count(x => x.Type == 1);
            var num2 = habbo.Relationships.Relationships.Count(x => x.Type == 2);
            var num3 = habbo.Relationships.Relationships.Count(x => x.Type == 3);

            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Id);
                message.AppendInteger(habbo.Relationships.Relationships.Count);

                foreach (var current in habbo.Relationships.Relationships)
                {
                    message.AppendInteger(current.Type);
                    message.AppendInteger(current.Type == 1 ? num : (current.Type == 2 ? num2 : num3));
                    message.AppendInteger(current.Friend.Id);
                    message.AppendString(current.Friend.Name);
                    message.AppendString(current.Friend.Look);
                }

                session.Send(message);
            }
        }
    }
}