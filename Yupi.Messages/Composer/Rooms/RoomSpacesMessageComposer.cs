using System.Globalization;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomSpacesMessageComposer : Contracts.RoomSpacesMessageComposer
    {
        public override void Compose(ISender session, RoomSpacesType type, RoomData data)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(type.DisplayName);

                if (type == RoomSpacesType.Wallpaper)
                    message.AppendString(data.WallPaper.ToString(CultureInfo.InvariantCulture));
                else if (type == RoomSpacesType.Floor)
                    message.AppendString(data.Floor.ToString(CultureInfo.InvariantCulture));
                else message.AppendString(data.LandScape.ToString(CultureInfo.InvariantCulture));

                session.Send(message);
            }
        }
    }
}