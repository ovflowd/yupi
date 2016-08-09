using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;

namespace Yupi.Messages.Catalog
{
	public class GetRecyclerRewardsMessageEvent : AbstractHandler
	{
		private Repository<EcotronLevel> EcotronRepository;

		public GetRecyclerRewardsMessageEvent ()
		{
			EcotronRepository = DependencyFactory.Resolve<Repository<EcotronLevel>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<RecyclerRewardsMessageComposer> ().Compose (session, EcotronRepository.All ().ToArray ());
		}
	}
}

