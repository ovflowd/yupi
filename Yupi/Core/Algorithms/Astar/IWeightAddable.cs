namespace Yupi.Core.Algorithms.Astar
{
    public interface IWeightAddable<T>
    {
        T WeightChange { get; set; }
    }
}