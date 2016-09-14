// ---------------------------------------------------------------------------------
// <copyright file="AStarTest.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Numerics;

    using NUnit.Framework;

    using Yupi.Util;
    using Yupi.Util.Pathfinding;

    [TestFixture]
    public class AStarTest
    {
        #region Methods

        [Test]
        public void BlockingStart()
        {
            Pathfinder astar = new Pathfinder(x => { return x != new Vector2(0, 0); }, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));
            Assert.IsNull(nodes);
        }

        [Test]
        public void BlockingTarget()
        {
            Pathfinder astar = new Pathfinder(x => { return x != new Vector2(0, 2); }, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));
            Assert.IsNull(nodes);
        }

        [Test]
        public void BlockingTile()
        {
            Pathfinder astar = new Pathfinder(x => { return x != new Vector2(0, 1); }, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(0, 2)
            }));
        }

        [Test]
        public void MultipleTiles()
        {
            Pathfinder astar = new Pathfinder(x => true, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(0, 2)
            }));
        }

        [Test]
        public void OneTile()
        {
            Pathfinder astar = new Pathfinder(x => true, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(1, 1));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 1)
            }));
        }

        [Test]
        public void Reuse()
        {
            Pathfinder astar = new Pathfinder(x => { return x != new Vector2(0, 1); }, getNeighbours);

            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(0, 2)
            }));

            nodes = astar.Find(new Vector2(1, 0), new Vector2(0, 2));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 2)
            }));
        }

        private Vector2[] getNeighbours(Vector2 tile)
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

        #endregion Methods
    }
}