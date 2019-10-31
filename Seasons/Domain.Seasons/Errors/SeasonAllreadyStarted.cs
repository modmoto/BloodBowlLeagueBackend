using Microwave.Domain.Validation;

namespace Domain.Seasons.Errors
{
    public class SeasonAllreadyStarted : DomainError
    {
        public SeasonAllreadyStarted() : base("Can not add a Team to a running season")
        {
        }
    }
}