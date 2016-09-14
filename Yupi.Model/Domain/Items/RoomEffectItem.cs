namespace Yupi.Model.Domain
{
    using System;
    using System.Globalization;

    public class RoomEffectItem : FloorItem<RoomEffectBaseItem>
    {
        #region Properties

        // TODO Number???
        public virtual double Number
        {
            get; set;
        }

        #endregion Properties

        #region Methods

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

        #endregion Methods
    }
}