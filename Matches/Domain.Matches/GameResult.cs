using Microwave.Domain.Identities;

namespace Domain.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; }
        public PointsOfTeam Winner { get; }
        public PointsOfTeam Looser { get; }
        public long HomeTouchDowns { get; }
        public long GuestTouchDowns { get; }

        private GameResult(
            bool isDraw,
            PointsOfTeam winner,
            PointsOfTeam looser,
            long homeTouchDowns,
            long guestTouchDowns)
        {
            IsDraw = isDraw;
            Winner = winner;
            Looser = looser;
            HomeTouchDowns = homeTouchDowns;
            GuestTouchDowns = guestTouchDowns;
        }

        private static GameResult WinResultHome(PointsOfTeam home, PointsOfTeam guest)
        {
            return new GameResult(false, home, guest, home.TouchDowns, guest.TouchDowns);
        }

        private static GameResult WinResultGuest(PointsOfTeam home, PointsOfTeam guest)
        {
            return new GameResult(false, guest, home, home.TouchDowns, guest.TouchDowns);
        }

        public static GameResult CreatGameResult(PointsOfTeam homeTouchDowns, PointsOfTeam guestTouchDowns)
        {
            GameResult gameResult;
            if (homeTouchDowns == guestTouchDowns) gameResult = Draw(homeTouchDowns, guestTouchDowns);
            else
            {
                gameResult = homeTouchDowns.TouchDowns > guestTouchDowns.TouchDowns
                    ? WinResultHome(homeTouchDowns, guestTouchDowns)
                    : WinResultGuest(homeTouchDowns, guestTouchDowns);
            }

            return gameResult;
        }

        private static GameResult Draw(PointsOfTeam homeTouchDowns, PointsOfTeam guestTouchDowns)
        {
            return new GameResult(true, null, null, homeTouchDowns.TouchDowns, guestTouchDowns.TouchDowns);
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