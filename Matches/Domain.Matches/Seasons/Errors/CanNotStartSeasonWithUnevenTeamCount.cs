using Microwave.Domain;

namespace Domain.Matches.Seasons.Errors
{
    public class CanNotStartSeasonWithUnevenTeamCount : DomainError
    {
        public CanNotStartSeasonWithUnevenTeamCount(int count) : base($"Can not start a season with {count} teams, they have to be even. Add the orc dummy team!")
        {
        }
    }
}