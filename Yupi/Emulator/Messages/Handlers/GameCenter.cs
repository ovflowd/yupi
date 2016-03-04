using System;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
    internal partial class MessageHandler
    {
        /// <summary>
        ///     Games the center load game.
        /// </summary>
        internal void GameCenterLoadGame()
        {
            SimpleServerMessageBuffer achievements =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterGameAchievementsMessageComposer"));
            achievements.AppendInteger(18);
            achievements.AppendInteger(1); //count
            achievements.AppendInteger(295); //id
            achievements.AppendInteger(1);
            achievements.AppendString("ACH_StoryChallengeChampion1");
            achievements.AppendInteger(0);
            achievements.AppendInteger(1);
            achievements.AppendInteger(0);
            achievements.AppendInteger(0);
            achievements.AppendInteger(0);
            achievements.AppendBool(false);
            achievements.AppendString("games");
            achievements.AppendString("elisa_habbo_stories");
            achievements.AppendInteger(1);
            achievements.AppendInteger(0);
            achievements.AppendString("");
            Session.SendMessage(achievements);

            SimpleServerMessageBuffer weeklyLeaderboard =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterLeaderboardMessageComposer"));
            weeklyLeaderboard.AppendInteger(2014);
            weeklyLeaderboard.AppendInteger(49);
            weeklyLeaderboard.AppendInteger(0);
            weeklyLeaderboard.AppendInteger(0);
            weeklyLeaderboard.AppendInteger(6526);
            weeklyLeaderboard.AppendInteger(1);
            weeklyLeaderboard.AppendInteger(Session.GetHabbo().Id);
            weeklyLeaderboard.AppendInteger(0);
            weeklyLeaderboard.AppendInteger(1);
            weeklyLeaderboard.AppendString(Session.GetHabbo().UserName);
            weeklyLeaderboard.AppendString(Session.GetHabbo().Look);
            weeklyLeaderboard.AppendString(Session.GetHabbo().Gender);
            weeklyLeaderboard.AppendInteger(1);
            weeklyLeaderboard.AppendInteger(18);
            Session.SendMessage(weeklyLeaderboard);

            SimpleServerMessageBuffer weeklyLeaderboard2 =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterLeaderboard2MessageComposer"));
            weeklyLeaderboard2.AppendInteger(2014);
            weeklyLeaderboard2.AppendInteger(49);
            weeklyLeaderboard2.AppendInteger(0);
            weeklyLeaderboard2.AppendInteger(0);
            weeklyLeaderboard2.AppendInteger(6526);
            weeklyLeaderboard2.AppendInteger(1);
            weeklyLeaderboard2.AppendInteger(Session.GetHabbo().Id);
            weeklyLeaderboard2.AppendInteger(0);
            weeklyLeaderboard2.AppendInteger(1);
            weeklyLeaderboard2.AppendString(Session.GetHabbo().UserName);
            weeklyLeaderboard2.AppendString(Session.GetHabbo().Look);
            weeklyLeaderboard2.AppendString(Session.GetHabbo().Gender);
            weeklyLeaderboard2.AppendInteger(0);
            weeklyLeaderboard2.AppendInteger(18);
            Session.SendMessage(weeklyLeaderboard2);

            SimpleServerMessageBuffer weeklyLeaderboard3 =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterLeaderboard2MessageComposer"));
            weeklyLeaderboard3.AppendInteger(2014);
            weeklyLeaderboard3.AppendInteger(48);
            weeklyLeaderboard3.AppendInteger(0);
            weeklyLeaderboard3.AppendInteger(1);
            weeklyLeaderboard3.AppendInteger(6526);
            weeklyLeaderboard3.AppendInteger(1);
            weeklyLeaderboard3.AppendInteger(Session.GetHabbo().Id);
            weeklyLeaderboard3.AppendInteger(0);
            weeklyLeaderboard3.AppendInteger(1);
            weeklyLeaderboard3.AppendString(Session.GetHabbo().UserName);
            weeklyLeaderboard3.AppendString(Session.GetHabbo().Look);
            weeklyLeaderboard3.AppendString(Session.GetHabbo().Gender);
            weeklyLeaderboard3.AppendInteger(0);
            weeklyLeaderboard3.AppendInteger(18);
            Session.SendMessage(weeklyLeaderboard3);

            SimpleServerMessageBuffer gamesLeft = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterGamesLeftMessageComposer"));
            gamesLeft.AppendInteger(18);
            gamesLeft.AppendInteger(-1);
            gamesLeft.AppendInteger(0);
            Session.SendMessage(gamesLeft);

            SimpleServerMessageBuffer previousWinner =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterPreviousWinnerMessageComposer"));
            previousWinner.AppendInteger(18);
            previousWinner.AppendInteger(0);

            previousWinner.AppendString("name");
            previousWinner.AppendString("figure");
            previousWinner.AppendString("gender");
            previousWinner.AppendInteger(0);
            previousWinner.AppendInteger(0);

            Session.SendMessage(previousWinner);

            SimpleServerMessageBuffer allAchievements =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterAllAchievementsMessageComposer"));
            allAchievements.AppendInteger(0); //count

            allAchievements.AppendInteger(0); //gameId
            allAchievements.AppendInteger(0); //count
            allAchievements.AppendInteger(0); //achId
            allAchievements.AppendString("SnowWarTotalScore"); //achName
            allAchievements.AppendInteger(0); //levels

            Session.SendMessage(allAchievements);

            SimpleServerMessageBuffer enterInGame = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterEnterInGameMessageComposer"));
            enterInGame.AppendInteger(18);
            enterInGame.AppendInteger(0);
            Session.SendMessage(enterInGame);
        }

        /// <summary>
        ///     Games the center join queue.
        /// </summary>
        internal void GameCenterJoinQueue()
        {
            SimpleServerMessageBuffer joinQueue = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterJoinGameQueueMessageComposer"));
            joinQueue.AppendInteger(18);
            Session.SendMessage(joinQueue);

            SimpleServerMessageBuffer loadGame = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("GameCenterLoadGameUrlMessageComposer"));
            loadGame.AppendInteger(18);
            loadGame.AppendString(Convert.ToString(Yupi.GetUnixTimeStamp()));
            loadGame.AppendString(ServerExtraSettings.GameCenterStoriesUrl);
            Session.SendMessage(loadGame);
        }
    }
}