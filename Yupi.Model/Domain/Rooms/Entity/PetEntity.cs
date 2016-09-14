namespace Yupi.Model.Domain
{
    using System;

    [Ignore]
    public class PetEntity : RoomEntity
    {
        #region Properties

        public override BaseInfo BaseInfo
        {
            get { return Info; }
        }

        public PetInfo Info
        {
            get; set;
        }

        public PetStatus PetStatus
        {
            get; private set;
        }

        public override EntityStatus Status
        {
            get { return PetStatus; }
        }

        public override EntityType Type
        {
            get { return EntityType.Pet; }
        }

        #endregion Properties

        #region Constructors

        public PetEntity(Room room, int id)
            : base(room, id)
        {
            PetStatus = new PetStatus(this);
        }

        #endregion Constructors
    }
}