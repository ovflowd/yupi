namespace Yupi.Messages.User
{
    using System;

    public class UnignoreUserMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string text = request.GetString();
            throw new NotImplementedException();
            /*
            Yupi.Model.Domain.Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();

            if (habbo == null)
                return;

            if(!session.Info.MutedUsers.Contains(habbo.Info))
                return;

            session.Info.MutedUsers.Remove(habbo.Info);
            // TODO Save
            router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.LISTEN, username);
            */
        }

        #endregion Methods
    }
}