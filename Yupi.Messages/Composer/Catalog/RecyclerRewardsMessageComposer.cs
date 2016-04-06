using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Catalog
{
	public class RecyclerRewardsMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
		{
			// TODO Hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				List<int> ecotronRewardsLevels = Yupi.GetGame().GetCatalogManager().GetEcotronRewardsLevels();

				message.AppendInteger(ecotronRewardsLevels.Count);
				// TODO Refactor
				foreach (int current in ecotronRewardsLevels)
				{
					message.AppendInteger(current);
					message.AppendInteger(current);

					List<EcotronReward> ecotronRewardsForLevel =
						Yupi.GetGame().GetCatalogManager().GetEcotronRewardsForLevel(uint.Parse(current.ToString()));

					message.AppendInteger(ecotronRewardsForLevel.Count);

					foreach (EcotronReward current2 in ecotronRewardsForLevel)
					{
						message.AppendString(current2.GetBaseItem().PublicName);
						message.AppendInteger(1);
						message.AppendString(current2.GetBaseItem().Type.ToString());
						message.AppendInteger(current2.GetBaseItem().SpriteId);
					}
				}

				session.Send (message);
			}
		}
	}
}

