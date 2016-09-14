using System;
using System.Collections.Generic;
using System.Text;

namespace Yupi.Model.Domain
{
    [Ignore]
    public class HeightMap
    {
        private static readonly char[] base32 =
        {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't', 'u', 'v'
        };

        private readonly short[] Map;

        public HeightMap(string map)
        {
            // TODO Use other representation internally!
            var rows = map.Split('\r');
            TotalY = rows.Length;
            TotalX = rows.Length > 0 ? rows[0].Length : 0;

            Map = new short[TotalX*TotalY];

            for (var y = 0; y < TotalY; ++y)
                for (var x = 0; x < TotalX; ++x)
                {
                    var height = ParseTileChar(rows[y][x]);
                    SetTileHeight(x, y, height);
                }
        }

        public virtual int MapSize
        {
            get { return Map.Length; }
        }

        public virtual int TotalX { get; }

        public virtual int TotalY { get; }

        public bool IsWalkable(Vector2 position)
        {
            // TODO Implement properly
            return GetTileHeight(position) >= 0;
        }

        public bool IsValidTile(Vector2 position)
        {
            return (position.X >= 0)
                   && (position.Y >= 0)
                   && (position.Y < TotalY)
                   && (position.X < TotalX);
        }

        public List<Vector2> GetNeighbours(Vector2 position)
        {
            List<Vector2> neighbours = new List<Vector2>(8);
            for (var x = -1; x <= 1; ++x)
                for (var y = -1; y <= 1; ++y)
                {
                    if ((y == 0) && (x == 0)) continue;

                    Vector2 neighbour = position + new Vector2(x, y);

                    if (IsValidTile(neighbour)) neighbours.Add(neighbour);
                }

            return neighbours;
        }

        private short ParseTileChar(char height)
        {
            if (height == 'x') return -1;

            var index = (short) Array.IndexOf(base32, height);

            if (index < 0) throw new ArgumentOutOfRangeException("height");

            return index;
        }

        private void SetTileHeight(int x, int y, short height)
        {
            Map[x*TotalY + y] = height;
        }

        /// <summary>
        ///     The height of the tile with furniture.
        /// </summary>
        /// <returns>The height.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public short GetTileHeight(int x, int y)
        {
            return Map[x*TotalY + y];
        }

        public short GetTileHeight(int index)
        {
            return Map[index];
        }

        public short GetTileHeight(Vector2 position)
        {
            return GetTileHeight((int) position.X, (int) position.Y);
        }

        public short GetTileHeight(Vector3 position)
        {
            return GetTileHeight((int) position.X, (int) position.Y);
        }

        /// <summary>
        ///     Get the internal map. Should only be used for testing.
        /// </summary>
        /// <returns>The map as string representation</returns>
        public string GetMap()
        {
            var sb = new StringBuilder();

            for (var x = 0; x < TotalX; ++x)
            {
                for (var y = 0; y < TotalY; ++y)
                {
                    var height = GetTileHeight(x, y);

                    if (height < 0) sb.Append("x");
                    else sb.Append(base32[height]);
                }

                if (x + 1 < TotalX) sb.Append('\r');
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var map = GetMap();
            var lines = map.Split('\r');

            sb.Append("  ");

            for (var y = 0; y < lines[0].Length; ++y)
            {
                sb.Append(" ");
                sb.Append(y.ToString("D2"));
            }

            sb.AppendLine();

            for (var x = 0; x < lines.Length; ++x)
            {
                sb.Append(x.ToString("D2"));
                sb.Append(" ");

                foreach (var tile in lines[x])
                {
                    sb.Append(tile.ToString().PadLeft(2));
                    sb.Append(" ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}