using Azure.HabboHotel.GameClients;

namespace Azure.HabboHotel.Commands.List
{
    /// <summary>
    /// Class Empty. This class cannot be inherited.
    /// </summary>
    internal sealed class Test : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        public Test()
        {
            MinRank = 8;
            Description = "Testing things.";
            Usage = ":test";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            // You can write here any test command.
            return true;
        }
    }
}