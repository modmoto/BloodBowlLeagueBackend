using System.Collections.Generic;

namespace Domain.Seasons
{
    public class MatchupMatrix : List<List<MatchupState>>
    {
        public MatchupMatrix(int count)
        {
            var thisList = new List<List<MatchupState>>();
            for (var i = 0; i < count; i++)
            {
                var matchupStates = new List<MatchupState>();
                for (var j = 0; j < count; i++)
                {
                    matchupStates.Add(i == j ? MatchupState.IsDone : MatchupState.IsFree);
                }
                thisList.Add(matchupStates);
            }
            
            AddRange(thisList);
        }

        public void MarkAsDone(int x, int y)
        {
            this[x][y] = MatchupState.IsDone;
        }

        public void MarkRowAsDoneForToday(int x)
        {
            var y = 0;
            foreach (var cell in this[x])
            {
                y++;
                if (cell == MatchupState.IsDone) continue;
                this[x][y - 1] = MatchupState.IsMatchUpForThisDay;
            }
        }

        public void MarkColumAsDoneForToday(int y)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i][y] == MatchupState.IsDone) continue;
                this[i][y] = MatchupState.IsMatchUpForThisDay;
            }
        }
    }
}