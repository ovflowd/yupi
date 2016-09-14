namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class MessengerMessage
    {
        #region Properties

        public virtual UserInfo From
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual bool Read
        {
            get; set;
        }

        public virtual string Text
        {
            get; set;
        }

        public virtual DateTime Timestamp
        {
            get; set;
        }

        public virtual UserInfo To
        {
            get; set;
        }

        public virtual string UnfilteredText
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual TimeSpan Diff()
        {
            return DateTime.Now - Timestamp;
        }

        #endregion Methods
    }
}