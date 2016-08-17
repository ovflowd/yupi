using System;

namespace Yupi.Messages.Contracts
{
	public abstract class AuthenticationOKMessageComposer : Contracts.AbstractComposerEmpty
	{
		public override void Compose (Yupi.Protocol.ISender session)
		{
			// Do nothing
		}
	}
}

