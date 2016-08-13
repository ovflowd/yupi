using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class NavigatorSavedSearchesComposer : AbstractComposer<IList<UserSearchLog>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<UserSearchLog> searchLog)
		{
		 // Do nothing by default.
		}
	}
}
