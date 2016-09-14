using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupDataEditMessageComposer : Contracts.GroupDataEditMessageComposer
    {
        public override void Compose(ISender session, Group group)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0);
                message.AppendBool(true);
                message.AppendInteger(group.Id);
                message.AppendString(group.Name);
                message.AppendString(group.Description);
                message.AppendInteger(group.Room.Id);
                message.AppendInteger(group.Colour1.Colour);
                message.AppendInteger(group.Colour2.Colour);
                message.AppendInteger(group.State);
                message.AppendInteger(group.AdminOnlyDeco);
                message.AppendBool(false);
                message.AppendString(string.Empty);

                // TODO Hardcoded stuff..

                var array = group.Badge.Replace("b", string.Empty).Split('s');

                message.AppendInteger(5);

                var num = 5 - array.Length;

                var num2 = 0;
                var array2 = array;

                foreach (var text in array2)
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

                session.Send(message);
            }
        }
    }
}