namespace Yupi.Model
{
    using System;
    using System.Globalization;

    public class LayPosture : EntityPosture
    {
        #region Fields

        public static readonly LayPosture Default = new LayPosture(0.55f);

        // TODO What are valid values for deltaZ?
        private float DeltaZ;

        #endregion Fields

        #region Constructors

        public LayPosture(float deltaZ)
        {
            this.DeltaZ = deltaZ;
        }

        #endregion Constructors

        #region Methods

        public override string ToStatusString()
        {
            return "lay " + this.DeltaZ.ToString(CultureInfo.InvariantCulture);
        }

        #endregion Methods
    }
}