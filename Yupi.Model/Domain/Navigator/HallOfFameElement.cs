namespace Yupi.Model.Domain
{
    using System;

    public class HallOfFameElement
    {
        #region Properties

        // TODO Competition should not be string?!
        public virtual string Competition
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual int Score
        {
            get; set;
        }

        public virtual UserInfo User
        {
            get; set;
        }

        #endregion Properties
    }
}