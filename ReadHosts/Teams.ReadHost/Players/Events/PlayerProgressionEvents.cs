using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerPassed : ISubscribedDomainEvent
    {
        public PlayerPassed(GuidIdentity playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public GuidIdentity PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public Identity EntityId => PlayerId;
    }

    public class PlayerWasNominatedMostValuablePlayer : ISubscribedDomainEvent
    {
        public PlayerWasNominatedMostValuablePlayer(GuidIdentity playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public GuidIdentity PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public Identity EntityId => PlayerId;
    }

    public class PlayerMadeTouchdown : ISubscribedDomainEvent
    {
        public PlayerMadeTouchdown(GuidIdentity playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public GuidIdentity PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public Identity EntityId => PlayerId;
    }

    public class PlayerMadeCasualty : ISubscribedDomainEvent
    {
        public PlayerMadeCasualty(GuidIdentity playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public GuidIdentity PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public Identity EntityId => PlayerId;
    }
}