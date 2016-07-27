using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GroupDataEditMessageComposer : AbstractComposer<Group>
	{
		public override void Compose (Yupi.Protocol.ISender session, Group group)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0);
				message.AppendBool (true);
				message.AppendInteger (group.Id);
				message.AppendString (group.Name);
				message.AppendString (group.Description);
				message.AppendInteger (group.RoomId);
				message.AppendInteger (group.Colour1);
				message.AppendInteger (group.Colour2);
				message.AppendInteger (group.State);
				message.AppendInteger (group.AdminOnlyDeco);
				message.AppendBool (false);
				message.AppendString (string.Empty);

				// TODO Hardcoded stuff..

				string[] array = group.Badge.Replace("b", string.Empty).Split('s');

				message.AppendInteger(5);

				int num = 5 - array.Length;

				int num2 = 0;
				string[] array2 = array;

				foreach (string text in array2)
				{
					message.AppendInteger(text.Length >= 6
						? uint.Parse(text.Substring(0, 3))
						: uint.Parse(text.Substring(0, 2)));
					message.AppendInteger(text.Length >= 6
						? uint.Parse(text.Substring(3, 2))
						: uint.Parse(text.Substring(2, 2)));

					if (text.Length < 5)
						message.AppendInteger(0);
					else if (text.Length >= 6)
						message.AppendInteger(uint.Parse(text.Substring(5, 1)));
					else
						message.AppendInteger(uint.Parse(text.Substring(4, 1)));
				}

				while (num2 != num)
				{
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendInteger(0);
					num2++;
				}

				message.AppendString(group.Badge);
				message.AppendInteger(group.Members.Count);

				session.Send (message);
			}
		}
	}
}

