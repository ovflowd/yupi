namespace Yupi.Model.Domain
{
    public class BotInfo : BaseInfo
    {
        public virtual string Look { get; set; }
        public virtual char Gender { get; set; }
        public virtual UserInfo Owner { get; set; }
    }
}