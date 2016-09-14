namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using Yupi.Messages.Encoders;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class UpdateUserStatusMessageComposer : Contracts.UpdateUserStatusMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<Yupi.Model.Domain.RoomEntity> entities)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entities.Count);

                foreach (RoomEntity entity in entities)
                {
                    message.AppendInteger(entity.Id);
                    message.Append(entity.Position);
                    message.AppendInteger(entity.RotHead);
                    message.AppendInteger(entity.RotBody);
                    message.AppendString(entity.Status.ToString()); // TODO Extra Data

                    // TODO Implement states:
                    // mv x,y,z
                    // sign Model.Domain.Sign
                    // (probably human & pet?) gst Model.Domain.Gesture#DisplayName
                    // (probably pet only) gst Model.Domain.PetGesture#DisplayName
                    // trd
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}