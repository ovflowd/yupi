using System;
using Yupi.Model.Domain;
using System.Collections.Generic;

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
	}
}

