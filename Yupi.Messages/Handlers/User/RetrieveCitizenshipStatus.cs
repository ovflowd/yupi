namespace Yupi.Messages.User
{
    using System;

    public class RetrieveCitizenshipStatus : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string whatever = message.GetString(); // TODO What does the string contain?
            router.GetComposer<CitizenshipStatusMessageComposer>().Compose(session, whatever);
        }

        #endregion Methods
    }
}