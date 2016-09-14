namespace Yupi.Model.Domain
{
    using System;

    public class BotInfo : BaseInfo
    {
        #region Properties

        public virtual char Gender
        {
            get; set;
        }

        public virtual string Look
        {
            get; set;
        }

        public virtual UserInfo Owner
        {
            get; set;
        }

        #endregion Properties
    }
}