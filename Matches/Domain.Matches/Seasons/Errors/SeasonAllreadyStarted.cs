using Microwave.Domain;

namespace Domain.Matches.Seasons.Errors
{
    public class SeasonAllreadyStarted : DomainError
    {
        public SeasonAllreadyStarted() : base("Can not add a Team to a running season")
        {
        }
    }
}