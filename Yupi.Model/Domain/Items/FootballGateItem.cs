namespace Yupi.Model.Domain
{
    public class FootballGateItem : FloorItem<FootballGateBaseItem>
    {
        public FootballGateItem()
        {
            LookMale = "lg-270-82.ch-210-66";
            LookFemale = "lg-270-82.ch-210-66";
        }

        public virtual string LookMale { get; set; }
        public virtual string LookFemale { get; set; }

        public override string GetExtraData()
        {
            return string.Join(";", LookMale, LookFemale);
        }
    }
}