using Microwave.Domain.Validation;

namespace Domain.Players.DomainErrors
{
    public class NoLevelUpsAvailable : DomainError
    {
        public NoLevelUpsAvailable() : base("No level ups available, player needs more SPs")
        {
        }
    }
}