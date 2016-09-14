namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class FloorMapMessageComposer : Yupi.Messages.Contracts.FloorMapMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string heightmap, int wallHeight)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true); // TODO Hardcoded
                message.AppendInteger(wallHeight);
                message.AppendString(heightmap);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}