using System;
using Yupi.Util;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Messenger
{
	public class DeleteFriendMessageEvent : AbstractHandler
	{
		private RelationshipController RelationshipController;

		public DeleteFriendMessageEvent ()
		{
			RelationshipController = DependencyFactory.Resolve<RelationshipController> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int count = request.GetInteger();
			for (int i = 0; i < count; i++)
			{
				int friendId = request.GetInteger();

				RelationshipController.Remove (session, friendId);
			}
		}
	}
}

