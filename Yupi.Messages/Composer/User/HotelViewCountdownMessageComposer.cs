using System;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class HotelViewCountdownMessageComposer : Contracts.HotelViewCountdownMessageComposer
    {
        public override void Compose(ISender session, string time)
        {
            DateTime date;
            DateTime.TryParse(time, out date);
            var diff = date - DateTime.Now;

            using (var message = Pool.GetMessageBuffer(Id))
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