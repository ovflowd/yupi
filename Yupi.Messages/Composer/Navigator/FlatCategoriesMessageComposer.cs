using System;
using System.Collections.Specialized;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;


namespace Yupi.Messages.Navigator
{
	public class FlatCategoriesMessageComposer : Yupi.Messages.Contracts.FlatCategoriesMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<NavigatorCategory> categories, int userRank)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (categories.Count);

				foreach (NavigatorCategory flatCat in categories)
				{
					message.AppendInteger(flatCat.Id);
					message.AppendString(flatCat.Caption);
					message.AppendBool(flatCat.MinRank <= userRank); // TODO What is this usually used for?
					message.AppendBool(false);
					message.AppendString("NONE");
					message.AppendString(string.Empty);
					message.AppendBool(false);
				}

				session.Send (message);
			}
		}
	}
}

