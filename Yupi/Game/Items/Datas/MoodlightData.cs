using System.Collections.Generic;
using System.Data;
using System.Text;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items.Datas
{
    /// <summary>
    ///     Class MoodlightData.
    /// </summary>
    internal class MoodlightData
    {
        /// <summary>
        ///     The current preset
        /// </summary>
        internal int CurrentPreset;

        /// <summary>
        ///     The enabled
        /// </summary>
        internal bool Enabled;

        /// <summary>
        ///     The item identifier
        /// </summary>
        internal uint ItemId;

        /// <summary>
        ///     The presets
        /// </summary>
        internal List<MoodlightPreset> Presets;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MoodlightData" /> class.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <exception cref="System.NullReferenceException">No moodlightdata found in the database</exception>
        internal MoodlightData(uint itemId)
        {
            ItemId = itemId;
            DataRow row;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT enabled,current_preset,preset_one,preset_two,preset_three FROM items_moodlight WHERE item_id='{itemId}'");
                row = commitableQueryReactor.GetRow();
            }
            if (row != null)
            {
                Enabled = Yupi.EnumToBool(row["enabled"].ToString());
                CurrentPreset = (int) row["current_preset"];
                Presets = new List<MoodlightPreset>
                {
                    GeneratePreset((string) row["preset_one"]),
                    GeneratePreset((string) row["preset_two"]),
                    GeneratePreset((string) row["preset_three"])
                };
            }
        }

        /// <summary>
        ///     Generates the preset.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>MoodlightPreset.</returns>
        internal static MoodlightPreset GeneratePreset(string data)
        {
            string[] array = data.Split(',');
            if (!IsValidColor(array[0]))
            {
                array[0] = "#000000";
            }

            return new MoodlightPreset(array[0], int.Parse(array[1]), Yupi.EnumToBool(array[2]));
        }

        /// <summary>
        ///     Determines whether [is valid color] [the specified color code].
        /// </summary>
        /// <param name="colorCode">The color code.</param>
        /// <returns><c>true</c> if [is valid color] [the specified color code]; otherwise, <c>false</c>.</returns>
        internal static bool IsValidColor(string colorCode)
        {
            switch (colorCode)
            {
                case "#000000":
                case "#0053F7":
                case "#EA4532":
                case "#82F349":
                case "#74F5F5":
                case "#E759DE":
                case "#F2F851":
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Determines whether [is valid intensity] [the specified intensity].
        /// </summary>
        /// <param name="intensity">The intensity.</param>
        /// <returns><c>true</c> if [is valid intensity] [the specified intensity]; otherwise, <c>false</c>.</returns>
        internal static bool IsValidIntensity(int intensity)
        {
            return intensity >= 0 && intensity <= 255;
        }

        /// <summary>
        ///     Enables this instance.
        /// </summary>
        internal void Enable()
        {
            Enabled = true;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery($"UPDATE items_moodlight SET enabled = '1' WHERE item_id = {ItemId}");
        }

        /// <summary>
        ///     Disables this instance.
        /// </summary>
        internal void Disable()
        {
            Enabled = false;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery($"UPDATE items_moodlight SET enabled = '0' WHERE item_id = {ItemId}");
        }

        /// <summary>
        ///     Updates the preset.
        /// </summary>
        /// <param name="preset">The preset.</param>
        /// <param name="color">The color.</param>
        /// <param name="intensity">The intensity.</param>
        /// <param name="bgOnly">if set to <c>true</c> [bg only].</param>
        /// <param name="hax">if set to <c>true</c> [hax].</param>
        internal void UpdatePreset(int preset, string color, int intensity, bool bgOnly, bool hax = false)
        {
            if (!IsValidColor(color) || (!IsValidIntensity(intensity) && !hax))
                return;

            string text = "one";
            switch (preset)
            {
                case 2:
                    text = "two";
                    break;

                case 3:
                    text = "three";
                    break;

                default:
                    text = "one";
                    break;
            }

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"UPDATE items_moodlight SET preset_{text}='{color},{intensity},{Yupi.BoolToEnum(bgOnly)}' WHERE item_id='{ItemId}'");
                commitableQueryReactor.RunQuery();
            }

            GetPreset(preset).ColorCode = color;
            GetPreset(preset).ColorIntensity = intensity;
            GetPreset(preset).BackgroundOnly = bgOnly;
        }

        /// <summary>
        ///     Gets the preset.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>MoodlightPreset.</returns>
        internal MoodlightPreset GetPreset(int i)
        {
            {
                i--;
                return Presets[i] ?? new MoodlightPreset("#000000", 255, false);
            }
        }

        /// <summary>
        ///     Generates the extra data.
        /// </summary>
        /// <returns>System.String.</returns>
        internal string GenerateExtraData()
        {
            MoodlightPreset preset = GetPreset(CurrentPreset);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Enabled ? 2 : 1);
            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset);
            stringBuilder.Append(",");
            stringBuilder.Append(preset.BackgroundOnly ? 2 : 1);
            stringBuilder.Append(",");
            stringBuilder.Append(preset.ColorCode);
            stringBuilder.Append(",");
            stringBuilder.Append(preset.ColorIntensity);
            return stringBuilder.ToString();
        }
    }
}