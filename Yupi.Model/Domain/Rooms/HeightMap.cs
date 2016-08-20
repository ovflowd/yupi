using System;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class HeightMap
	{
		public char[] Map;

		public virtual int TotalX { get; private set; }

		public virtual int TotalY { get; private set; }

		public HeightMap (string map)
		{
			// TODO Use other representation internally!
			string[] rows = map.Split ('\r');	
			TotalX = rows.Length;
			TotalY = rows.Length > 0 ? rows [0].Length : 0;

			Map = new char[TotalX*TotalY];

			for (int x = 0; x < TotalX; ++x) {
				for (int y = 0; y < TotalY; ++y) {
					// TODO Parse to byte/short
					SetTileHeight (x, y, rows [x] [y]);
				}
			}
		}

		private void SetTileHeight (int x, int y, char value)
		{
			Map [x + y * TotalY] = value;
		}

		private char GetTileHeight(int x, int y) {
			return Map [x + y * TotalY];
		}

		/// <summary>
		/// The height of the tile with furniture.
		/// </summary>
		/// <returns>The height.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public short TileHeight (int x, int y)
		{
			// TODO Implement
			return (short)(GetTileHeight(x, y) == 'x' ? -1 : 0);
		}
	}
}

