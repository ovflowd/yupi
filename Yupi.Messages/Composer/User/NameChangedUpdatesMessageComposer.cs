using System.Collections.Generic;
using Yupi.Messages.Encoders;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    // TODO Shouldn't this be called NameCHECKED

    public class NameChangedUpdatesMessageComposer : Contracts.NameChangedUpdatesMessageComposer
    {
        public override void Compose(ISender session, Status status, string newName, IList<string> alternatives = null)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) status);
                message.AppendString(newName);

                if (alternatives == null) message.AppendInteger(0); // TODO Magic constant
                else message.Append(alternatives);
                session.Send(message);
            }
        }
    }
}