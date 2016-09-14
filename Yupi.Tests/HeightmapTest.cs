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