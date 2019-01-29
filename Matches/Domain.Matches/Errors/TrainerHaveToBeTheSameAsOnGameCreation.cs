using Microwave.Domain;

namespace Domain.Matches.Errors
{
    public class TrainerHaveToBeTheSameAsOnGameCreation : DomainError
    {
        public TrainerHaveToBeTheSameAsOnGameCreation(GuidIdentity trainerAtHome, GuidIdentity trainerAsGuest) : base
            ($"Can not start a game with this trainers. The trainers that created this match are {trainerAtHome} and {trainerAsGuest}")
        {
        }
    }
}