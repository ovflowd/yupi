namespace Yupi.Model.Domain.Components
{
    using System;

    public class UserBuilderComponent
    {
        #region Properties

        public virtual int BuildersExpire
        {
            get; set;
        }

        public virtual int BuildersItemsMax
        {
            get; set;
        }

        public virtual int BuildersItemsUsed
        {
            get; set;
        }

        #endregion Properties
    }
}