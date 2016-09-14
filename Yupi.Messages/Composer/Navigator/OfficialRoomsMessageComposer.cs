namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class OfficialRoomsMessageComposer : Yupi.Messages.Contracts.OfficialRoomsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData roomData)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomData.Id);
                message.AppendString(roomData.CCTs);
                message.AppendInteger(roomData.Id);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}