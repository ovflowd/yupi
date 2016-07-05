using System;
using System.Collections.Specialized;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Navigator
{
	public class FlatCategoriesMessageComposer : AbstractComposer<HybridDictionary, int>
	{
		public override void Compose (Yupi.Protocol.ISender session, HybridDictionary categories, int userRank)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.StartArray();

				foreach (PublicCategory flatCat in categories.Values)
				{
					message.Clear();

					if (flatCat == null)
						continue;

					message.AppendInteger(flatCat.Id);
					message.AppendString(flatCat.Caption);
					message.AppendBool(flatCat.MinRank <= userRank);
					message.AppendBool(false);
					message.AppendString("NONE");
					message.AppendString(string.Empty);
					message.AppendBool(false);

					message.SaveArray();
				}

				message.EndArray();
				session.Send (message);
			}
		}
	}
}

