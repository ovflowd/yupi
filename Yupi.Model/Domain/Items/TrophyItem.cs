using System;

namespace Yupi.Model.Domain
{
    public class TrophyItem : FloorItem<TrophyBaseItem>
    {
        public virtual UserInfo User { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string Message { get; set; }

        public override void TryParseExtraData(string data)
        {
            // TODO Word filter!
            CreatedAt = DateTime.Now;
            Message = data;
        }

        public override string GetExtraData()
        {
            return string.Concat(User.Name, Convert.ToChar(9),
                CreatedAt.ToString("dd-MM-yyyy"), Convert.ToChar(9), Message);
        }
    }
}