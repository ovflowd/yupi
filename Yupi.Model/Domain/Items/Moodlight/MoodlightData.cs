using System.Collections.Generic;
using System.Text;

namespace Yupi.Model.Domain
{
    public class MoodlightData
    {
        public MoodlightData()
        {
            Presets = new List<MoodlightPreset>();
            Enabled = false;
            CurrentPreset = new MoodlightPreset();
        }

        public virtual int Id { get; protected set; }

        public virtual MoodlightPreset CurrentPreset { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual IList<MoodlightPreset> Presets { get; protected set; }

        public virtual string GenerateExtraData()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Enabled ? 2 : 1); // TODO Enum?
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset);
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset.BackgroundOnly ? 2 : 1);
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset.ColorCode);
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset.ColorIntensity);
            return stringBuilder.ToString();
        }
    }
}