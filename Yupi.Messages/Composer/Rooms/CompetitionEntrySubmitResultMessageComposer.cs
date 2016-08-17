﻿using System;


using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class CompetitionEntrySubmitResultMessageComposer : Yupi.Messages.Contracts.CompetitionEntrySubmitResultMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomCompetition competition, int status, RoomData room = null)
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
					message.AppendInteger(competition.RequiredItems.Count);

					foreach (BaseItem furni in competition.RequiredItems)
					{
						message.AppendString(furni.Name);
					}

					if (room == null)
						message.AppendInteger(0);
					else
					{
						message.StartArray();

						foreach (BaseItem furni in competition.RequiredItems)
						{
							/*
							if (!room.GetRoomItemHandler().HasFurniByItemName(furni))
							{
								message.AppendString(furni);
								message.SaveArray();
							}
							*/
							throw new NotImplementedException ();
						}

						message.EndArray();
					}
				}
				session.Send (message);
			}
		}
	}
}
