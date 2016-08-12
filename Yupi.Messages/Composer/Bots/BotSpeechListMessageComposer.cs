using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Bots
{
	public class BotSpeechListMessageComposer : Yupi.Messages.Contracts.BotSpeechListMessageComposer
	{
		// TODO Refactor
		public override void Compose ( Yupi.Protocol.ISender session, int num, BotInfo bot)
		{
			string text = "";

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
		}
	}
}

