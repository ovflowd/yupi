using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Collections;
using Yupi.Model.Domain.Components;
using Yupi.Model.Domain;
using System.Numerics;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateFurniStackMapMessageComposer : AbstractComposer<IList<Vector3>, RoomData>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<Vector3> affectedTiles, RoomData room)
		{
		 // Do nothing by default.
		}
	}
}
