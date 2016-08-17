using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;


namespace Yupi.Messages.User
{
	public class HotelViewHallOfFameMessageComposer : Yupi.Messages.Contracts.HotelViewHallOfFameMessageComposer
	{
		private IRepository<HallOfFameElement> FameRepository;

		public HotelViewHallOfFameMessageComposer ()
		{
			FameRepository = DependencyFactory.Resolve<IRepository<HallOfFameElement>> ();
		}

		public override void Compose (Yupi.Protocol.ISender session, string code)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (code);

				List<HallOfFameElement> rankings = FameRepository.FilterBy (x => x.Competition == code).OrderByDescending (x => x.Score).ToList ();

				message.AppendInteger (rankings.Count);

				for (int rank = 1; rank <= rankings.Count; ++rank) {
					HallOfFameElement element = rankings [rank - 1];
					message.AppendInteger (element.User.Id);
					message.AppendString (element.User.UserName);
					message.AppendString (element.User.Look);
					message.AppendInteger (rank);
					message.AppendInteger (element.Score);
				}
				session.Send (message);
			}
		}
	}
}

