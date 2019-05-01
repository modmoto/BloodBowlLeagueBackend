using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerPassed : IDomainEvent
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

    public class PlayerWasNominatedMostValuablePlayer : IDomainEvent
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

    public class PlayerMadeTouchdown : IDomainEvent
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

    public class PlayerMadeCasualty : IDomainEvent
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