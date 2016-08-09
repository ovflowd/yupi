using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using System.Linq;
using Yupi.Model;


namespace Yupi.Messages.User
{
	public class GetTalentsTrackMessageEvent : AbstractHandler
	{
		private Repository<Talent> TalentRepository;

		public GetTalentsTrackMessageEvent ()
		{
			TalentRepository = DependencyFactory.Resolve<Repository<Talent>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string trackType = message.GetString();
			TalentType talentType;

			if (TalentType.TryParse (trackType, out talentType)) {
				List<Talent> talents = TalentRepository.FilterBy (x => x.Type == talentType).ToList();

				router.GetComposer<TalentsTrackMessageComposer> ().Compose (session, talentType, talents);
			}
		}
	}
}

