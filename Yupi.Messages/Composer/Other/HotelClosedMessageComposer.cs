using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class HotelClosedMessageComposer : Contracts.HotelClosedMessageComposer
    {
        public override void Compose(ISender session, int openHour, int openMinute, bool userThrownOut)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(openHour);
                message.AppendInteger(openMinute);
                message.AppendBool(userThrownOut);
                session.Send(message);
            }
        }
    }
}