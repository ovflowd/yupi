using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class DoorbellAnswerMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session))
                return;

            string userName = request.GetString();
            bool anwser = request.GetBool();

            GameClient clientByUserName = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);

            if (clientByUserName == null)
                return;

            if (anwser)
            {
                clientByUserName.GetHabbo().LoadingChecksPassed = true;

                router.GetComposer<DoorbellOpenedMessageComposer> ().Compose (clientByUserName);
            }
            else if (clientByUserName.GetHabbo().CurrentRoomId != session.GetHabbo().CurrentRoomId)
            {
                router.GetComposer<DoorbellNoOneMessageComposer> ().Compose (clientByUserName);
            }
            */
            throw new NotImplementedException();
        }
    }
}