namespace Yupi.Model.Domain
{
    public class HallOfFameElement
    {
        public virtual int Id { get; protected set; }

        // TODO Competition should not be string?!
        public virtual string Competition { get; set; }
        public virtual int Score { get; set; }
        public virtual UserInfo User { get; set; }
    }
}