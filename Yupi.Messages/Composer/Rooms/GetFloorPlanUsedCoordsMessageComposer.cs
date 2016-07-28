using System;
using System.Drawing;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class GetFloorPlanUsedCoordsMessageComposer : AbstractComposer<Point[]>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Point[] coords)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(coords.Length);

				foreach (Point point in coords)
				{
					message.AppendInteger(point.X);
					message.AppendInteger(point.Y);
				}

				session.Send (message);
			}
		}
	}
}

