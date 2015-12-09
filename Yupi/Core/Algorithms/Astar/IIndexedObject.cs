namespace Yupi.Core.Algorithms.Astar
{
    public interface IWeightAlterable<T>
    {
        T Weight { get; set; }
    }
}