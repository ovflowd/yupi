using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Bots
{
	public class BotSpeechListMessageComposer : AbstractComposer<int, RoomBot>
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISender session, int num, RoomBot bot)
		{
			string text = "";

			switch(num) {
			case 2:
				text = bot.BotData.RandomSpeech == null ? string.Empty : string.Join ("\n", bot.BotData.RandomSpeech);

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
				message.AppendInteger (bot.BotId);
				message.AppendInteger (num);
				message.AppendString(text);
				session.Send (message);
			}
		}
	}
}

