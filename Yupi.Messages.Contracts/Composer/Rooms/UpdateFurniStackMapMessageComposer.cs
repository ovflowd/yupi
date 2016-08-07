using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Collections;
using Yupi.Model.Domain.Components;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateFurniStackMapMessageComposer : AbstractComposer<IList<Vector>, RoomData>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<Vector> affectedTiles, RoomData room)
		{
		 // Do nothing by default.
		}
	}
}
