using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;


namespace Yupi.Messages.Groups
{
	public class UpdateForumSettingsMessageEvent : AbstractHandler
	{
		private IRepository<Group> GroupRepository;

		public UpdateForumSettingsMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<IRepository<Group>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger ();
			uint whoCanRead = request.GetUInt32();
			uint whoCanPost = request.GetUInt32();
			uint whoCanThread = request.GetUInt32();
			uint whoCanMod = request.GetUInt32();

			Group group = GroupRepository.FindBy (groupId);

			if (group?.Creator != session.Info)
				return;

			// TODO Check rights?!
			group.Forum.WhoCanRead = whoCanRead;
			group.Forum.WhoCanPost = whoCanPost;
			group.Forum.WhoCanThread = whoCanThread;
			group.Forum.WhoCanMod = whoCanMod;

			GroupRepository.Save (group);
			router.GetComposer<GroupForumDataMessageComposer> ().Compose (session, group, session.Info);
		}
	}
}

