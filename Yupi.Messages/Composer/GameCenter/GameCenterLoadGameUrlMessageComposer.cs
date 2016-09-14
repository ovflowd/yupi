using System;
using Yupi.Protocol;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterLoadGameUrlMessageComposer : Contracts.GameCenterLoadGameUrlMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO  hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                message.AppendString(UnixTimestamp.FromDateTime(DateTime.Now));
                message.AppendString(""); // TODO Reimplement: ServerExtraSettings.GameCenterStoriesUrl
                session.Send(message);
            }
        }
    }
}