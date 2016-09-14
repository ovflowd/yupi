// ---------------------------------------------------------------------------------
// <copyright file="HeightmapTest.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Tests
{
    using System;

    using NUnit.Framework;

    using Yupi.Model.Domain;

    [TestFixture]
    public class HeightmapTest
    {
        #region Methods

        [Test]
        public void ModelString()
        {
            string heightmap = RoomModel.Model_a.Heightmap;
            HeightMap map = new HeightMap(heightmap);

            Console.WriteLine(map);

            Assert.AreEqual(heightmap, map.GetMap());
        }

        [Test]
        public void Walkable()
        {
            string heightmap = RoomModel.Model_a.Heightmap;
            HeightMap map = new HeightMap(heightmap);

            string[] lines = heightmap.Split('\r');

            for (int y = 0; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    Assert.AreEqual(lines[y][x] != 'x', map.IsWalkable(new System.Numerics.Vector2(x, y)));
                }
            }
        }

        #endregion Methods
    }
}