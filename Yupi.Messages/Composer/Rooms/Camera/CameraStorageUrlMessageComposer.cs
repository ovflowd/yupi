using Yupi.Protocol;

namespace Yupi.Messages.Camera
{
    public class CameraStorageUrlMessageComposer : Contracts.CameraStorageUrlMessageComposer
    {
        public override void Compose(ISender session, string url)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(url);
                session.Send(message);
            }
        }
    }
}