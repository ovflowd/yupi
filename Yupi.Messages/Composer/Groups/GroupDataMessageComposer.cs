using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupDataMessageComposer : Contracts.GroupDataMessageComposer
    {
        public override void Compose(ISender session, Group group, UserInfo habbo, bool newWindow = false)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                // TODO Hide conversion between Unix <-> DateTime
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var dateTime2 = dateTime.AddSeconds(group.CreateTime);

                message.AppendInteger(group.Id);
                message.AppendBool(true);
                message.AppendInteger(group.State);
                message.AppendString(group.Name);
                message.AppendString(group.Description);
                message.AppendString(group.Badge);
                message.AppendInteger(group.Room.Id);
                message.AppendString(group.Room.Name);

                /*
                message.AppendInteger(@group.CreatorId == session.GetHabbo().Id
                    ? 3
                    : (group.Requests.ContainsKey(session.GetHabbo().Id)
                        ? 2
                        : (group.Members.ContainsKey(session.GetHabbo().Id) ? 1 : 0)));
                message.AppendInteger(group.Members.Count);
                message.AppendBool(session.GetHabbo().FavouriteGroup == group.Id);
                message.AppendString($"{dateTime2.Day.ToString("00")}-{dateTime2.Month.ToString("00")}-{dateTime2.Year}");
                message.AppendBool(group.CreatorId == session.GetHabbo().Id);
                message.AppendBool(group.Admins.ContainsKey(session.GetHabbo().Id));
                message.AppendString(Yupi.GetHabboById(@group.CreatorId) == null
                    ? string.Empty
                    : Yupi.GetHabboById(group.CreatorId).Name);
                message.AppendBool(newWindow);
                message.AppendBool(group.AdminOnlyDeco == 0u);
                message.AppendInteger(group.Requests.Count);
                message.AppendBool(group.Forum.Id != 0);
                session.Send (message);
                */
                throw new NotImplementedException();
            }
        }
    }
}