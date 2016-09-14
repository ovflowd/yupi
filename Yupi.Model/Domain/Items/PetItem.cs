using System;

namespace Yupi.Model.Domain
{
    public class PetItem : Item
    {
        public PetItem()
        {
            Info = new PetInfo();
        }

        public virtual PetBaseItem BaseItem { get; set; }

        public virtual PetInfo Info { get; set; }

        public virtual string GetExtraData()
        {
            throw new NotImplementedException();
        }

        public override void TryParseExtraData(string data)
        {
            var dataArray = data.Split('\n');

            if (dataArray.Length != 3) return;

            // TODO Validate
            var petName = dataArray[0];
            var color = dataArray[2];

            int race;
            int.TryParse(dataArray[1], out race);

            Info.Name = petName;
            Info.Race = race;
            Info.Color = color;
            Info.Owner = Owner;
        }
    }
}