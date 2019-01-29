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
                for (var j = 0; j < count; j++)
                {
                    matchupStates.Add(i == j ? MatchupState.IsDone : MatchupState.IsFree);
                }
                thisList.Add(matchupStates);
            }
            
            AddRange(thisList);
        }

        public void MarkAsDone(int column, int row)
        {
            this[column][row] = MatchupState.IsDone;
            this[row][column] = MatchupState.IsDone;
            MarkColumnAsDoneForToday(column);
            MarkColumnAsDoneForToday(row);
            MarkRowAsDoneForToday(row);
            MarkRowAsDoneForToday(column);
        }

        private void MarkColumnAsDoneForToday(int x)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[x][i] == MatchupState.IsDone) continue;
                this[x][i] = MatchupState.IsMatchUpForThisDay;
            }
        }

        private void MarkRowAsDoneForToday(int y)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i][y] == MatchupState.IsDone) continue;
                this[i][y] = MatchupState.IsMatchUpForThisDay;
            }
        }

        public void ResetTodayBlock()
        {
            for (var i = 0; i < Count; i++)
            {
                var rows = this[i];
                for (var index = 0; index < rows.Count; index++)
                {
                    if (rows[index] == MatchupState.IsMatchUpForThisDay) rows[index] = MatchupState.IsFree;
                }
            }
        }
    }
}