using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class HeightMapMessageComposer : AbstractComposer<HeightMap>
    {
        public override void Compose(ISender session, HeightMap map)
        {
            // Do nothing by default.
        }
    }
}