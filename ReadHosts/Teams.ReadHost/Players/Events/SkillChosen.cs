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

        public SkillChosen(GuidIdentity playerId, SkillReadModel newSkill)
        {
            PlayerId = playerId;
            NewSkill = newSkill;
        }
    }
}