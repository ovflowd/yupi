namespace Yupi.Model.Domain.Users
{
    /// <summary>
    ///     Class Relationship.
    /// </summary>
    public class Relationship
    {
		public virtual int Id { get; set; }
		// TODO Introduce ENUM
		public virtual int Type  { get; set; }
		public virtual Habbo Friend { get; set; }
    }
}