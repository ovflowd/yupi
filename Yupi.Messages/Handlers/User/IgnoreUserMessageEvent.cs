using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class IgnoreUserMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var username = request.GetString();

            /*
            // TODO Really?! By username?! Who the hell thought that would be a good idea? S.u.l.a.k.e ...?
            Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(username).GetHabbo();

            if (habbo == null)
                return;
            // TODO Rename mute to ignore!
            if (session.GetHabbo().MutedUsers.Contains(habbo.Id) || habbo.Rank > 4u)
                return;

            session.GetHabbo().MutedUsers.Add(habbo.Id);
            router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.IGNORE, username);
        */
            throw new NotImplementedException();
        }
    }
}