using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class NavigatorLiftedRoomsComposer : Yupi.Messages.Contracts.NavigatorLiftedRoomsComposer
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // Count
                /*
                foreach (NavigatorHeader navHeader in headers)
                {
                    message.AppendInteger(navHeader.RoomId);
                    message.AppendInteger(0);
                    message.AppendString(navHeader.Image);
                    message.AppendString(navHeader.Caption);
                }*/

                // TODO implement
                session.Send(message);
            }
        }
    }
}