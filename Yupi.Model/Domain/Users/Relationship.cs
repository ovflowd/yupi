namespace Yupi.Model.Domain
{
    /// <summary>
    ///     Class Relationship.
    /// </summary>
    public class Relationship
    {
        #region Properties

        // TODO Should not be required...
        public virtual bool Deleted
        {
            get; set;
        }

        public virtual UserInfo Friend
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        // TODO Introduce ENUM
        public virtual int Type
        {
            get; set;
        }

        #endregion Properties
    }
}