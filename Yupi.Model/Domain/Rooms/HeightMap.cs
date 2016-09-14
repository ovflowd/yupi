// ---------------------------------------------------------------------------------
// <copyright file="HeightMap.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    [Ignore]
    public class HeightMap
    {
        #region Fields

        private static readonly char[] base32 = new[]
                                                    {
                                                        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b',
                                                        'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                                                        'p', 'q', 'r', 's', 't', 'u', 'v'
                                                    };

        private short[] Map;

        #endregion Fields

        #region Properties

        public virtual int MapSize
        {
            get
            {
                return Map.Length;
            }
        }

        public virtual int TotalX
        {
            get; private set;
        }

        public virtual int TotalY
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public HeightMap(string map)
        {
            // TODO Use other representation internally!
            string[] rows = map.Split('\r');
            TotalY = rows.Length;
            TotalX = rows.Length > 0 ? rows[0].Length : 0;

            Map = new short[TotalX * TotalY];

            for (int y = 0; y < TotalY; ++y)
            {
                for (int x = 0; x < TotalX; ++x)
                {
                    short height = ParseTileChar(rows[y][x]);
                    SetTileHeight(x, y, height);
                }
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get the internal map. Should only be used for testing.
        /// </summary>
        /// <returns>The map as string representation</returns>
        public string GetMap()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < this.TotalY; ++y)
            {
                for (int x = 0; x < this.TotalX; ++x)
                {
                    short height = GetTileHeight(x, y);

                    if (height < 0)
                    {
                        sb.Append("x");
                    }
                    else
                    {
                        sb.Append(base32[height]);
                    }
                }

                if (y + 1 < this.TotalY)
                {
                    sb.Append('\r');
                }
            }

            return sb.ToString();
        }

        public Vector2[] GetNeighbours(Vector2 tile)
        {
            return new Vector2[]
            {
                new Vector2(tile.X - 1, tile.Y),
                new Vector2(tile.X, tile.Y + 1),
                new Vector2(tile.X, tile.Y - 1),
                new Vector2(tile.X + 1, tile.Y),
                new Vector2(tile.X + 1, tile.Y - 1),
                new Vector2(tile.X + 1, tile.Y + 1),
                new Vector2(tile.X - 1, tile.Y - 1),
                new Vector2(tile.X - 1, tile.Y + 1)
            };
        }

        /// <summary>
        /// The height of the tile with furniture.
        /// </summary>
        /// <returns>The height.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public short GetTileHeight(int x, int y)
        {
            return Map[x * TotalY + y];
        }

        public short GetTileHeight(int index)
        {
            return Map[index];
        }

        public short GetTileHeight(Vector2 position)
        {
            return GetTileHeight((int)position.X, (int)position.Y);
        }

        public short GetTileHeight(Vector3 position)
        {
            return GetTileHeight((int)position.X, (int)position.Y);
        }

        public bool IsValidTile(Vector2 position)
        {
            return position.X >= 0 && position.Y >= 0 && position.Y < TotalY && position.X < TotalX;
        }

        public bool IsWalkable(Vector2 position)
        {
            return IsValidTile(position) && GetTileHeight(position) >= 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string map = GetMap();
            string[] lines = map.Split('\r');

            sb.Append("  ");

            for (int y = 0; y < lines[0].Length; ++y)
            {
                sb.Append(" ");
                sb.Append(y.ToString("D2"));
            }

            sb.AppendLine();

            for (int x = 0; x < lines.Length; ++x)
            {
                sb.Append(x.ToString("D2"));
                sb.Append(" ");

                foreach (char tile in lines[x])
                {
                    sb.Append(tile.ToString().PadLeft(2));
                    sb.Append(" ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private short ParseTileChar(char height)
        {
            if (height == 'x')
            {
                return -1;
            }

            short index = (short)Array.IndexOf(base32, height);

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            return index;
        }

        private void SetTileHeight(int x, int y, short height)
        {
            Map[x * TotalY + y] = height;
        }

        #endregion Methods
    }
}