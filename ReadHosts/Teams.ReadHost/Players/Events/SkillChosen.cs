using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class SkillChosen : ISubscribedDomainEvent
    {
        public GuidIdentity PlayerId { get; }
        public StringIdentity NewSkill { get; }
        public SkillChosen(GuidIdentity playerId, StringIdentity newSkill)
        {
            PlayerId = playerId;
            NewSkill = newSkill;
        }

        public Identity EntityId => PlayerId;
    }
}