using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CameraStorageUrlMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string url)
        {
            // Do nothing by default.
        }
    }
}