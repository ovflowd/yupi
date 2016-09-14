namespace Yupi.Model.Domain
{
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class MoodlightData
    {
        #region Properties

        public virtual MoodlightPreset CurrentPreset
        {
            get; set;
        }

        public virtual bool Enabled
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual IList<MoodlightPreset> Presets
        {
            get; protected set;
        }

        #endregion Properties

        #region Constructors

        public MoodlightData()
        {
            Presets = new List<MoodlightPreset>();
            Enabled = false;
            CurrentPreset = new MoodlightPreset();
        }

        #endregion Constructors

        #region Methods

        public virtual string GenerateExtraData()
        {
            StringBuilder stringBuilder = new StringBuilder();
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

        #endregion Methods
    }
}