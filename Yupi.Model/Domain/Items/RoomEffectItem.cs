using System.Globalization;

namespace Yupi.Model.Domain
{
    public class RoomEffectItem : FloorItem<RoomEffectBaseItem>
    {
        // TODO Number???
        public virtual double Number { get; set; }

        public override string GetExtraData()
        {
            return Number.ToString(CultureInfo.InvariantCulture);
        }

        public override void TryParseExtraData(string data)
        {
            double tmp;
            double.TryParse(data, out tmp);
            Number = tmp;
        }
    }
}