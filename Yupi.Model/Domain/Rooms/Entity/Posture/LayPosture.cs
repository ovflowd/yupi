using System;
using System.Globalization;

namespace Yupi.Model
{
    public class LayPosture : EntityPosture
    {
        // TODO What are valid values for deltaZ?
        private float DeltaZ;

        public static readonly LayPosture Default = new LayPosture(0.55f);

        public LayPosture(float deltaZ)
        {
            this.DeltaZ = deltaZ;
        }

        public override string ToStatusString()
        {
            return "lay " + this.DeltaZ.ToString(CultureInfo.InvariantCulture);
        }
    }
}