namespace Yupi.Model.Domain
{
    using System;

    public class PetItem : Item
    {
        #region Constructors

        public PetItem()
        {
            Info = new PetInfo();
        }

        #endregion Constructors

        #region Properties

        public virtual PetBaseItem BaseItem
        {
            get; set;
        }

        public virtual PetInfo Info
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual string GetExtraData()
        {
            throw new NotImplementedException();
        }

        public override void TryParseExtraData(string data)
        {
            string[] dataArray = data.Split('\n');

            if (dataArray.Length != 3)
            {
                return;
            }

            // TODO Validate
            string petName = dataArray[0];
            string color = dataArray[2];

            int race;
            int.TryParse(dataArray[1], out race);

            Info.Name = petName;
            Info.Race = race;
            Info.Color = color;
            Info.Owner = Owner;
        }

        #endregion Methods
    }
}