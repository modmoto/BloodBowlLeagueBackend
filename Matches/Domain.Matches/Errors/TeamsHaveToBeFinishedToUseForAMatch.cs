using Microwave.Domain;

namespace Domain.Matches.Errors
{
    public class TeamsHaveToBeFinishedToUseForAMatch : DomainError
    {
        public TeamsHaveToBeFinishedToUseForAMatch()
            : base("Both Teams have to be finished first, recruit more players")
        {
        }
    }
}