using Microwave.Domain;

namespace Domain.Matches.Matches.Errors
{
    public class MatchAllreadyFinished : DomainError
    {
        public MatchAllreadyFinished() : base("Can not finish a game that was allready finished")
        {
        }
    }
}