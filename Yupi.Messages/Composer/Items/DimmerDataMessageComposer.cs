using System;
using Yupi.Emulator.Game.Items.Datas;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Messages.Items
{
	public class DimmerDataMessageComposer : AbstractComposer<MoodlightData>
	{
		public override void Compose (Yupi.Protocol.ISender session, MoodlightData moodlight)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(moodlight.Presets.Count);
				message.AppendInteger(moodlight.CurrentPreset);

				for (int i = 0; i < moodlight.Presets.Count; ++i) {
					MoodlightPreset preset = moodlight.Presets [i];
					message.AppendInteger(i);
					message.AppendInteger(preset.BackgroundOnly ? 2 : 1);
					message.AppendString(preset.ColorCode);
					message.AppendInteger(preset.ColorIntensity);
				}
				session.Send (message);
			}
		}
	}
}

