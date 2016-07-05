using System;


using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class CompetitionEntrySubmitResultMessageComposer : AbstractComposer<RoomCompetition, int, Room>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomCompetition competition, int status, Room room = null)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(competition.Id);
				message.AppendString(competition.Name);
				message.AppendInteger(status);

				if (status != 3)
				{
					message.AppendInteger(0);
					message.AppendInteger(0);
				}
				else
				{
					message.StartArray();

					foreach (string furni in competition.RequiredFurnis)
					{
						message.AppendString(furni);
						message.SaveArray();
					}

					message.EndArray();

					if (room == null)
						message.AppendInteger(0);
					else
					{
						message.StartArray();

						foreach (string furni in competition.RequiredFurnis)
						{
							if (!room.GetRoomItemHandler().HasFurniByItemName(furni))
							{
								message.AppendString(furni);
								message.SaveArray();
							}
						}

						message.EndArray();
					}
				}
				session.Send (message);
			}
		}
	}
}

