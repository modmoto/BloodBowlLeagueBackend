using System;
using System.Collections.Generic;
using Microwave.Domain.EventSourcing;

namespace Domain.Players.Events.Players
{
    public class PlayerLevelUpPossibilitiesChosen : IDomainEvent
    {
        public PlayerLevelUpPossibilitiesChosen(
            Guid playerId,
        FreeSkillPoint newFreeSkillPoint)
        {
            NewFreeSkillPoint = newFreeSkillPoint;
            PlayerId = playerId;
        }

        public string EntityId => PlayerId.ToString();
        public FreeSkillPoint NewFreeSkillPoint { get; }
        public Guid PlayerId { get; }
    }
}