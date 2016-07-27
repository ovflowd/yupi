using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Yupi.Model.Domain
{
	public class MoodlightData
	{
		public virtual int Id { get; protected set; }

		public virtual int CurrentPreset { get; set; }

		public virtual bool Enabled { get; set; }

		// TODO Is this not fixed size?
		public virtual IList<MoodlightPreset> Presets { get; protected set; }

		public MoodlightData ()
		{
			Presets = new List<MoodlightPreset> ();
		}

		public virtual string GenerateExtraData ()
		{
			MoodlightPreset preset = Presets[CurrentPreset];
			StringBuilder stringBuilder = new StringBuilder ();
			stringBuilder.Append (Enabled ? 2 : 1);
			stringBuilder.Append (",");
			stringBuilder.Append (CurrentPreset);
			stringBuilder.Append (",");
			stringBuilder.Append (preset.BackgroundOnly ? 2 : 1);
			stringBuilder.Append (",");
			stringBuilder.Append (preset.ColorCode);
			stringBuilder.Append (",");
			stringBuilder.Append (preset.ColorIntensity);
			return stringBuilder.ToString ();
		}
	}
}