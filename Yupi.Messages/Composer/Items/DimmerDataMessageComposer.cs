using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class DimmerDataMessageComposer : Contracts.DimmerDataMessageComposer
    {
        public override void Compose(ISender session, MoodlightData moodlight)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(moodlight.Presets.Count);
                message.AppendInteger(moodlight.CurrentPreset.Id);

                for (var i = 0; i < moodlight.Presets.Count; ++i)
                {
                    var preset = moodlight.Presets[i];
                    message.AppendInteger(i);
                    message.AppendInteger(preset.BackgroundOnly ? 2 : 1);
                    message.AppendString(preset.ColorCode);
                    message.AppendInteger(preset.ColorIntensity);
                }
                session.Send(message);
            }
        }
    }
}