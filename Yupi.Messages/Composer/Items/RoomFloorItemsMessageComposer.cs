using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class RoomFloorItemsMessageComposer : Contracts.RoomFloorItemsMessageComposer
    {
        public override void Compose(ISender session, RoomData data, IReadOnlyDictionary<uint, FloorItem> items)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                if (data.Group != null)
                {
                    // TODO Refactor
                    if (data.Group.AdminOnlyDeco == 1u)
                    {
                        message.AppendInteger(data.Group.Admins.Count + 1);


                        foreach (var member in data.Group.Admins)
                            if (member != null)
                            {
                                message.AppendInteger(member.Id);
                                message.AppendString(member.Name);
                            }

                        message.AppendInteger(data.Owner.Id);
                        message.AppendString(data.Owner.Name);
                    }
                    else
                    {
                        message.AppendInteger(data.Group.Members.Count + 1);

                        foreach (var member in data.Group.Members)
                        {
                            message.AppendInteger(member.Id);
                            message.AppendString(member.Name);
                        }
                    }
                }
                else
                {
                    message.AppendInteger(1);
                    message.AppendInteger(data.Owner.Id);
                    message.AppendString(data.Owner.Name);
                }

                message.AppendInteger(items.Count);

                foreach (var roomItem in items) throw new NotImplementedException();

                session.Send(message);
            }
        }
    }
}