namespace Yupi.Model.Domain
{
    using System;

    public class Minimail
    {
        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        public virtual bool Seen
        {
            get; protected set;
        }

        #endregion Properties
    }
}