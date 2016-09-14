using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class HeightMapMessageComposer : Contracts.HeightMapMessageComposer
    {
        public override void Compose(ISender session, HeightMap map)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(map.TotalX);
                message.AppendInteger(map.MapSize);
                for (var i = 0; i < map.MapSize; i++)
                    message.AppendShort((short) (map.GetTileHeight(i)*256));
                session.Send(message);
            }
        }
    }
}