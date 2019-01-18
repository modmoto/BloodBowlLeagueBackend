using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerPassed : IDomainEvent
    {
        public PlayerPassed(long newStarPlayerPoints, Identity entityId)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }

    public class PlayerWasNominatedMostValuablePlayer : IDomainEvent
    {
        public PlayerWasNominatedMostValuablePlayer(long newStarPlayerPoints, Identity entityId)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }

    public class PlayerMadeTouchdown : IDomainEvent
    {
        public PlayerMadeTouchdown(long newStarPlayerPoints, Identity entityId)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }

    public class PlayerMadeCasualty : IDomainEvent
    {
        public PlayerMadeCasualty(long newStarPlayerPoints, Identity entityId)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }
}