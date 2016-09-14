using Yupi.Util.Pathfinding;

namespace Yupi.Tests
{
    [TestFixture()]
    public class AStarTest
    {
        private Vector2[] getNeighbours(Vector2 tile)
        {
            return new Vector2[]
            {
                new Vector2(tile.X - 1, tile.Y),
                new Vector2(tile.X - 1, tile.Y - 1),
                new Vector2(tile.X - 1, tile.Y + 1),
                new Vector2(tile.X + 1, tile.Y),
                new Vector2(tile.X + 1, tile.Y - 1),
                new Vector2(tile.X + 1, tile.Y + 1),
                new Vector2(tile.X, tile.Y + 1),
                new Vector2(tile.X, tile.Y - 1)
            };
        }

        [Test()]
        public void OneTile()
        {
            var astar = new Pathfinder(x => true, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(1, 1));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 1)
            }));
        }

        [Test()]
        public void MultipleTiles()
        {
            var astar = new Pathfinder(x => true, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(0, 2)
            }));
        }

        [Test()]
        public void BlockingTile()
        {
            var astar = new Pathfinder(x => { return x != new Vector2(0, 1); }, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(-1, 1),
                new Vector2(0, 2)
            }));
        }

        [Test()]
        public void BlockingTarget()
        {
            var astar = new Pathfinder(x => { return x != new Vector2(0, 2); }, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));
            Assert.IsNull(nodes);
        }

        [Test()]
        public void BlockingStart()
        {
            var astar = new Pathfinder(x => { return x != new Vector2(0, 0); }, getNeighbours);
            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));
            Assert.IsNull(nodes);
        }

        [Test()]
        public void Reuse()
        {
            var astar = new Pathfinder(x => { return x != new Vector2(0, 1); }, getNeighbours);

            var nodes = astar.Find(new Vector2(0, 0), new Vector2(0, 2));

            Assert.That(nodes, Is.EquivalentTo(new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(-1, 1),
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
    }
}