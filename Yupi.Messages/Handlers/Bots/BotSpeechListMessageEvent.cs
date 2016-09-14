using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Bots
{
    public class BotSpeechListMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var botId = request.GetUInt32();
            var num = request.GetInteger(); // TODO meaning?

            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            // TODO introduce entity classes and proper inheritance structure
            RoomUser bot = room?.GetRoomUserManager().GetBot(botId);

            if (bot == null || !bot.IsBot)
                return;

            router.GetComposer<BotSpeechListMessageComposer> ().Compose (session, num, bot.BotData);
            */
            throw new NotImplementedException();
        }
    }
}