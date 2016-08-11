using System;
using Yupi.Model.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Controller
{
	public class RoomManager
	{
		private List<Room> _loadedRooms;

		public IReadOnlyList<Room> LoadedRooms {
			get {
				return _loadedRooms.AsReadOnly ();
			}
		}

		public RoomManager ()
		{
			_loadedRooms = new List<Room> (); 
		}

		public bool isLoaded(RoomData room) {
			return _loadedRooms.Any (x => x.Data == room);
		}
	}
}

