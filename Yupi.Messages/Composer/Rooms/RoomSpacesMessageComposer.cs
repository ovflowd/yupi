namespace Yupi.Messages.Rooms
{
    using System;
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomSpacesMessageComposer : Yupi.Messages.Contracts.RoomSpacesMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomSpacesMessageComposer.RoomSpacesType type,
            RoomData data)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(type.DisplayName);

                if (type == RoomSpacesType.Wallpaper)
                {
                    message.AppendString(data.WallPaper.ToString(CultureInfo.InvariantCulture));
                }
                else if (type == RoomSpacesType.Floor)
                {
                    message.AppendString(data.Floor.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    message.AppendString(data.LandScape.ToString(CultureInfo.InvariantCulture));
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}