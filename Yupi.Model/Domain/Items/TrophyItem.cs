namespace Yupi.Model.Domain
{
    using System;

    public class TrophyItem : FloorItem<TrophyBaseItem>
    {
        #region Properties

        public virtual DateTime CreatedAt
        {
            get; set;
        }

        public virtual string Message
        {
            get; set;
        }

        public virtual UserInfo User
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override string GetExtraData()
        {
            return string.Concat(User.Name, Convert.ToChar(9),
                CreatedAt.ToString("dd-MM-yyyy"), Convert.ToChar(9), Message);
        }

        public override void TryParseExtraData(string data)
        {
            // TODO Word filter!
            CreatedAt = DateTime.Now;
            Message = data;
        }

        #endregion Methods
    }
}