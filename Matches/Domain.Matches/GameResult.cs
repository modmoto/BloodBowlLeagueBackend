using Microwave.Domain.Identities;

namespace Domain.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; }
        public PointsOfTeam HomeTeam { get; }
        public PointsOfTeam GuestTeam { get; }
        public Identity Winner => GetTeamWithMoreTouchDowns();
        public Identity Looser => GetTeamWithLessTouchDowns();

        private Identity GetTeamWithMoreTouchDowns()
        {
            if (IsDraw) return null;
            return HomeTeam.TouchDowns > GuestTeam.TouchDowns ? HomeTeam.TeamId : GuestTeam.TeamId;
        }

        private Identity GetTeamWithLessTouchDowns()
        {
            if (IsDraw) return null;
            return HomeTeam.TouchDowns < GuestTeam.TouchDowns ? HomeTeam.TeamId : GuestTeam.TeamId;
        }

        private GameResult(
            bool isDraw,
            PointsOfTeam homeTeam,
            PointsOfTeam guestTeam)
        {
            IsDraw = isDraw;
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

        public static GameResult CreatGameResult(PointsOfTeam homeTouchDowns, PointsOfTeam guestTouchDowns)
        {
            var gameResult = homeTouchDowns == guestTouchDowns
                ? Draw(homeTouchDowns, guestTouchDowns)
                : WinResult(homeTouchDowns, guestTouchDowns);

            return gameResult;
        }

        private static GameResult Draw(PointsOfTeam homeTouchDowns, PointsOfTeam guestTouchDowns)
        {
            return new GameResult(true, homeTouchDowns, guestTouchDowns);
        }

        private static GameResult WinResult(PointsOfTeam home, PointsOfTeam guest)
        {
            return new GameResult(false, home, guest);
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