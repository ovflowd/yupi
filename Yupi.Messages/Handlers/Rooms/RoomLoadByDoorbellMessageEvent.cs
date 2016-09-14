namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Messages.Groups;

    public class RoomLoadByDoorbellMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*Room currentLoadingRoom = CurrentLoadingRoom;

            if (currentLoadingRoom == null || !session.GetHabbo().LoadingChecksPassed)
                return queuedServerMessageBuffer;

            if (session.GetHabbo().FavouriteGroup > 0u)
            {
                if (CurrentLoadingRoom.RoomData.Group != null && !CurrentLoadingRoom.LoadedGroups.ContainsKey(CurrentLoadingRoom.RoomData.Group.Id))
                    CurrentLoadingRoom.LoadedGroups.Add(CurrentLoadingRoom.RoomData.Group.Id, CurrentLoadingRoom.RoomData.Group.Badge);

                if (!CurrentLoadingRoom.LoadedGroups.ContainsKey(Session.GetHabbo().FavouriteGroup) && Yupi.GetGame().GetGroupManager().GetGroup(Session.GetHabbo().FavouriteGroup) != null)
                    CurrentLoadingRoom.LoadedGroups.Add(Session.GetHabbo().FavouriteGroup, Yupi.GetGame().GetGroupManager().GetGroup(Session.GetHabbo().FavouriteGroup).Badge);
            }

            router.GetComposer<RoomGroupMessageComposer> ().Compose (session);
            router.GetComposer<InitialRoomInfoMessageComposer> ().Compose (session, currentLoadingRoom);

            if (session.GetHabbo().SpectatorMode)
            {
                router.GetComposer<SpectatorModeMessageComposer> ().Compose (session);
            }

            if (currentLoadingRoom.RoomData.WallPaper != "0.0")
            {
                router.GetComposer<RoomSpacesMessageComposer> ().Compose (session, RoomSpacesMessageComposer.RoomSpacesType.WALLPAPER, currentLoadingRoom);
            }

            if (currentLoadingRoom.RoomData.Floor != "0.0")
            {
                router.GetComposer<RoomSpacesMessageComposer> ().Compose (session, RoomSpacesMessageComposer.RoomSpacesType.FLOOR, currentLoadingRoom);
            }

            router.GetComposer<RoomSpacesMessageComposer> ().Compose (session, RoomSpacesMessageComposer.RoomSpacesType.LANDSCAPE, currentLoadingRoom);

            if (currentLoadingRoom.CheckRights(session, true))
            {
                // TODO Magic number
                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 4);
                router.GetComposer<HasOwnerRightsMessageComposer> ().Compose (session);
            }
            else if (currentLoadingRoom.CheckRights(session, false, true))
            {
                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 1);
            }
            else
            {
                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (session, 0);
            }

            router.GetComposer<RoomRatingMessageComposer> ().Compose (session, urrentLoadingRoom.RoomData.Score,
                !session.GetHabbo ().RatedRooms.Contains (currentLoadingRoom.RoomId) && !currentLoadingRoom.CheckRights (session, true)); // TODO Refactor

            router.GetComposer<RoomUpdateMessageComposer> ().Compose (session, currentLoadingRoom.RoomId);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}