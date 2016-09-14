using Yupi.Messages.Encoders;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomSettingsDataMessageComposer : Contracts.RoomSettingsDataMessageComposer
    {
        public override void Compose(ISender session, RoomData room)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendString(room.Name);
                message.AppendString(room.Description);
                message.AppendInteger(room.State.Value);
                message.AppendInteger(room.Category.Id);
                message.AppendInteger(room.UsersMax);
                message.AppendInteger(0); // unused
                message.AppendInteger(room.Tags.Count);

                foreach (var s in room.Tags)
                    message.AppendString(s);

                message.AppendInteger(room.TradeState.Value);
                message.AppendInteger(room.AllowPets);
                message.AppendInteger(room.AllowPetsEating);
                message.AppendInteger(room.AllowWalkThrough);
                message.AppendInteger(room.HideWall);
                message.AppendInteger(room.WallThickness);
                message.AppendInteger(room.FloorThickness);
                message.Append(room.Chat);
                message.AppendBool(false); //TODO allow_dyncats_checkbox
                message.Append(room.ModerationSettings);
                session.Send(message);
            }
        }
    }
}