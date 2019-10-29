using System;
using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerPassed : IDomainEvent
    {
        public PlayerPassed(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId;
    }

    public class PlayerWasNominatedMostValuablePlayer : IDomainEvent
    {
        public PlayerWasNominatedMostValuablePlayer(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId;
    }

    public class PlayerMadeTouchdown : IDomainEvent
    {
        public PlayerMadeTouchdown(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId;
    }

    public class PlayerMadeCasualty : IDomainEvent
    {
        public PlayerMadeCasualty(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId;
    }
}