namespace Yupi.Core.Algorithms.Astar
{
    public interface IPathNode
    {
        bool IsBlocked(int x, int y, bool lastTile);
    }
}