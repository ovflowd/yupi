namespace Yupi.Messages.Rooms
{
    using System;

    public class RoomSettingsMuteAllMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Room currentRoom = session.GetHabbo().CurrentRoom;

            if (currentRoom == null || !currentRoom.CheckRights(session, true))
                return;

            currentRoom.RoomMuted = !currentRoom.RoomMuted;

            router.GetComposer<RoomMuteStatusMessageComposer> ().Compose (currentRoom, currentRoom.RoomMuted);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}