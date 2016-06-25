using System;
using Yupi.Emulator.Game.Groups.Structs;
using System.Collections.Generic;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Groups
{
	public class GroupUpdateBadgeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint guildId = request.GetUInt32();

			Group guild = Yupi.GetGame().GetGroupManager().GetGroup(guildId);

			if (guild != null)
			{
				Room room = Yupi.GetGame().GetRoomManager().GetRoom(guild.RoomId);

				if (room != null)
				{
					request.GetInteger();

					int Base = request.GetInteger();
					int baseColor = request.GetInteger();

					// TODO Unused value!
					request.GetInteger();

					List<int> guildStates = new List<int>();

					for (int i = 0; i < 12; i++)
						guildStates.Add(request.GetInteger());

					string badge = Yupi.GetGame().GetGroupManager().GenerateGuildImage(Base, baseColor, guildStates);

					guild.Badge = badge;

					router.GetComposer<RoomGroupMessageComposer> ().Compose (room);

					Yupi.GetGame().GetGroupManager().SerializeGroupInfo(guild, Response, Session, room);

					using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
					{
						queryReactor.SetQuery(
							$"UPDATE groups_data SET group_badge = @badge WHERE id = {guildId}");
						queryReactor.AddParameter("badge", badge);
						queryReactor.RunQuery();
					}

					if (session.GetHabbo().CurrentRoom != null)
					{
						// TODO Isn't this a duplicate of the above?
						session.GetHabbo().CurrentRoom.LoadedGroups[guildId] = guild.Badge;

						router.GetComposer<RoomGroupMessageComposer> ().Compose (session.GetHabbo ().CurrentRoom);

						Yupi.GetGame()
							.GetGroupManager()
							.SerializeGroupInfo(guild, Response, Session, Session.GetHabbo().CurrentRoom);
					}
				}
			}
		}
	}
}

