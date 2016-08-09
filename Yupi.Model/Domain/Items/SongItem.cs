using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Model.Domain
{ 
	public class SongItem : FloorItem<MusicDiscBaseItem>
	{
		public virtual SongData Song { 
			get { 
				return BaseItem.Song;
			}
		}
	}
}