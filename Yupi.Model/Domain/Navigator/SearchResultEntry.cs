using System;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class SearchResultEntry
	{
		public NavigatorCategory Category { get; private set; }
		public IList<RoomData> Rooms { get; private set; }

		public SearchResultEntry (NavigatorCategory category, IList<RoomData> rooms)
		{
			this.Category = category;
			this.Rooms = rooms;
		}
		
	}
}

