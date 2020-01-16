using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Players.Events.Players
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(
            Guid playerId,
            int newLevel)
        {
            PlayerId = playerId;
            NewLevel = newLevel;
        }

        public string EntityId => PlayerId.ToString();
        public Guid PlayerId { get; }
        public int NewLevel { get; }
    }
}