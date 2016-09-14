namespace Yupi.Model.Domain
{
    using System;

    using Yupi.Model.Domain;

    public class RoomEffectBaseItem : FloorBaseItem
    {
        #region Methods

        public override Item CreateNew()
        {
            return new RoomEffectItem();
        }

        #endregion Methods
    }
}