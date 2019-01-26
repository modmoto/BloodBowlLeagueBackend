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
        public TeamReadModel GuestTeam { get; private set; }
        public TeamReadModel HomeTeam { get; private set; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; private set; }
        public bool IsFinished { get; private set; }


        public static DomainResult Create(TeamReadModel trainerAtHome, TeamReadModel trainerAsGuest)
        {
            var domainEvents = new MatchCreated(GuidIdentity.Create(), trainerAtHome, trainerAsGuest);
            return DomainResult.Ok(domainEvents);
        }


        public DomainResult Finish(IEnumerable<PlayerProgression> playerProgressions)
        {
            if (IsFinished) return DomainResult.Error(new MatchAllreadyFinished());
            var progressions = playerProgressions.ToList();

            var homeTeamProgression = new List<PlayerProgression>();
            var guestTeamProgression = new List<PlayerProgression>();
            foreach (var progression in progressions)
            {
                if (HomeTeam.Players.Contains(progression.PlayerId))
                {
                    homeTeamProgression.Add(progression);
                    continue;
                }

                if (GuestTeam.Players.Contains(progression.PlayerId))
                {
                    guestTeamProgression.Add(progression);
                    continue;
                }

                return DomainResult.Error(new PlayerWasNotPartOfTheTeamOnMatchCreation(progression.PlayerId));
            }

            var homeTouchDowns = CountTouchDowns(homeTeamProgression);
            var guestTouchDowns = CountTouchDowns(guestTeamProgression);

            GameResult gameResult;
            if (homeTouchDowns == guestTouchDowns) gameResult = GameResult.Draw();
            else
            {
                var homeResult = new TrainerGameResult(HomeTeam.TeamId, homeTouchDowns);
                var guestResult = new TrainerGameResult(GuestTeam.TeamId, guestTouchDowns);

                gameResult = homeTouchDowns > guestTouchDowns
                    ? GameResult.WinResult(homeResult, guestResult)
                    : GameResult.WinResult(guestResult, homeResult);
            }

            var matchResultUploaded = new MatchFinished(MatchId, progressions, gameResult);
            IsFinished = true;
            return DomainResult.Ok(matchResultUploaded);
        }

        private static int CountTouchDowns(IEnumerable<PlayerProgression> trainerResults)
        {
            return trainerResults.Sum(playerProgression => playerProgression.ProgressionEvents.Count(ev => ev == ProgressionEvent.PlayerMadeTouchdown));
        }

        public void Apply(MatchFinished domainEvent)
        {
            IsFinished = true;
            PlayerProgressions = domainEvent.PlayerProgressions;
        }

        public void Apply(MatchCreated domainEvent)
        {
            MatchId = (GuidIdentity) domainEvent.EntityId;
            HomeTeam = domainEvent.HomeTeam;
            GuestTeam = domainEvent.GuestTeam;
        }
    }
}