using Microwave.Domain.Validation;

namespace Domain.Players.DomainErrors
{
    public class PlayerIsNotLeveledUpAndHasNoFreeSkillPoints : DomainError
    {
        public PlayerIsNotLeveledUpAndHasNoFreeSkillPoints() : base("Player has no free level up to choose a skill type from.")
        {
        }
    }
}