using System.Collections.Generic;
using Yupi.Messages.Encoders;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class UpdateUserStatusMessageComposer : Contracts.UpdateUserStatusMessageComposer
    {
        public override void Compose(ISender session, IList<RoomEntity> entities)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entities.Count);

                foreach (var entity in entities)
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
    }
}