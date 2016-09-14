using System;

namespace Yupi.Model.Domain
{
    public class BadgeDisplayItem : FloorItem<BadgeDisplayBaseItem>
    {
        // TODO Use class instead of string?
        public virtual string Badge { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public override void TryParseExtraData(string data)
        {
            CreatedAt = DateTime.Now;

            if (Owner.Badges.HasBadge(data)) Badge = data;
        }

        public override string GetExtraData()
        {
            return string.Join("|", Badge, Owner.Name, CreatedAt.ToString("dd-MM-yyyy"));
        }
    }
}