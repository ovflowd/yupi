namespace Yupi.Model
{
    using System;

    public class SwimPosture : EntityPosture
    {
        #region Fields

        public static readonly SwimPosture Default = new SwimPosture();

        #endregion Fields

        #region Constructors

        private SwimPosture()
        {
            // Don't allow external initialization (useless)
        }

        #endregion Constructors

        #region Methods

        public override string ToStatusString()
        {
            return "swim";
        }

        #endregion Methods
    }
}