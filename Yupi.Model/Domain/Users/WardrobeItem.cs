namespace Yupi.Model.Domain
{
    using System;

    public class WardrobeItem
    {
        #region Properties

        public virtual string Gender
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual string Look
        {
            get; set;
        }

        public virtual int Slot
        {
            get; set;
        }

        #endregion Properties
    }
}