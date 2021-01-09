using BowlingScore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

        [TestMethod]
        public void Score_Updates_Correctly_After_Roll_Is_Called()
        {
            int[] scoreset = { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2};
            var expectedScore = scoreset.Sum();
            
            var game = new Game();

            foreach (var score in scoreset)
            {
                game.Roll(score);
            }

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Returns_Score_With_Spare_Bonus()
        {
            //index 4 of scoreset should trigger a spare bonus
            int[] scoreset = { 2, 2, 2, 8, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            var expectedScore = scoreset.Sum() + 2;

            var game = new Game();

            foreach (var score in scoreset)
            {
                game.Roll(score);
            }

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }
    }

}
