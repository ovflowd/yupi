namespace Yupi.Messages.Bots
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class BotSpeechListMessageComposer : Yupi.Messages.Contracts.BotSpeechListMessageComposer
    {
        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, int num, BotInfo bot)
        {
            string text = "";
            /*
            switch(num) {
            case 2:
                text = bot.RandomSpeech == null ? string.Empty : string.Join ("\n", bot.RandomSpeech);

                text += ";#;";
                text += bot.AutomaticChat ? "true" : "false";
                text += ";#;";
                text += bot.SpeechInterval.ToString ();
                text += ";#;";
                text += bot.MixPhrases ? "true" : "false";
                break;
            case 5:
                text = bot.Name;
                break;
            default:
                return;
            }

            using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
                message.AppendInteger (bot.Id);
                message.AppendInteger (num);
                message.AppendString(text);
                session.Send (message);
            }
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}