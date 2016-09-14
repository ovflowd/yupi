using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class ActivateMoodlightMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            if (room.MoodlightData == null)
            {
                foreach (
                    RoomItem current in
                    room.GetRoomItemHandler()
                    .WallItems.Values.Where(
                        current => current.GetBaseItem().InteractionType == Interaction.Dimmer))
                    room.MoodlightData = new MoodlightData(current.Id);
            }

            if (room.MoodlightData == null)
                return;

            router.GetComposer<DimmerDataMessageComposer> ().Compose (session, room.MoodlightData);
            */
            throw new NotImplementedException();
        }
    }
}