using Microwave.Domain;

namespace Domain.Matches.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; }
        public PointsOfTeam Team { get; }
        public PointsOfTeam Looser { get; }

        private GameResult(bool isDraw, PointsOfTeam team, PointsOfTeam looser)
        {
            IsDraw = isDraw;
            Team = team;
            Looser = looser;
        }

        private static GameResult Draw()
        {
            return new GameResult(true, null, null);
        }

        private static GameResult WinResult(PointsOfTeam team, PointsOfTeam looser)
        {
            return new GameResult(false, team, looser);
        }

        public static GameResult CreatGameResult(PointsOfTeam homeTouchDowns, PointsOfTeam guestTouchDowns)
        {
            GameResult gameResult;
            if (homeTouchDowns == guestTouchDowns) gameResult = Draw();
            else
            {
                gameResult = homeTouchDowns.TouchDowns > guestTouchDowns.TouchDowns
                    ? WinResult(homeTouchDowns, guestTouchDowns)
                    : WinResult(guestTouchDowns, homeTouchDowns);
            }

            return gameResult;
        }
    }

    public class PointsOfTeam
    {
        public Identity TeamId { get; }
        public long TouchDowns { get; }

        public PointsOfTeam(Identity teamId, long touchDowns)
        {
            TeamId = teamId;
            TouchDowns = touchDowns;
        }
    }
}