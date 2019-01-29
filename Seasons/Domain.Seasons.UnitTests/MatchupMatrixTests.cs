using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Seasons.UnitTests
{
    [TestClass]
    public class MatchupMatrixTests
    {
        [TestMethod]
        public void MakePairings_GameDaysOk()
        {
            var matchupMatrix = new MatchupMatrix(4);
            
            Assert.AreEqual(4, matchupMatrix.Count);
            Assert.AreEqual(4, matchupMatrix.Select(r => r.Count).Distinct());
        }
    }
}
