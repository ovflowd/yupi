namespace Yupi.Model.Domain
{
    using System;

    [Ignore]
    public class PetStatus : EntityStatus
    {
        #region Constructors

        public PetStatus(PetEntity entity)
            : base(entity)
        {
        }

        #endregion Constructors
    }
}