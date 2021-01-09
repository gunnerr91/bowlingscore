using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class Game
    {
        private List<int> scoreSet = new List<int>();
        private List<int> currentFrameScores = new List<int>();
        private Dictionary<int, int> scoreSetv2 = new Dictionary<int, int>();
        private int currentFrame = 1;
        private bool spareBonusActive = false;

        public void Roll(int score)
        {
            scoreSet.Add(score);

            currentFrameScores.Add(score);

            if (spareBonusActive)
            {
                scoreSetv2[currentFrame - 1] += score;
                spareBonusActive = false;
            }

            if (currentFrameScores.Sum() == 10)
            {
                spareBonusActive = true;
            }


            if (currentFrameScores.Count > 1)
            {

                scoreSetv2.Add(currentFrame, currentFrameScores.Sum());
                currentFrame++;
                currentFrameScores.Clear();
            }
        }

        public int Score()
        {
            var totalScore = 0;

            foreach (var s in scoreSetv2)
            {
                totalScore += s.Value;
            }
            return totalScore;
        }

    }
}
