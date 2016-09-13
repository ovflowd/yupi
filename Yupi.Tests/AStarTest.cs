using NUnit.Framework;
using System;
using Yupi.Controller;
using System.Numerics;

namespace Yupi.Tests
{
	[TestFixture ()]
	public class AStarTest
	{
		private Vector2[] getNeighbours (Vector2 tile)
		{
			return new Vector2[] {
				new Vector2 (tile.X - 1, tile.Y),
				new Vector2 (tile.X - 1, tile.Y - 1),
				new Vector2 (tile.X - 1, tile.Y + 1),
				new Vector2 (tile.X + 1, tile.Y),
				new Vector2 (tile.X + 1, tile.Y - 1),
				new Vector2 (tile.X + 1, tile.Y + 1),
				new Vector2 (tile.X, tile.Y + 1),
				new Vector2 (tile.X, tile.Y - 1),
			};
		}

		private float distance (Vector2 start, Vector2 target)
		{
			return (target - start).Length ();
		}

		[Test ()]
		public void OneTile ()
		{
			AStar<Vector2> astar = new AStar<Vector2> (x => true, getNeighbours, distance);
			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (1, 1));

			Assert.That (nodes.Count == 2);
			Assert.AreEqual (new Vector2 (1, 1), nodes [0]);
			Assert.AreEqual (new Vector2 (0, 0), nodes [1]);
		}

		[Test ()]
		public void MultipleTiles() {
			AStar<Vector2> astar = new AStar<Vector2> (x => true, getNeighbours, distance);
			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (0, 2));
			Assert.That (nodes.Count == 3);
			Assert.AreEqual (new Vector2 (0, 2), nodes [0]);
			Assert.AreEqual (new Vector2 (0, 1), nodes [1]);
			Assert.AreEqual (new Vector2 (0, 0), nodes [2]);
		}

		[Test ()]
		public void BlockingTile() {
			AStar<Vector2> astar = new AStar<Vector2> (x => {
				return x != new Vector2(0, 1);
			}, getNeighbours, distance);
			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (0, 2));
			Assert.That (nodes.Count == 3);
			Assert.AreEqual (new Vector2 (0, 2), nodes [0]);
			Assert.AreEqual (new Vector2 (-1, 1), nodes [1]);
			Assert.AreEqual (new Vector2 (0, 0), nodes [2]);
		}

		[Test ()]
		public void BlockingTarget() {
			AStar<Vector2> astar = new AStar<Vector2> (x => {
				return x != new Vector2(0, 2);
			}, getNeighbours, distance);
			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (0, 2));
			Assert.IsNull (nodes);
		}

		[Test ()]
		public void BlockingStart() {
			AStar<Vector2> astar = new AStar<Vector2> (x => {
				return x != new Vector2(0, 0);
			}, getNeighbours, distance);
			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (0, 2));
			Assert.That (nodes.Count == 3);
			Assert.AreEqual (new Vector2 (0, 2), nodes [0]);
			Assert.AreEqual (new Vector2 (0, 1), nodes [1]);
			Assert.AreEqual (new Vector2 (0, 0), nodes [2]);
		}

		[Test ()]
		public void Reuse() {
			AStar<Vector2> astar = new AStar<Vector2> (x => {
				return x != new Vector2(0, 1);
			}, getNeighbours, distance);

			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (0, 2));

			Assert.That (nodes.Count == 3);
			Assert.AreEqual (new Vector2 (0, 2), nodes [0]);
			Assert.AreEqual (new Vector2 (-1, 1), nodes [1]);
			Assert.AreEqual (new Vector2 (0, 0), nodes [2]);

			nodes = astar.Find (new Vector2 (1, 0), new Vector2 (0, 2));

			Assert.That (nodes.Count == 3);
			Assert.AreEqual (new Vector2 (0, 2), nodes [0]);
			Assert.AreEqual (new Vector2 (1, 1), nodes [1]);
			Assert.AreEqual (new Vector2 (1, 0), nodes [2]);
		}
	}
}

