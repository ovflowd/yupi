namespace Yupi.Model
{
    using System;

    public class StandPosture : EntityPosture
    {
        #region Fields

        public static readonly StandPosture Default = new StandPosture();

        #endregion Fields

        #region Constructors

        private StandPosture()
        {
            // Don't allow external initialization (useless)
        }

        #endregion Constructors

        #region Methods

        public override string ToStatusString()
        {
            return "std";
        }

        #endregion Methods
    }
}