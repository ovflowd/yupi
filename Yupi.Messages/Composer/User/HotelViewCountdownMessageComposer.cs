using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class HotelViewCountdownMessageComposer : Yupi.Messages.Contracts.HotelViewCountdownMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, string time)
        {
            DateTime date;
            DateTime.TryParse(time, out date);
            TimeSpan diff = date - DateTime.Now;

            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(time);
                message.AppendInteger(Convert.ToInt32(diff.TotalSeconds)); // TODO millenium bug?
                session.Send(message);
            }

            // TODO Why is there a output?
            Console.WriteLine(diff.TotalSeconds);
        }
    }
}