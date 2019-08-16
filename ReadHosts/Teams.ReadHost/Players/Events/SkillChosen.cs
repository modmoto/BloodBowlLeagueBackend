using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Teams.ReadHost.Races;

namespace Teams.ReadHost.Players.Events
{
    public class SkillChosen : ISubscribedDomainEvent
    {
        public Identity EntityId => PlayerId;
        public GuidIdentity PlayerId { get; }
        public SkillReadModel NewSkill { get; }
        public IEnumerable<FreeSkillPoint> NewFreeSkillPoints { get; }

        public SkillChosen(
            GuidIdentity playerId,
            SkillReadModel newSkill,
            IEnumerable<FreeSkillPoint> newFreeSkillPoints)
        {
            PlayerId = playerId;
            NewSkill = newSkill;
            NewFreeSkillPoints = newFreeSkillPoints;
        }
    }
}