using System;

namespace Yupi.Model.Domain
{
    public class MannequinItem : FloorItem<MannequinBaseItem>
    {
        // TODO Enum for gender
        public virtual string Gender { get; set; }
        public virtual string Look { get; set; }

        public MannequinItem()
        {
            Gender = "m";
            Look = "lg-270-82.ch-210-66";
        }

        public override string GetExtraData()
        {
            // TODO What is the last string good for?
            return string.Join("\u0005", Gender, Look, "Mannequin");
        }
    }
}