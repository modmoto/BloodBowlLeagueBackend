using System;
using System.Collections.Generic;
using Microwave.Domain;
using Microwave.Domain.EventSourcing;

namespace Domain.Players.Events.Players
{
    public class SkillChosen : IDomainEvent
    {
        public string EntityId => PlayerId.ToString();
        public Guid PlayerId { get; }
        public SkillReadModel NewSkill { get; }
        public IEnumerable<FreeSkillPoint> NewFreeSkillPoints { get; }

        public SkillChosen(
            Guid playerId,
            SkillReadModel newSkill,
            IEnumerable<FreeSkillPoint> newFreeSkillPoints)
        {
            PlayerId = playerId;
            NewSkill = newSkill;
            NewFreeSkillPoints = newFreeSkillPoints;
        }
    }
}