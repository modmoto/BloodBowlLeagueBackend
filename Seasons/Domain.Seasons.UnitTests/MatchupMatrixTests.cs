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

            Assert.AreEqual(4, matchupMatrix.GetPlayDay(2, 3));
            Assert.AreEqual(4, matchupMatrix.GetPlayDay(3, 2));
            Assert.AreEqual(1, matchupMatrix.GetPlayDay(2, 4));
            Assert.AreEqual(1, matchupMatrix.GetPlayDay(4, 2));
        }

        [TestMethod]
        public void InitArray_Ok2()
        {
            var matchupMatrix = new MatchupMatrix(6);

            Assert.AreEqual(4, matchupMatrix.GetPlayDay(2, 3));
            Assert.AreEqual(4, matchupMatrix.GetPlayDay(3, 2));
            Assert.AreEqual(5, matchupMatrix.GetPlayDay(2, 4));
            Assert.AreEqual(5, matchupMatrix.GetPlayDay(4, 2));

            Assert.AreEqual(4, matchupMatrix.GetPlayDay(5, 6));
            Assert.AreEqual(4, matchupMatrix.GetPlayDay(6, 5));
        }
    }
}
