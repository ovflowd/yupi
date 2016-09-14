using System;
using Yupi.Model.Domain;

namespace Yupi.Tests
{
    [TestFixture()]
    public class HeightmapTest
    {
        [Test()]
        public void ModelString()
        {
            var heightmap = RoomModel.Model_a.Heightmap;
            var map = new HeightMap(heightmap);

            Console.WriteLine(map);

            Assert.AreEqual(heightmap, map.GetMap());
        }

        [Test()]
        public void Walkable()
        {
            var heightmap = RoomModel.Model_a.Heightmap;
            var map = new HeightMap(heightmap);

            var lines = heightmap.Split('\r');

            for (var y = 0; y < lines.Length; ++y)
                for (var x = 0; x < lines[y].Length; ++x)
                    Assert.AreEqual(lines[y][x] != 'x', map.IsWalkable(new System.Numerics.Vector2(x, y)));
        }
    }
}