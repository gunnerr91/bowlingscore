using BowlingScore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GameTests
{
    [TestClass]
    public class GameTests
    {
        private Game game;

        [TestInitialize]
        public void Setup()
        {
            game = new Game();
        }

        [TestMethod]
        public void Score_Returns_Total_Score()
        {
            var score = game.Score();

            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void Score_Updates_Correctly_After_Roll_Is_Called()
        {
            int[] scoreSet = { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2};
            var expectedScore = scoreSet.Sum();

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Returns_Score_With_Spare_Bonus()
        {
            //index 4 of scoreSet should trigger a spare bonus
            int[] scoreSet = { 2, 2, 2, 8, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            var expectedScore = scoreSet.Sum() + 2;

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Returns_Score_With_Strike_Bonus()
        {
            int[] scoreSet = { 2, 2, 10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            var expectedScore = scoreSet.Sum() + 4;

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Returns_Correct_Score_For_Consecutive_Strike_Bonus()
        {
            int[] scoreSet = { 2, 2, 10, 10, 10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            var expectedScore = scoreSet.Sum() + 20 + 12 + 4;

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Score_Does_Not_Update_After_Game_Is_Complete()
        {
            int[] scoreSet = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            var expectedScore = scoreSet.Sum() - 4;

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Returns_Score_With_Spare_Bonus_On_The_10th_Frame()
        {
            int[] scoreSet = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 8, 2 };
            var expectedScore = scoreSet.Sum();

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Returns_Score_With_Spare_Bonus_On_The_9th_And_10th_Frame()
        {
            int[] scoreSet = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 8, 2, 8, 2 };
            var expectedScore = scoreSet.Sum() + 2;

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        [TestMethod]
        public void Returns_Score_With_Strike_Bonus_On_The_10th_Frame()
        {
            int[] scoreSet = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10, 10, 2, 2 };
            var expectedScore = scoreSet.Sum() + 12;

            RollTheBowl(scoreSet);

            var totalScore = game.Score();

            Assert.AreEqual(expectedScore, totalScore);
        }

        //[TestMethod]
        //public void Testing_Assessment()
        //{
        //    int[] scoreSet = { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10, 2, 8, 6 };
        //    var expectedScore = 133;

        //    RollTheBowl(scoreSet);

        //    var totalScore = game.Score();

        //    Assert.AreEqual(expectedScore, totalScore);
        //}

        private void RollTheBowl(int[] scoreSet)
        {
            foreach (var score in scoreSet)
            {
                game.Roll(score);
            }
        }
    }

}
