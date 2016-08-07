using System;

namespace Yupi.Model.Domain
{
	public class MusicDiscBaseItem : FloorBaseItem
	{
		public virtual SongData Song { get; protected set; }
	}
}

