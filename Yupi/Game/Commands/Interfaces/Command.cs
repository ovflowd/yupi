using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Interfaces
{
    /// <summary>
    ///     Class Command.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        ///     Gets or sets the minimum rank.
        /// </summary>
        /// <value>The minimum rank.</value>
        public virtual short MinRank { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        ///     Gets or sets the usage.
        /// </summary>
        /// <value>The usage.</value>
        public virtual string Usage { get; set; }

        /// <summary>
        ///     Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public virtual string Alias { get; set; }

        /// <summary>
        ///     Gets or sets the minimum parameters.
        /// </summary>
        /// <value>The minimum parameters.</value>
        public virtual short MinParams { get; set; }

        /// <summary>
        ///     Executes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="pms">The PMS.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool Execute(GameClient client, string[] pms);
    }
}