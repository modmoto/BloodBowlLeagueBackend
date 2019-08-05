using System.Threading.Tasks;
using Microwave.Queries;
using Seasons.ReadHost.Matches.Events;

namespace Seasons.ReadHost.Seasons
{
    public class OnMatchFinishedUpdateGameDay : IHandleAsync<MatchFinished>
    {
        private readonly IReadModelRepository _readModelRepository;

        public OnMatchFinishedUpdateGameDay(IReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public async Task HandleAsync(MatchFinished domainEvent)
        {
            var seasonResult = await _readModelRepository.LoadAll<SeasonReadModel>();
            foreach (var season in seasonResult.Value)
            {
                foreach (var gameDay in season.GameDays)
                {
                    foreach (var matchupDto in gameDay.Matchups)
                    {
                        if (matchupDto.MatchId != domainEvent.MatchId) continue;

                        matchupDto.Result = domainEvent.GameResult;
                        await _readModelRepository.Save(season, season.SeasonId, 0);
                        // todo version fixen
                    }
                }
            }
        }
    }
}