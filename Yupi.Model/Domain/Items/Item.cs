namespace Yupi.Model.Domain
{
    using System;

    [IsDiscriminated]
    public abstract class Item
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual UserInfo Owner
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual void TryParseExtraData(string data)
        {
        }

        #endregion Methods
    }
}