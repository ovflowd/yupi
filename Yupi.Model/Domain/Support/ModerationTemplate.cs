namespace Yupi.Model.Domain
{
    public class ModerationTemplate
    {
        public virtual int Id { get; protected set; }
        public virtual bool AvatarBan { get; set; }
        public virtual short BanHours { get; set; }
        public virtual string BanMessage { get; set; }
        public virtual string Caption { get; set; }

        // TODO Move these to own class
        public virtual short Category { get; set; }
        public virtual string CName { get; set; }

        public virtual bool Mute { get; set; }
        public virtual bool TradeLock { get; set; }
        public virtual string WarningMessage { get; set; }
    }
}