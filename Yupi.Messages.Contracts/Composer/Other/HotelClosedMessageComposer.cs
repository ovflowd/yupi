using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class HotelClosedMessageComposer : AbstractComposer<int, int, bool>
    {
        public override void Compose(ISender session, int openHour, int openMinute, bool userThrownOut)
        {
        }
    }
}