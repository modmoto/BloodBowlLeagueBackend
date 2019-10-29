using System;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerPassed : ISubscribedDomainEvent
    {
        public PlayerPassed(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId.ToString();
    }

    public class PlayerWasNominatedMostValuablePlayer : ISubscribedDomainEvent
    {
        public PlayerWasNominatedMostValuablePlayer(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId.ToString();
    }

    public class PlayerMadeTouchdown : ISubscribedDomainEvent
    {
        public PlayerMadeTouchdown(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId.ToString();
    }

    public class PlayerMadeCasualty : ISubscribedDomainEvent
    {
        public PlayerMadeCasualty(Guid playerId, long newStarPlayerPoints)
        {
            PlayerId = playerId;
            NewStarPlayerPoints = newStarPlayerPoints;
        }

        public Guid PlayerId { get; }
        public long NewStarPlayerPoints { get; }
        public string EntityId => PlayerId.ToString();
    }
}