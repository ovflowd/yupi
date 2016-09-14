using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class DimmerDataMessageComposer : AbstractComposer<MoodlightData>
	{
		public override void Compose(Yupi.Protocol.ISender session, MoodlightData moodlight)
		{
		 // Do nothing by default.
		}
	}
}
