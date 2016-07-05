using System;


using Yupi.Messages.Notification;
using Yupi.Messages.User;

namespace Yupi.Messages.Rooms
{
	public class SaveFloorPlanEditorMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
	
			Room room = session.GetHabbo ().CurrentRoom;

			if (room == null || !room.CheckRights (session, true)) {
				session.SendNotif (Yupi.GetLanguage ().GetVar ("user_is_not_in_room"));
				return;
			}
				
			string heightMap = request.GetString ();
			int doorX = request.GetInteger ();
			int doorY = request.GetInteger ();
			int doorOrientation = request.GetInteger ();
			int wallThickness = request.GetInteger ();
			int floorThickness = request.GetInteger ();
			int wallHeight = request.GetInteger ();

			if (heightMap.Length < 2) {
				session.SendNotif (Yupi.GetLanguage ().GetVar ("invalid_room_length"));
				return;
			}

			if (wallThickness < -2 || wallThickness > 1)
				wallThickness = 0;

			if (floorThickness < -2 || floorThickness > 1)
				floorThickness = 0;

			if (doorOrientation < 0 || doorOrientation > 8)
				doorOrientation = 2;

			if (wallHeight < -1 || wallHeight > 16)
				wallHeight = -1;

			char[] validLetters = {
					'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g',
					'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', '\r'
				};

			if (heightMap.Any (letter => !validLetters.Contains (letter))) {
				session.SendNotif (Yupi.GetLanguage ().GetVar ("user_floor_editor_error"));
				return;
			}

			if (heightMap.Last () == Convert.ToChar (13))
				heightMap = heightMap.Remove (heightMap.Length - 1);

			if (heightMap.Length > 1800) {

				// TODO Hardcoded string
				router.GetComposer<SuperNotificationMessageComposer> ().Compose (session, string.Empty,
					"(general): too large height (max 64 tiles)\r(general): too large area (max 1800 tiles)", "", "", "floorplan_editor.error", 1);
				return;
			}

			if (heightMap.Split ((char)13).Length - 1 < doorY) {
				router.GetComposer<SuperNotificationMessageComposer> ().Compose (session, string.Empty,
					"Y: Door is in invalid place.", "", "", "floorplan_editor.error", 1);
				return;
			}

			string[] lines = heightMap.Split ((char)13);
			int lineWidth = lines [0].Length;

			for (int i = 1; i < lines.Length; i++) {
				if (lines [i].Length != lineWidth) {
					router.GetComposer<SuperNotificationMessageComposer> ().Compose (session, string.Empty,
						"(general): Line " + (i + 1).ToString () + " is of different length than line 1", "", "", "floorplan_editor.error", 1);
					return;
				}
			}

			char charDoor = lines [doorY] [doorX];

			// TODO Shouldn't this be int?
			double doorZ;

			if (charDoor >= 'a' && charDoor <= 'w') // a-w
					doorZ = charDoor - 'W'; // TODO Shouldn't this be lower case?
			else
				double.TryParse (charDoor.ToString (), out doorZ);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				// TODO REPLACE is a MySQL specific extension!
				queryReactor.SetQuery ("REPLACE INTO rooms_models_customs (roomid,door_x,door_y,door_z,door_dir,heightmap,poolmap)" +
				" VALUES (@room, @doorX,@doorY, @doorZ, @door_dir, @newmodel,'')");

				queryReactor.AddParameter ("room", room.RoomId);
				queryReactor.AddParameter ("doorX", doorX);
				queryReactor.AddParameter ("doorY", doorY);
				queryReactor.AddParameter ("doorZ", doorZ);
				queryReactor.AddParameter ("door_dir", doorOrientation);
				queryReactor.AddParameter ("newmodel", heightMap);
				queryReactor.RunQuery ();

				room.RoomData.WallHeight = wallHeight;
				room.RoomData.WallThickness = wallThickness;
				room.RoomData.FloorThickness = floorThickness;
				room.RoomData.Model.DoorZ = doorZ;

				Yupi.GetGame ().GetAchievementManager ().ProgressUserAchievement (session, "ACH_RoomDecoHoleFurniCount", 1);

				queryReactor.SetQuery ("UPDATE rooms_data SET model_name = 'custom', wallthick = @wallthick, floorthick = @floorthick, walls_height = @walls_height WHERE id = @room");
				queryReactor.AddParameter ("wallthick", wallThickness);
				queryReactor.AddParameter ("floorthick", floorThickness);
				queryReactor.AddParameter ("walls_height", wallHeight);
				queryReactor.AddParameter ("room", room.RoomId);
				queryReactor.RunQuery ();

				RoomModel roomModel = new RoomModel (doorX, doorY, doorZ, doorOrientation, heightMap, string.Empty, false, string.Empty);
				Yupi.GetGame ().GetRoomManager ().UpdateCustomModel (room.RoomId, roomModel);

				room.ResetGameMap ("custom", wallHeight, wallThickness, floorThickness);

				Yupi.GetGame ().GetRoomManager ().UnloadRoom (room, "Reload floor");

				// TODO Sure this shouldn't be a room broadcast?
				router.GetComposer<RoomForwardMessageComposer> ().Compose (session, room.RoomId);
			}
		}

	}
}