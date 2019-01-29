using Microwave.Domain;

namespace Domain.Seasons
{
    public class SeasonAllreadyStarted : DomainError
    {
        public SeasonAllreadyStarted() : base("Can not add a Team to a running season")
        {
        }
    }
}