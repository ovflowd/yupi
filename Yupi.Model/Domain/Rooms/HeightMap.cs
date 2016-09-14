using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

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
			TotalY = rows.Length;
			TotalX = rows.Length > 0 ? rows [0].Length : 0;

			Map = new short[TotalX * TotalY];

			for (int y = 0; y < TotalY; ++y) {
				for (int x = 0; x < TotalX; ++x) {
					short height = ParseTileChar (rows [y] [x]);
					SetTileHeight (x, y, height);
				}
			}
		}

		public bool IsWalkable (Vector2 position)
		{
			// TODO Implement properly
			return GetTileHeight (position) >= 0;
		}

		public bool IsValidTile (Vector2 position)
		{
			return position.X >= 0
			&& position.Y >= 0
			&& position.Y < TotalY
			&& position.X < TotalX;
		}

		public List<Vector2> GetNeighbours (Vector2 position)
		{
			List<Vector2> neighbours = new List<Vector2> (8);
			for (int x = -1; x <= 1; ++x) {
				for (int y = -1; y <= 1; ++y) {
					if (y == 0 && x == 0) {
						continue;
					}

					Vector2 neighbour = position + new Vector2 (x, y);

					if (IsValidTile (neighbour)) {
						neighbours.Add (neighbour);
					}
				}
			}

			return neighbours;
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

		public short GetTileHeight (Vector2 position)
		{
			return GetTileHeight ((int)position.X, (int)position.Y);
		}

		public short GetTileHeight (Vector3 position)
		{
			return GetTileHeight ((int)position.X, (int)position.Y);
		}

		/// <summary>
		/// Get the internal map. Should only be used for testing.
		/// </summary>
		/// <returns>The map as string representation</returns>
		public string GetMap ()
		{
			StringBuilder sb = new StringBuilder ();

			for (int x = 0; x < this.TotalX; ++x) {
				for (int y = 0; y < this.TotalY; ++y) {
					short height = GetTileHeight (x, y);

					if (height < 0) {
						sb.Append ("x");
					} else {
						sb.Append (base32 [height]);
					}
				}

				if (x + 1 < this.TotalX) {
					sb.Append ('\r');
				}
			}

			return sb.ToString ();
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();

			string map = GetMap ();
			string[] lines = map.Split ('\r');

			sb.Append ("  ");

			for (int y = 0; y < lines [0].Length; ++y) {
				sb.Append (" ");
				sb.Append (y.ToString("D2"));
			}

			sb.AppendLine ();

			for (int x = 0; x < lines.Length; ++x) {
				sb.Append (x.ToString("D2"));
				sb.Append (" ");

				foreach (char tile in lines[x]) {
					sb.Append (tile.ToString ().PadLeft(2));
					sb.Append (" ");
				}

				sb.AppendLine ();
			}

			return sb.ToString ();
		}
	}
}

