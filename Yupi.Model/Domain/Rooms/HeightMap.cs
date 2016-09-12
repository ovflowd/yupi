using System;
using System.Numerics;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class HeightMap
	{
		private short[] Map;

		public virtual int MapSize {
			get {
				return Map.Length;
			}
		}

		public virtual int TotalX { get; private set; }

		public virtual int TotalY { get; private set; }

		private static readonly char[] base32 = new char[] {
			'0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
			'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v'
		};

		public HeightMap (string map)
		{
			// TODO Use other representation internally!
			string[] rows = map.Split ('\r');	
			TotalX = rows.Length;
			TotalY = rows.Length > 0 ? rows [0].Length : 0;

			Map = new short[TotalX * TotalY];

			for (int y = 0; y < TotalY; ++y) {
				for (int x = 0; x < TotalX; ++x) {
					// TODO Parse to byte/short
					short height = ParseTileChar (rows [x] [y]);
					SetTileHeight (x, y, height);
				}
			}
		}

		private short ParseTileChar (char height)
		{
			if (height == 'x') {
				return -1;
			}

			short index = (short)Array.IndexOf (base32, height);

			if (index < 0) {
				throw new ArgumentOutOfRangeException ("height");
			}

			return index;
		}

		private void SetTileHeight (int x, int y, short height)
		{
			Map [x * TotalY + y] = height;
		}

		/// <summary>
		/// The height of the tile with furniture.
		/// </summary>
		/// <returns>The height.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public short GetTileHeight (int x, int y)
		{
			return Map [x * TotalY + y];
		}

		public short GetTileHeight (int index)
		{
			return Map [index];
		}

		public short GetTileHeight(Vector3 position) {
			return GetTileHeight ((int)position.X, (int)position.Y);
		}
	}
}

