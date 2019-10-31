using System;
using Microwave.Domain.Validation;

namespace Domain.Matches.Errors
{
    public class PlayerWasNotPartOfTheTeamWhenStartingTheMatch : DomainError
    {
        public PlayerWasNotPartOfTheTeamWhenStartingTheMatch(Guid playerIdentity) : base($"The progression for player {playerIdentity} are not possible, as they where not part of one team on the creation of this match.")
        {
        }
    }
}