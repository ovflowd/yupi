using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
    public class GameCenterLoadGameMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<GameCenterGameAchievementsMessageComposer>().Compose(session);
            router.GetComposer<GameCenterLeaderboardMessageComposer>().Compose(session, session.Info);
            router.GetComposer<GameCenterLeaderboard2MessageComposer>().Compose(session, session.Info);
            router.GetComposer<GameCenterGamesLeftMessageComposer>().Compose(session);
            router.GetComposer<GameCenterPreviousWinnerMessageComposer>().Compose(session);
            router.GetComposer<GameCenterAllAchievementsMessageComposer>().Compose(session);
            router.GetComposer<GameCenterEnterInGameMessageComposer>().Compose(session);
        }
    }
}