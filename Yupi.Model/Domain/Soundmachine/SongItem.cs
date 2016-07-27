using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Model.Domain
{  // TODO Use inheritance ?
	/// <summary>
	///     Class SongItem.
	/// </summary>
	public class SongItem
	{
		public virtual int Id { get; protected set; }

		public BaseItem BaseItem;

		public SongData Song;
	}
}