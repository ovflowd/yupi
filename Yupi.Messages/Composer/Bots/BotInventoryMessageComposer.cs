using System;
using Yupi.Protocol;

namespace Yupi.Messages.Bots
{
    public class BotInventoryMessageComposer : Contracts.BotInventoryMessageComposer
    {
        public override void Compose(ISender session, HybridDictionary items)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(items.Count);

                /*
                foreach (RoomBot current in items.Values)
                {
                    message.AppendInteger(current.BotId);
                    message.AppendString(current.Name);
                    message.AppendString(current.Motto);
                    message.AppendString(current.Gender.ToLower()); 
                    message.AppendString(current.Look);
                }*/
                throw new NotImplementedException();
                session.Send(message);
            }
        }
    }
}