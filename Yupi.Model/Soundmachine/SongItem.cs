using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Model.Songmachine
{  // TODO Use inheritance ?
	/// <summary>
	///     Class SongItem.
	/// </summary>
	public class SongItem
	{
		/// <summary>
		///     The base item
		/// </summary>
		public Item BaseItem;

		/// <summary>
		///     The extra data
		/// </summary>
		public string ExtraData;

		public SongData Song;
	}
}