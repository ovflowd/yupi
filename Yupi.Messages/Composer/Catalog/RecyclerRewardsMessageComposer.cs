namespace Yupi.Messages.Catalog
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RecyclerRewardsMessageComposer : Yupi.Messages.Contracts.RecyclerRewardsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, EcotronLevel[] levels)
        {
            // TODO Hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(levels.Length);
                // TODO Refactor
                foreach (EcotronLevel current in levels)
                {
                    message.AppendInteger(current.Id);
                    message.AppendInteger(current.Id);

                    message.AppendInteger(current.Rewards.Count);

                    foreach (EcotronReward current2 in current.Rewards)
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

        #endregion Methods
    }
}