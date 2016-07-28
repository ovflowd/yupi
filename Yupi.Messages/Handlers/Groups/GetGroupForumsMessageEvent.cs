using System;
using System.Collections.Generic;


using System.Data;
using System.Linq;

namespace Yupi.Messages.Groups
{
	public class GetGroupForumsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int selectType = request.GetInteger();
			int startIndex = request.GetInteger();

			router.GetComposer<GroupForumListingsMessageComposer> ().Compose (session, selectType, startIndex);
		}
	}
}

