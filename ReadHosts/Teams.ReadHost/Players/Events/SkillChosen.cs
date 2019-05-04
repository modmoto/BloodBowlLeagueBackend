using Microwave.Domain;

namespace Teams.ReadHost.Players.Events
{
    public class SkillChosen
    {
        public GuidIdentity PlayerId { get; }
        public StringIdentity NewSkill { get; }
        public SkillChosen(GuidIdentity playerId, StringIdentity newSkill)
        {
            PlayerId = playerId;
            NewSkill = newSkill;
        }
    }
}