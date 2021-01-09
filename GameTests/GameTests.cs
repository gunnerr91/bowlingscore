using BowlingScore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Score_Returns_Total_Score()
        {
            var game = new Game();

            var score = game.Score();

            Assert.AreEqual(0, score);
        }
    }

}
