using System.Collections.Generic;
using System.Linq;
using Microwave.Domain;

namespace Domain.Seasons
{
    public class MatchupMatrix
    {
        private readonly List<List<int>> _matrix;

        public MatchupMatrix(int count)
        {
            var thisList = new List<List<int>>();
            var dayNotation = 0;
            for (var i = 0; i < count; i++)
            {
                var matchupStates = new List<int>();
                for (var j = 0; j < count; j++)
                {
                    dayNotation = dayNotation + 1;
                    if (dayNotation > count) dayNotation = 1;
                    matchupStates.Add(dayNotation);
                }

                dayNotation = i + 1;
                thisList.Add(matchupStates);
            }

            _matrix = thisList;
        }

        public int GetPlayDay(int homeTeam, int guestTeam)
        {
            return _matrix[homeTeam - 1][guestTeam - 1];
        }

        public IEnumerable<GameDay> CreateGameDays(IEnumerable<GuidIdentity> teamIds)
        {
            var count = teamIds.Count();
            var thisList = new List<List<int>>();
            var dayNotation = 0;
            for (var i = 0; i < count; i++)
            {
                var matchupStates = new List<int>();
                for (var j = 0; j < count; j++)
                {
                    dayNotation = dayNotation + 1;
                    if (dayNotation > count) dayNotation = 1;
                    matchupStates.Add(dayNotation);
                }

                dayNotation = i + 1;
                thisList.Add(matchupStates);
            }

            _matrix = thisList;
            return null;
        }
    }
}