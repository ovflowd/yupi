using System;


using System.Data;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;

namespace Yupi.Messages.Groups
{
	public class PublishForumThreadMessageEvent : AbstractHandler
	{
		private Repository<Group> GroupRepository;

		public PublishForumThreadMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<Repository<Group>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();
			int threadId = request.GetInteger();
			string subject = request.GetString();
			string content = request.GetString();

			Group group = GroupRepository.FindBy(groupId);

			if (group == null)
				return;

			GroupForumThread thread;

			if (threadId == 0) {
				// New thread
				thread = new GroupForumThread ();
			} else {
				thread = group.Forum.GetThread (threadId);

				if (thread == null) {
					return;
				}
			}

			if (thread.Locked || thread.Hidden)
			{
				return;
			}

			GroupForumPost post = new GroupForumPost () {
				Content = content,
				Subject = subject,
				Poster = session.UserData.Info
			};

			group.Forum.ForumScore += 0.25;
			// TODO SAVE
			throw new NotImplementedException ();
			/*
			group.UpdateForum();

			if (threadId == 0)
			{
				router.GetComposer<GroupForumNewThreadMessageComposer> ().Compose (session, groupId, threadId, session.GetHabbo ().Id, subject, content, timestamp);
			}
			else
			{
				router.GetComposer<GroupForumNewResponseMessageComposer> ().Compose (
					session, groupId, threadId, group.Forum.ForumMessagesCount, session.GetHabbo (), timestamp);
			}*/
		}
	}
}

