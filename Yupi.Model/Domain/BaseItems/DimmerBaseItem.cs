namespace Yupi.Model.Domain
{
    using System;

    // TODO Consistency: Moodlight vs Dimmer
    public class DimmerBaseItem : WallBaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new DimmerItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}