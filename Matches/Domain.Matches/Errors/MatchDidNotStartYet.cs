using Microwave.Domain;

namespace Domain.Matches.Errors
{
    public class MatchDidNotStartYet : DomainError
    {
        public MatchDidNotStartYet() : base("Match not started yet, start the match first")
        {
        }
    }
}