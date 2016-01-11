using System;
using Yupi.Core.Settings;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary>
        ///     Games the center load game.
        /// </summary>
        internal void GameCenterLoadGame()
        {
            ServerMessage achievements =
                new ServerMessage(LibraryParser.OutgoingRequest("GameCenterGameAchievementsMessageComposer"));
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

            ServerMessage weeklyLeaderboard =
                new ServerMessage(LibraryParser.OutgoingRequest("GameCenterLeaderboardMessageComposer"));
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

            ServerMessage weeklyLeaderboard2 =
                new ServerMessage(LibraryParser.OutgoingRequest("GameCenterLeaderboard2MessageComposer"));
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

            ServerMessage weeklyLeaderboard3 =
                new ServerMessage(LibraryParser.OutgoingRequest("GameCenterLeaderboard2MessageComposer"));
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

            ServerMessage gamesLeft = new ServerMessage(LibraryParser.OutgoingRequest("GameCenterGamesLeftMessageComposer"));
            gamesLeft.AppendInteger(18);
            gamesLeft.AppendInteger(-1);
            gamesLeft.AppendInteger(0);
            Session.SendMessage(gamesLeft);

            ServerMessage previousWinner =
                new ServerMessage(LibraryParser.OutgoingRequest("GameCenterPreviousWinnerMessageComposer"));
            previousWinner.AppendInteger(18);
            previousWinner.AppendInteger(0);

            previousWinner.AppendString("name");
            previousWinner.AppendString("figure");
            previousWinner.AppendString("gender");
            previousWinner.AppendInteger(0);
            previousWinner.AppendInteger(0);

            Session.SendMessage(previousWinner);

            /*ServerMessage Products = new ServerMessage(LibraryParser.OutgoingRequest("GameCenterProductsMessageComposer"));
            Products.AppendInteger(18);//gameId
            Products.AppendInteger(0);//count
            Products.AppendInteger(6526);
            Products.AppendBool(false);
            Session.SendMessage(Products);*/

            ServerMessage allAchievements =
                new ServerMessage(LibraryParser.OutgoingRequest("GameCenterAllAchievementsMessageComposer"));
            allAchievements.AppendInteger(0); //count

            //For Stories
            /*PacketName5.AppendInteger(18);
            PacketName5.AppendInteger(1);
            PacketName5.AppendInteger(191);
            PacketName5.AppendString("StoryChallengeChampion");
            PacketName5.AppendInteger(20);*/

            allAchievements.AppendInteger(0); //gameId
            allAchievements.AppendInteger(0); //count
            allAchievements.AppendInteger(0); //achId
            allAchievements.AppendString("SnowWarTotalScore"); //achName
            allAchievements.AppendInteger(0); //levels

            Session.SendMessage(allAchievements);

            ServerMessage enterInGame = new ServerMessage(LibraryParser.OutgoingRequest("GameCenterEnterInGameMessageComposer"));
            enterInGame.AppendInteger(18);
            enterInGame.AppendInteger(0);
            Session.SendMessage(enterInGame);
        }

        /// <summary>
        ///     Games the center join queue.
        /// </summary>
        internal void GameCenterJoinQueue()
        {
            ServerMessage joinQueue = new ServerMessage(LibraryParser.OutgoingRequest("GameCenterJoinGameQueueMessageComposer"));
            joinQueue.AppendInteger(18);
            Session.SendMessage(joinQueue);

            ServerMessage loadGame = new ServerMessage(LibraryParser.OutgoingRequest("GameCenterLoadGameUrlMessageComposer"));
            loadGame.AppendInteger(18);
            loadGame.AppendString(Convert.ToString(Yupi.GetUnixTimeStamp()));
            loadGame.AppendString(ServerExtraSettings.GameCenterStoriesUrl);
            Session.SendMessage(loadGame);
        }
    }
}