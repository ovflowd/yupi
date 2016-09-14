using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GroupUpdateSettingsMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;
        private readonly RoomManager RoomManager;

        public GroupUpdateSettingsMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var state = request.GetUInt32();
            var admindeco = request.GetUInt32();

            var group = GroupRepository.FindBy(groupId);

            if (group?.Creator != session.Info)
                return;

            group.State = state;
            group.AdminOnlyDeco = admindeco;

            GroupRepository.Save(group);

            var room = RoomManager.GetIfLoaded(group.Room);
            /*
            if (room != null)
            {
                foreach (RoomUser current in room.GetRoomUserManager().GetRoomUsers())
                {
                    if (room.Data.Owner != current.UserId && !group.Admins.ContainsKey(current.UserId) &&
                        group.Members.ContainsKey(current.UserId))
                    {
                        // TODO USE F*KING ENUMS!
                        if (admindeco == 1u)
                        {
                            current.RemoveStatus("flatctrl 1");
                            router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (current.GetClient(), 0);
                        }
                        else
                        {
                            if (admindeco == 0u && !current.Statusses.ContainsKey("flatctrl 1"))
                            {
                                current.AddStatus("flatctrl 1", string.Empty);
                                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (current.GetClient(), 1);
                            }
                        }

                        current.UpdateNeeded = true;
                    }
                }
            }
            router.GetComposer<GroupDataMessageComposer> ().Compose (session.GetHabbo().CurrentRoom, group, session.GetHabbo());
                */
            throw new NotImplementedException();
        }
    }
}