namespace Yupi.Messages.Bots
{
    using System;

    public class BotSpeechListMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint botId = request.GetUInt32();
            int num = request.GetInteger(); // TODO meaning?

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

        #endregion Methods
    }
}