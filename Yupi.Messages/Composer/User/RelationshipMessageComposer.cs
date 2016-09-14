using System;
using System.Linq;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.User
{
    public class RelationshipMessageComposer : Yupi.Messages.Contracts.RelationshipMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            // TODO Refactor

            int num = habbo.Relationships.Relationships.Count(x => x.Type == 1);
            int num2 = habbo.Relationships.Relationships.Count(x => x.Type == 2);
            int num3 = habbo.Relationships.Relationships.Count(x => x.Type == 3);

            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Id);
                message.AppendInteger(habbo.Relationships.Relationships.Count);

                foreach (Relationship current in habbo.Relationships.Relationships)
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