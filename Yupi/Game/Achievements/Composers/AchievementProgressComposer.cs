using Yupi.Game.Achievements.Structs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Achievements.Composers
{
    /// <summary>
    ///     Class AchievementProgressComposer.
    /// </summary>
    internal class AchievementProgressComposer
    {
        /// <summary>
        ///     Composes the specified achievement.
        /// </summary>
        /// <param name="achievement">The achievement.</param>
        /// <param name="targetLevel">The target level.</param>
        /// <param name="targetLevelData">The target level data.</param>
        /// <param name="totalLevels">The total levels.</param>
        /// <param name="userData">The user data.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(Achievement achievement, int targetLevel, AchievementLevel targetLevelData, int totalLevels, UserAchievement userData)
        {
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("AchievementProgressMessageComposer"));

            serverMessage.AppendInteger(achievement.Id);
            serverMessage.AppendInteger(targetLevel);
            serverMessage.AppendString($"{achievement.GroupName}{targetLevel}");
            serverMessage.AppendInteger(targetLevelData.Requirement);
            serverMessage.AppendInteger(targetLevelData.Requirement);
            serverMessage.AppendInteger(targetLevelData.RewardPixels);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(userData.Progress);
            serverMessage.AppendBool(userData.Level >= totalLevels);
            serverMessage.AppendString(achievement.Category);
            serverMessage.AppendString(string.Empty);
            serverMessage.AppendInteger(totalLevels);
            serverMessage.AppendInteger(0);

            return serverMessage;
        }
    }
}