using NUnit.Framework;
using System;
using Yupi.Controller;
using System.Numerics;

namespace Yupi.Tests
{
	[TestFixture ()]
	public class AStarTest
	{
		[Test ()]
		public void OneTile ()
		{
			AStar astar = new AStar ();
			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (1, 1));

			Assert.That (nodes.Count == 2);
			Assert.AreEqual (new Vector2 (1, 1), nodes [0]);
			Assert.AreEqual (new Vector2 (0, 0), nodes [1]);
		}

		[Test ()]
		public void MultipleTiles() {
			AStar astar = new AStar ();
			var nodes = astar.Find (new Vector2 (0, 0), new Vector2 (0, 2));
			Assert.That (nodes.Count == 3);
			Assert.AreEqual (new Vector2 (0, 2), nodes [0]);
			Assert.AreEqual (new Vector2 (0, 1), nodes [1]);
			Assert.AreEqual (new Vector2 (0, 0), nodes [2]);
		}
	}
}

