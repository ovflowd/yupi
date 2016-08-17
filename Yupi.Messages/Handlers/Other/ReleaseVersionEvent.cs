using System;

namespace Yupi.Messages.Other
{
	public class ReleaseVersionEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			session.ReleaseName = request.GetString ();
			string unknown1 = request.GetString ();
			int unknown2 = request.GetInteger();
			int unknown3 = request.GetInteger ();
		}
	}
}

