namespace Yupi.Model.Domain
{
    using System;

    public class BaseInfo
    {
        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        // TODO Do pets have mottos?
        public virtual string Motto
        {
            get; set;
        }

        public virtual string Name
        {
            get; set;
        }

        #endregion Properties
    }
}