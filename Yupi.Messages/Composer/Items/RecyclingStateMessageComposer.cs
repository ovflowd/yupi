using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class RecyclingStateMessageComposer : Contracts.RecyclingStateMessageComposer
    {
        public override void Compose(ISender session, int insertId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(insertId);
                session.Send(message);
            }
        }
    }
}