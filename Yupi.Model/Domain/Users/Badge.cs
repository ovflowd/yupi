namespace Yupi.Model.Domain
{
    using System;

    public class Badge
    {
        #region Properties

        public virtual string Code
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual int Slot
        {
            get; set;
        }

        #endregion Properties
    }
}