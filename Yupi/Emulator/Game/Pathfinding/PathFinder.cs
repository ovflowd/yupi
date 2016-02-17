/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Collections.Generic;
using Yupi.Game.Pathfinding.Vectors;
using Yupi.Game.Rooms.User;
using Yupi.Game.Rooms.User.Path;

namespace Yupi.Game.Pathfinding
{
    /// <summary>
    ///     Class PathFinder.
    /// </summary>
    internal class PathFinder
    {
        /// <summary>
        ///     The diag move points
        /// </summary>
        public static Vector2D[] DiagMovePoints =
        {
            new Vector2D(-1, -1),
            new Vector2D(0, -1),
            new Vector2D(1, -1),
            new Vector2D(1, 0),
            new Vector2D(1, 1),
            new Vector2D(0, 1),
            new Vector2D(-1, 1),
            new Vector2D(-1, 0)
        };

        /// <summary>
        ///     The no diag move points
        /// </summary>
        public static Vector2D[] NoDiagMovePoints =
        {
            new Vector2D(0, -1),
            new Vector2D(1, 0),
            new Vector2D(0, 1),
            new Vector2D(-1, 0)
        };

        /// <summary>
        ///     Finds the path.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="diag">if set to <c>true</c> [diag].</param>
        /// <param name="map">The map.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>List&lt;Vector2D&gt;.</returns>
        public static List<Vector2D> FindPath(RoomUser user, bool diag, Gamemap map, Vector2D start, Vector2D end)
        {
            List<Vector2D> list = new List<Vector2D>();

            PathFinderNode pathFinderNode = FindPathReversed(user, diag, map, start, end);

            if (pathFinderNode != null)
            {
                list.Add(end);

                while (pathFinderNode.Next != null)
                {
                    list.Add(pathFinderNode.Next.Position);
                    pathFinderNode = pathFinderNode.Next;
                }
            }

            return list;
        }

        /// <summary>
        ///     Finds the path reversed.
        /// </summary>
        /// <param name="roomUserable">The user.</param>
        /// <param name="whatIsDiag">if set to <c>true</c> [diag].</param>
        /// <param name="gameLocalMap">The map.</param>
        /// <param name="startMap">The start.</param>
        /// <param name="endMap">The end.</param>
        /// <returns>PathFinderNode.</returns>
        public static PathFinderNode FindPathReversed(RoomUser roomUserable, bool whatIsDiag, Gamemap gameLocalMap,
            Vector2D startMap, Vector2D endMap)
        {
            MinHeap<PathFinderNode> minSpanTreeCost = new MinHeap<PathFinderNode>(256);
            PathFinderNode[,] pathFinderMap = new PathFinderNode[gameLocalMap.Model.MapSizeX, gameLocalMap.Model.MapSizeY];
            PathFinderNode pathFinderStart = new PathFinderNode(startMap) {Cost = 0};
            PathFinderNode pathFinderEnd = new PathFinderNode(endMap);

            pathFinderMap[pathFinderStart.Position.X, pathFinderStart.Position.Y] = pathFinderStart;
            minSpanTreeCost.Add(pathFinderStart);

            while (minSpanTreeCost.Count > 0)
            {
                pathFinderStart = minSpanTreeCost.ExtractFirst();
                pathFinderStart.InClosed = true;

                for (int index = 0;
                    (whatIsDiag ? (index < DiagMovePoints.Length ? 1 : 0) : (index < NoDiagMovePoints.Length ? 1 : 0)) !=
                    0;
                    index++)
                {
                    Vector2D realEndPosition = pathFinderStart.Position +
                                          (whatIsDiag ? DiagMovePoints[index] : NoDiagMovePoints[index]);

                    bool isEndOfPath = (realEndPosition.X == endMap.X) && (realEndPosition.Y == endMap.Y);

                    if (gameLocalMap.IsValidStep(roomUserable,
                        new Vector2D(pathFinderStart.Position.X, pathFinderStart.Position.Y), realEndPosition,
                        isEndOfPath, roomUserable.AllowOverride))
                    {
                        PathFinderNode pathFinderSecondNodeCalculation;

                        if (pathFinderMap[realEndPosition.X, realEndPosition.Y] == null)
                        {
                            pathFinderSecondNodeCalculation = new PathFinderNode(realEndPosition);
                            pathFinderMap[realEndPosition.X, realEndPosition.Y] = pathFinderSecondNodeCalculation;
                        }
                        else
                            pathFinderSecondNodeCalculation = pathFinderMap[realEndPosition.X, realEndPosition.Y];

                        if (!pathFinderSecondNodeCalculation.InClosed)
                        {
                            int internalSpanTreeCost = 0;

                            if (pathFinderStart.Position.X != pathFinderSecondNodeCalculation.Position.X)
                                internalSpanTreeCost++;

                            if (pathFinderStart.Position.Y != pathFinderSecondNodeCalculation.Position.Y)
                                internalSpanTreeCost++;

                            int loopTotalCost = pathFinderStart.Cost + internalSpanTreeCost +
                                                pathFinderSecondNodeCalculation.Position.GetDistanceSquared(endMap);

                            if (loopTotalCost < pathFinderSecondNodeCalculation.Cost)
                            {
                                pathFinderSecondNodeCalculation.Cost = loopTotalCost;
                                pathFinderSecondNodeCalculation.Next = pathFinderStart;
                            }

                            if (!pathFinderSecondNodeCalculation.InOpen)
                            {
                                if (pathFinderSecondNodeCalculation.Equals(pathFinderEnd))
                                {
                                    pathFinderSecondNodeCalculation.Next = pathFinderStart;

                                    return pathFinderSecondNodeCalculation;
                                }

                                pathFinderSecondNodeCalculation.InOpen = true;

                                minSpanTreeCost.Add(pathFinderSecondNodeCalculation);
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Calculates the rotation.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>System.Int32.</returns>
        internal static int CalculateRotation(int x1, int y1, int x2, int y2)
        {
            int dX = x2 - x1, dY = y2 - y1;

            double d = Math.Atan2(dY, dX)*180/Math.PI;

            return ((int) d + 90)/45;
        }

        /// <summary>
        ///     Gets the distance.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="toX">To x.</param>
        /// <param name="toY">To y.</param>
        /// <returns>System.Int32.</returns>
        public static int GetDistance(int x, int y, int toX, int toY)
            => Convert.ToInt32(Math.Sqrt(Math.Pow(toX - x, 2) + Math.Pow(toY - y, 2)));
    }
}