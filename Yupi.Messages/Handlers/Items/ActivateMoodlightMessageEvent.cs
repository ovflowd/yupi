namespace Yupi.Messages.Items
{
    using System;

    public class ActivateMoodlightMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
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

        #endregion Methods
    }
}