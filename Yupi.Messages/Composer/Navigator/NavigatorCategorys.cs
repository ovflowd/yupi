using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;


namespace Yupi.Messages.Navigator
{
	// TODO Rename
	public class NavigatorCategorys : AbstractComposer<IList<NavigatorCategory>>
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<NavigatorCategory> categories)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(categories.Count);

				foreach (NavigatorCategory category in categories)
					message.AppendString(category.Caption);
				session.Send (message);
			}
		}
	}
}

