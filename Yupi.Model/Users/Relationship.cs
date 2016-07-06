using FluentNHibernate.Data;

namespace Yupi.Model.Users
{
    /// <summary>
    ///     Class Relationship.
    /// </summary>
    public class Relationship : Entity
    {
		// TODO Introduce ENUM
		public virtual int Type  { get; set; }
		public virtual Habbo Friend { get; set; }
    }
}