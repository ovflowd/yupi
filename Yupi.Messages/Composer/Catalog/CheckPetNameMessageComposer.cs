using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class CheckPetNameMessageComposer : Contracts.CheckPetNameMessageComposer
    {
        public override void Compose(ISender session, int status, string name)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(status);
                message.AppendString(name);
                session.Send(message);
            }
        }
    }
}