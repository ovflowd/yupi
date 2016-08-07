using Yupi.Protocol.Buffers;
using System.Globalization;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class OpenHelpToolMessageComposer : AbstractComposer<UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
		{
		 // Do nothing by default.
		}
	}
}
