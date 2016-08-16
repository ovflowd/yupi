using System;


using System.Data;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;

namespace Yupi.Messages.Groups
{
	public class ReadForumThreadMessageEvent : AbstractHandler
	{
		private Repository<Group> GroupRepository;

		public ReadForumThreadMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<Repository<Group>> ();
		}

		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger ();
			int threadId = request.GetInteger ();
			int startIndex = request.GetInteger ();

			request.GetInteger (); // TODO Unused

			Group theGroup = GroupRepository.FindBy (groupId);

			if (theGroup == null) {
				return;
			}

			GroupForumThread thread = theGroup.Forum.GetThread (threadId);

			if (thread == null) {
				return;
			}

			// TODO Magic constant
			List<GroupForumPost> posts = thread.Posts.Skip(startIndex).Take (20).ToList ();

			router.GetComposer<GroupForumReadThreadMessageComposer> ().Compose (session, groupId, threadId, startIndex, posts);
		}
	}
}

