namespace Yupi.Model.Domain
{
    [IsDiscriminated]
    public abstract class Item
    {
        public virtual int Id { get; set; }

        public virtual UserInfo Owner { get; set; }

        public virtual void TryParseExtraData(string data)
        {
        }
    }
}