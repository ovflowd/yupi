using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Globalization;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateUserStatusMessageComposer : AbstractComposer<List<RoomEntity>>
	{
		public override void Compose(Yupi.Protocol.ISender session, List<RoomEntity> entities)
		{
		 // Do nothing by default.
		}

		public virtual void Compose ( Yupi.Protocol.ISender session, RoomEntity user) {
			// TODO Remove?
		}
	}
}
