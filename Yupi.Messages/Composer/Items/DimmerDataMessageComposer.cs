namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class DimmerDataMessageComposer : Yupi.Messages.Contracts.DimmerDataMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, MoodlightData moodlight)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(moodlight.Presets.Count);
                message.AppendInteger(moodlight.CurrentPreset.Id);

                for (int i = 0; i < moodlight.Presets.Count; ++i)
                {
                    MoodlightPreset preset = moodlight.Presets[i];
                    message.AppendInteger(i);
                    message.AppendInteger(preset.BackgroundOnly ? 2 : 1);
                    message.AppendString(preset.ColorCode);
                    message.AppendInteger(preset.ColorIntensity);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}