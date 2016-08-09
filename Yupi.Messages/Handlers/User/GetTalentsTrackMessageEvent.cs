using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;


namespace Yupi.Messages.User
{
	public class GetTalentsTrackMessageEvent : AbstractHandler
	{
		private Repository<Talent> TalentRepository;

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string trackType = message.GetString();
			// TODO Magic constant
			List<Talent> talents = TalentRepository.FilterBy(x => x.Type == Talent.TalentType.Citizenship);

			router.GetComposer<TalentsTrackMessageComposer> ().Compose (session, trackType, talents);
		}
	}
}

