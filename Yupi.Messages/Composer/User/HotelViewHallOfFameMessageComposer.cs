using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Emulator.Game.Users;

namespace Yupi.Messages.User
{
	public class HotelViewHallOfFameMessageComposer : AbstractComposer<string>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, string code)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(code);
				// TODO Refactor
				IEnumerable<HallOfFameElement> rankings = Yupi.GetGame().GetHallOfFame().Rankings.Where(e => e.Competition == code);
				message.StartArray();

				int rank = 1;
				foreach (HallOfFameElement element in rankings)
				{
					Habbo user = Yupi.GetHabboById(element.UserId);

					if (user == null) 
						continue;

					message.AppendInteger(user.Id);
					message.AppendString(user.UserName);
					message.AppendString(user.Look);
					message.AppendInteger(rank);
					message.AppendInteger(element.Score);
					rank++;
					message.SaveArray();
				}
				message.EndArray();

				session.Send (message);
			}
		}
	}
}

