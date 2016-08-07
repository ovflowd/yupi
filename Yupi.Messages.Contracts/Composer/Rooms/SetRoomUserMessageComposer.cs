using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;
using System.Globalization;

namespace Yupi.Messages.Contracts
{
	public abstract class SetRoomUserMessageComposer : AbstractComposer<List<RoomEntity>, bool>
	{
		public override void Compose(Yupi.Protocol.ISender room, List<RoomEntity> users, bool hasPublicPool = false)
		{
		 // Do nothing by default.
		}
		public virtual void Compose(Yupi.Protocol.ISender room, RoomEntity user, bool hasPublicPool = false)
		{
		 // Do nothing by default.
		}
	}
}
