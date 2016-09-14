namespace Yupi.Model
{
    using System;
    using System.Globalization;

    public class SitPosture : EntityPosture
    {
        #region Fields

        public static readonly SitPosture Default = new SitPosture(0.55f);

        private bool CanStandUp;

        // TODO What are valid values for deltaZ?
        private float DeltaZ;

        #endregion Fields

        #region Constructors

        public SitPosture(float deltaZ, bool canStandUp = true)
        {
            this.DeltaZ = deltaZ;
            this.CanStandUp = canStandUp;
        }

        #endregion Constructors

        #region Methods

        public override string ToStatusString()
        {
            return "sit " + this.DeltaZ.ToString(CultureInfo.InvariantCulture) + " " + Convert.ToInt32(this.CanStandUp);
        }

        #endregion Methods
    }
}