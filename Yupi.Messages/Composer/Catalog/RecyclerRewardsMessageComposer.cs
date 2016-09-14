using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class RecyclerRewardsMessageComposer : Contracts.RecyclerRewardsMessageComposer
    {
        public override void Compose(ISender session, EcotronLevel[] levels)
        {
            // TODO Hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(levels.Length);
                // TODO Refactor
                foreach (var current in levels)
                {
                    message.AppendInteger(current.Id);
                    message.AppendInteger(current.Id);

                    message.AppendInteger(current.Rewards.Count);

                    foreach (var current2 in current.Rewards)
                    {
                        message.AppendString(current2.BaseItem.PublicName);
                        message.AppendInteger(1);
                        message.AppendString(current2.BaseItem.Type.ToString());
                        message.AppendInteger(current2.BaseItem.SpriteId);
                    }
                }

                session.Send(message);
            }
        }
    }
}