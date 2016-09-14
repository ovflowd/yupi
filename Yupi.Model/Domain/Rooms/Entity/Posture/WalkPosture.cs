namespace Yupi.Model
{
    using System;
    using System.Globalization;
    using System.Numerics;

    public class WalkPosture : EntityPosture
    {
        #region Fields

        public readonly Vector3 Target;

        #endregion Fields

        #region Constructors

        public WalkPosture(Vector3 target)
        {
            this.Target = target;
        }

        #endregion Constructors

        #region Methods

        public override string ToStatusString()
        {
            return "mv " +
                   String.Join(",", (int) Target.X, (int) Target.Y, Target.Z.ToString(CultureInfo.InvariantCulture));
        }

        #endregion Methods
    }
}