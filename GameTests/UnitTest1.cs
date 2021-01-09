using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Score_Returns_Total_Score()
        {
            var game = new Game();

            var score = game.Score();

            Assert.AreEqual(0, score);
        }
    }

    public class Game
    {
        public int Score()
        {
            return 0;
        }
    }
}
