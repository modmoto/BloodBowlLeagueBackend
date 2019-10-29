using System;
using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class SkillChosen : IDomainEvent
    {
        public string EntityId => PlayerId;
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