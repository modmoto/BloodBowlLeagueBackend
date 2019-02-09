using Microwave.Domain;

namespace Teams.ReadHost.Teams.PlayerReadModels.Events
{
    public class PlayerPassed : IDomainEvent
    {
        public PlayerPassed(Identity entityId, long newStarPlayerPoints)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }

    public class PlayerWasNominatedMostValuablePlayer : IDomainEvent
    {
        public PlayerWasNominatedMostValuablePlayer(Identity entityId, long newStarPlayerPoints)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }

    public class PlayerMadeTouchdown : IDomainEvent
    {
        public PlayerMadeTouchdown(Identity entityId, long newStarPlayerPoints)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }

    public class PlayerMadeCasualty : IDomainEvent
    {
        public PlayerMadeCasualty(Identity entityId, long newStarPlayerPoints)
        {
            NewStarPlayerPoints = newStarPlayerPoints;
            EntityId = entityId;
        }

        public long NewStarPlayerPoints { get; }
        public Identity EntityId { get; }
    }
}