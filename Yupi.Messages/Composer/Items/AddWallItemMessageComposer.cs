using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class AddWallItemMessageComposer : Contracts.AddWallItemMessageComposer
    {
        public override void Compose(ISender session, WallItem item, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                /*
                message.AppendString(item.Id);
                message.AppendInteger(item.BaseItem.SpriteId);
                message.AppendString(item.Position.ToString());

                message.AppendString(item.GetExtraData());
                message.AppendInteger(-1);
                message.AppendInteger(item.BaseItem.Modes > 1 ? 1 : 0);
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                session.Send (message);
                */
                throw new NotImplementedException();
            }
        }
    }
}