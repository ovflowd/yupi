using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Model.Domain
{ 
	/// <summary>
	///     Class SongItem.
	/// </summary>
	public class SongItem : Item<MusicDiscBaseItem>
	{
		// TODO Remove
		[Ignore]
		public virtual SongData Song { 
			get { 
				return BaseItem.Song;
			}
		}
	}
}