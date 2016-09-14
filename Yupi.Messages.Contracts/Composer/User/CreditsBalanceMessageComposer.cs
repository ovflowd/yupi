using System;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
	public class CreditsBalanceMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISender session, int credits)
		{
			
		}
	}
}

