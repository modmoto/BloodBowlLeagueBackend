using System.Collections.Generic;
using System.Linq;
using Domain.Matches.Errors;
using Domain.Matches.Events;
using Microwave.Domain;

namespace Domain.Matches
{
    public class Match : Entity, IApply<MatchFinished>, IApply<MatchCreated>
    {
        public GuidIdentity MatchId { get; private set; }

        public GuidIdentity TrainerAsGuest { get; private set; }

        public GuidIdentity TrainerAtHome { get; private set; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; private set; }
        public bool IsFinished { get; private set; }


        public static DomainResult Create(GuidIdentity trainerAtHome, GuidIdentity trainerAsGuest)
        {
            var domainEvents = new MatchCreated(GuidIdentity.Create(), trainerAtHome, trainerAsGuest);
            return DomainResult.Ok(domainEvents);
        }


        public DomainResult Finish(IEnumerable<PlayerProgression> playerProgressions)
        {
            var progressions = playerProgressions.ToList();
            var trainerResults = progressions.GroupBy(p => p.PlayerId).ToList();
            if (TrainersInResultAreNotTheTrainersOfThisMatch(trainerResults)) return DomainResult.Error(new TrainersCanOnlyBeFromThisMatch());

            var homeTouchDowns = CountTouchDowns(trainerResults.Single(k => k.Key == TrainerAtHome));
            var guestTouchDowns = CountTouchDowns(trainerResults.Single(k => k.Key == TrainerAsGuest));


            GameResult gameResult;
            if (homeTouchDowns == guestTouchDowns) gameResult = GameResult.Draw();
            else
            {
                var homeResult = new TrainerGameResult(TrainerAtHome, homeTouchDowns);
                var guestResult = new TrainerGameResult(TrainerAsGuest, guestTouchDowns);

                gameResult = homeTouchDowns > guestTouchDowns
                    ? GameResult.WinResult(homeResult, guestResult)
                    : GameResult.WinResult(guestResult, homeResult);
            }

            var matchResultUploaded = new MatchFinished(MatchId, progressions, gameResult);
            IsFinished = true;
            return DomainResult.Ok(matchResultUploaded);
        }

        private static int CountTouchDowns(IGrouping<GuidIdentity, PlayerProgression> trainerResults)
        {
            return trainerResults.Sum(playerProgression => playerProgression.ProgressionEvents.Count(ev => ev == ProgressionEvent.PlayerMadeTouchdown));
        }

        private bool TrainersInResultAreNotTheTrainersOfThisMatch(IList<IGrouping<GuidIdentity, PlayerProgression>> trainers)
        {
            return !(trainers.Count == 2
                   && (trainers[0].Key== TrainerAtHome || trainers[0].Key == TrainerAsGuest)
                   && (trainers[1].Key== TrainerAtHome || trainers[1].Key == TrainerAsGuest));
        }

        public void Apply(MatchFinished domainEvent)
        {
            PlayerProgressions = domainEvent.PlayerProgressions;
        }

        public void Apply(MatchCreated domainEvent)
        {
            MatchId = (GuidIdentity) domainEvent.EntityId;
            TrainerAtHome = domainEvent.TrainerAtHome;
            TrainerAsGuest = domainEvent.TrainerAsGuest;
        }
    }
}