namespace Yupi.Model.Domain
{
    using System;

    public class Link
    {
        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        // Internal will open in window with id habboMain
        public virtual bool IsInternal
        {
            get; set;
        }

        public virtual string Text
        {
            get; set;
        }

        public virtual string URL
        {
            get; set;
        }

        #endregion Properties
    }
}