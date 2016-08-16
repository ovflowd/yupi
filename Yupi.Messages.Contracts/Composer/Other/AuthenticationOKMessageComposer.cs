using System;

namespace Yupi.Messages.Contracts
{
	public abstract class AuthenticationOKMessageComposer : Contracts.AbstactComposerEmpty
	{
		public override void Compose (Yupi.Protocol.ISender session)
		{
			// Do nothing
		}
	}
}

