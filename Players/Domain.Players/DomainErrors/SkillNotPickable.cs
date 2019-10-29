using Microwave.Domain;

namespace Domain.Players.DomainErrors
{
    public class SkillNotPickable : DomainError
    {
        public SkillNotPickable(FreeSkillPoint? pickableSkill) : base($"Skill is not pickable, can only choose {pickableSkill}")
        {
        }
    }
}