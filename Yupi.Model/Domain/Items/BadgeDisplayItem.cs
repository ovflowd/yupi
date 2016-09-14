namespace Yupi.Model.Domain
{
    using System;

    public class BadgeDisplayItem : FloorItem<BadgeDisplayBaseItem>
    {
        #region Properties

        // TODO Use class instead of string?
        public virtual string Badge
        {
            get; set;
        }

        public virtual DateTime CreatedAt
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override string GetExtraData()
        {
            return string.Join("|", Badge, Owner.Name, CreatedAt.ToString("dd-MM-yyyy"));
        }

        public override void TryParseExtraData(string data)
        {
            CreatedAt = DateTime.Now;

            if (Owner.Badges.HasBadge(data))
            {
                Badge = data;
            }
        }

        #endregion Methods
    }
}