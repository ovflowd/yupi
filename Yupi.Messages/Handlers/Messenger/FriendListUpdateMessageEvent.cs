using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class FriendListUpdateMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            // TODO Implement
            //session.GetHabbo().GetMessenger();
        }
    }
}