using System.Globalization;

namespace Yupi.Model
{
    public class LayPosture : EntityPosture
    {
        public static readonly LayPosture Default = new LayPosture(0.55f);
        // TODO What are valid values for deltaZ?
        private readonly float DeltaZ;

        public LayPosture(float deltaZ)
        {
            DeltaZ = deltaZ;
        }

        public override string ToStatusString()
        {
            return "lay " + DeltaZ.ToString(CultureInfo.InvariantCulture);
        }
    }
}