using System;

namespace Yupi.Model.Domain
{ 
	// TODO SongItem vs MusicDiscItem
	public class SongItem : FloorItem<MusicDiscBaseItem>
	{
		// TODO remove
		[Ignore]
		public virtual SongData Song { 
			get { 
				return BaseItem.Song;
			}
		}

		public virtual DateTime CreatedAt { get; set; }

		public override void TryParseExtraData (string data)
		{
			CreatedAt = DateTime.Now;
		}

		public override string GetExtraData ()
		{
			return string.Join ("\n", Owner.UserName, CreatedAt.Year, CreatedAt.Month, CreatedAt.Day, 
				Song.LengthSeconds, Song.Name);
		}
	}
}