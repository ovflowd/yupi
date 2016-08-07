using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model;

namespace Yupi.Messages.Contracts
{
	public abstract class LoadModerationToolMessageComposer : AbstractComposer<ModerationTool, UserInfo>
	{
		public override void Compose(Yupi.Protocol.ISender session, ModerationTool tool, UserInfo user)
		{
		 // Do nothing by default.
		}
	}
}
