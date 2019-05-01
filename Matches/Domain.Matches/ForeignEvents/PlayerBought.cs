using Microwave.Domain;

namespace Domain.Matches.ForeignEvents
{
    public class PlayerBought
    {
        public PlayerBought(GuidIdentity matchId, GuidIdentity playerId)
        {
            MatchId = matchId;
            PlayerId = playerId;
        }

        public GuidIdentity MatchId { get; }
        public GuidIdentity PlayerId { get; }
    }
}