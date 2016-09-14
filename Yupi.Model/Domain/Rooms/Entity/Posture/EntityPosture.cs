namespace Yupi.Model
{
    using System;

    public abstract class EntityPosture : IStatusString
    {
        #region Methods

        public abstract string ToStatusString();

        #endregion Methods
    }
}