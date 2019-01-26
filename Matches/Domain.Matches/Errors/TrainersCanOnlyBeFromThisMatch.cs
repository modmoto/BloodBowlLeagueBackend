using Microwave.Domain;

namespace Domain.Matches.Errors
{
    public class TrainersCanOnlyBeFromThisMatch : DomainError
    {
        public TrainersCanOnlyBeFromThisMatch() : base("The trainers did not create this match, can not finish with this results.")
        {
        }
    }
}