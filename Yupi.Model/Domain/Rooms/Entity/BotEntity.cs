namespace Yupi.Model.Domain
{
    [Ignore]
    public class BotEntity : HumanEntity
    {
        public BotEntity(Room room, int id) : base(room, id)
        {
        }

        public BotInfo Info { get; set; }

        public override EntityType Type
        {
            get { return EntityType.Bot; }
        }

        public override BaseInfo BaseInfo
        {
            get { return Info; }
        }
    }
}