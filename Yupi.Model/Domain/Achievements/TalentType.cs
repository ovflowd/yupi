namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class TalentType : Enumeration<TalentType>
    {
        #region Fields

        public static readonly TalentType Citizenship = new TalentType(1, "citizenship");
        public static readonly TalentType Status = new TalentType(2, "status");

        #endregion Fields

        #region Constructors

        private TalentType(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}