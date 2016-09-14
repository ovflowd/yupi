namespace Yupi.Messages.Items
{
    using System;

    public class YouTubeChoosePlaylistVideoMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}