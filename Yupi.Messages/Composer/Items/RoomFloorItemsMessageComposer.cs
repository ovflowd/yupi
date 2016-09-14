using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
    public class RoomFloorItemsMessageComposer : Yupi.Messages.Contracts.RoomFloorItemsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData data,
            IReadOnlyDictionary<uint, FloorItem> items)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                if (data.Group != null)
                {
                    // TODO Refactor
                    if (data.Group.AdminOnlyDeco == 1u)
                    {
                        message.AppendInteger(data.Group.Admins.Count + 1);


                        foreach (UserInfo member in data.Group.Admins)
                        {
                            if (member != null)
                            {
                                message.AppendInteger(member.Id);
                                message.AppendString(member.Name);
                            }
                        }

                        message.AppendInteger(data.Owner.Id);
                        message.AppendString(data.Owner.Name);
                    }
                    else
                    {
                        message.AppendInteger(data.Group.Members.Count + 1);

                        foreach (UserInfo member in data.Group.Members)
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

                foreach (KeyValuePair<uint, FloorItem> roomItem in items)
                {
                    // RoomItem::Serialize
                    throw new NotImplementedException();
                    //roomItem.Value.Serialize (message);
                }

                session.Send(message);
            }
        }
    }
}