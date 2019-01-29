using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Seasons.UnitTests
{
    [TestClass]
    public class MatchupMatrixTests
    {
        [TestMethod]
        public void InitArray_Ok()
        {
            var matchupMatrix = new MatchupMatrix(4);
            
            Assert.AreEqual(4, matchupMatrix.Count);
            Assert.IsTrue(matchupMatrix.All(r => r.Count == 4));

            Assert.IsTrue(matchupMatrix[0][0] == MatchupState.IsDone);
            Assert.IsTrue(matchupMatrix[1][1] == MatchupState.IsDone);
            Assert.IsTrue(matchupMatrix[2][2] == MatchupState.IsDone);
            Assert.IsTrue(matchupMatrix[3][3] == MatchupState.IsDone);
        }

        [TestMethod]
        public void SetIsWorking()
        {
            var matchupMatrix = new MatchupMatrix(4);
            matchupMatrix.MarkAsDone(1, 1);

            Assert.IsTrue(matchupMatrix[1][0] == MatchupState.IsMatchUpForThisDay);
            Assert.IsTrue(matchupMatrix[1][1] == MatchupState.IsDone);
            Assert.IsTrue(matchupMatrix[1][2] == MatchupState.IsMatchUpForThisDay);
            Assert.IsTrue(matchupMatrix[1][3] == MatchupState.IsMatchUpForThisDay);

            Assert.IsTrue(matchupMatrix[0][1] == MatchupState.IsMatchUpForThisDay);
            Assert.IsTrue(matchupMatrix[1][1] == MatchupState.IsDone);
            Assert.IsTrue(matchupMatrix[2][1] == MatchupState.IsMatchUpForThisDay);
            Assert.IsTrue(matchupMatrix[3][1] == MatchupState.IsMatchUpForThisDay);
        }

        [TestMethod]
        public void ResetIsWorking()
        {
            var matchupMatrix = new MatchupMatrix(4);
            matchupMatrix.MarkAsDone(1, 1);
            matchupMatrix.ResetTodayBlock();

            Assert.IsTrue(matchupMatrix[1][0] == MatchupState.IsFree);
            Assert.IsTrue(matchupMatrix[1][1] == MatchupState.IsDone);
            Assert.IsTrue(matchupMatrix[1][2] == MatchupState.IsFree);
            Assert.IsTrue(matchupMatrix[1][3] == MatchupState.IsFree);
        }

        [TestMethod]
        public void ResetIsWorking_Multiple()
        {
            var matchupMatrix = new MatchupMatrix(4);
            matchupMatrix.MarkAsDone(1, 1);
            matchupMatrix.MarkAsDone(1, 2);
            matchupMatrix.MarkAsDone(3, 3);
            matchupMatrix.ResetTodayBlock();

            Assert.AreEqual(0, matchupMatrix.SelectMany(r => r).Count(r => r == MatchupState.IsMatchUpForThisDay));
        }
    }
}
