using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class NavigatorLiftedRoomsComposer : Contracts.NavigatorLiftedRoomsComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
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