using System;

using System.Data;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Groups
{
	public class GetGroupForumThreadRootMessageEvent : AbstractHandler
	{
		private IRepository<Group> GroupRepository;

		public GetGroupForumThreadRootMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<IRepository<Group>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger ();

			int startIndex = request.GetInteger();

			Group theGroup = GroupRepository.FindBy (groupId);

			if (theGroup == null) {
				return;
			}

			// TODO Magic constant!
			List<GroupForumThread> threads = theGroup.Forum.Threads.Skip (startIndex).Take (20).ToList();

			router.GetComposer<GroupForumThreadRootMessageComposer> ().Compose (session, groupId, startIndex, threads);
		}
	}
}

