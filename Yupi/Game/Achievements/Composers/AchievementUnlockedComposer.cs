using Yupi.Game.Achievements.Structs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Achievements.Composers
{
    /// <summary>
    ///     Class AchievementUnlockedComposer.
    /// </summary>
    internal class AchievementUnlockedComposer
    {
        /// <summary>
        ///     Composes the specified achievement.
        /// </summary>
        /// <param name="achievement">The achievement.</param>
        /// <param name="level">The level.</param>
        /// <param name="pointReward">The point reward.</param>
        /// <param name="pixelReward">The pixel reward.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(Achievement achievement, int level, int pointReward, int pixelReward)
        {
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("UnlockAchievementMessageComposer"));

            serverMessage.AppendInteger(achievement.Id);
            serverMessage.AppendInteger(level);
            serverMessage.AppendInteger(144);
            serverMessage.AppendString($"{achievement.GroupName}{level}");
            serverMessage.AppendInteger(pointReward);
            serverMessage.AppendInteger(pixelReward);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(10);
            serverMessage.AppendInteger(21);
            serverMessage.AppendString(level > 1 ? $"{achievement.GroupName}{(level - 1)}" : string.Empty);
            serverMessage.AppendString(achievement.Category);
            serverMessage.AppendBool(true);

            return serverMessage;
        }
    }
}