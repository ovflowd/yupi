using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class UnignoreUserMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var text = request.GetString();
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
    }
}