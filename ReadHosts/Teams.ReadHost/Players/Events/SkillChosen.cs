using System;
using System.Collections.Generic;
using Microwave.Queries;
using Teams.ReadHost.Races;

namespace Teams.ReadHost.Players.Events
{
    public class SkillChosen : ISubscribedDomainEvent
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