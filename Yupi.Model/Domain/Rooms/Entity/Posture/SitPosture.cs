using System;
using System.Globalization;

namespace Yupi.Model
{
    public class SitPosture : EntityPosture
    {
        public static readonly SitPosture Default = new SitPosture(0.55f);
        private readonly bool CanStandUp;
        // TODO What are valid values for deltaZ?
        private readonly float DeltaZ;

        public SitPosture(float deltaZ, bool canStandUp = true)
        {
            DeltaZ = deltaZ;
            CanStandUp = canStandUp;
        }

        public override string ToStatusString()
        {
            return "sit " + DeltaZ.ToString(CultureInfo.InvariantCulture) + " " + Convert.ToInt32(CanStandUp);
        }
    }
}