using Microwave.Domain;
using Microwave.Domain.Validation;

namespace Domain.Seasons.Errors
{
    public class SeasonMustHaveAtLeast2Teams : DomainError
    {
        public SeasonMustHaveAtLeast2Teams(int teamCount)
            : base($"Season needs to have at least two players, current TeamCount is {teamCount}")
        {
        }
    }
}