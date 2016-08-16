using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;
using System.Globalization;

namespace Yupi.Messages.Contracts
{
	public abstract class SetRoomUserMessageComposer : AbstractComposer<List<RoomEntity>>
	{
		public override void Compose(Yupi.Protocol.ISender room, List<RoomEntity> users)
		{
		 // Do nothing by default.
		}
		public virtual void Compose(Yupi.Protocol.ISender room, RoomEntity user)
		{
		 // Do nothing by default.
		}
	}
}
