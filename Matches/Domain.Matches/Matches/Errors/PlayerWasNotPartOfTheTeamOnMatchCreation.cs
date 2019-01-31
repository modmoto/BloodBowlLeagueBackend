using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Matches.Matches.Errors
{
    public class PlayerWasNotPartOfTheTeamOnMatchCreation : DomainError
    {
        public PlayerWasNotPartOfTheTeamOnMatchCreation(IEnumerable<GuidIdentity> playerIdentity) : base($"The progression for players {string.Join(", ", playerIdentity)} are not possible, as they where not part of one team on the creation of this match.")
        {
        }
    }
}