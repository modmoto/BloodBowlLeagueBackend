using Microwave.Domain;

namespace Domain.Matches.Errors
{
    public class PlayerWasNotPartOfTheTeamOnMatchCreation : DomainError
    {
        public PlayerWasNotPartOfTheTeamOnMatchCreation(GuidIdentity playerIdentity) : base($"The progression for player {playerIdentity} is not possible, as he was not part of one team on the creation of this match.")
        {
        }
    }
}