namespace Yupi.Model.Domain
{
    using System;

    public class TalentLevel
    {
        #region Properties

        public virtual Achievement Achievement
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual int Level
        {
            get; set;
        }

        #endregion Properties
    }
}