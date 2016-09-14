namespace Yupi.Messages.Messenger
{
    using System;

    public class FriendListUpdateMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            // TODO Implement
            //session.GetHabbo().GetMessenger();
        }

        #endregion Methods
    }
}