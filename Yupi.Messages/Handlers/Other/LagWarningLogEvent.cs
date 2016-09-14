using System;
using Yupi.Model.Domain;
using Yupi.Protocol.Buffers;
using Yupi.Protocol;

namespace Yupi.Messages.Other
{
	public class LagWarningLogEvent : AbstractHandler
	{
		public override void HandleMessage (Habbo session, ClientMessage request, IRouter router)
		{
			int lagCount = request.GetInteger ();

			// TODO Now what?! 
		}
	}
}

