namespace Yupi.Model.Domain
{
    /// <summary>
    ///     Class Relationship.
    /// </summary>
    public class Relationship
    {
        public virtual int Id { get; protected set; }
        // TODO Introduce ENUM
        public virtual int Type { get; set; }
        public virtual UserInfo Friend { get; set; }

        // TODO Should not be required...
        public virtual bool Deleted { get; set; }
    }
}