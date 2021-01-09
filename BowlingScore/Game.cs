using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class Game
    {
        private List<int> currentFrameScores = new List<int>();
        private Dictionary<int, int> scoreSet = new Dictionary<int, int>();
        private int currentFrame = 1;
        private bool spareBonusActive = false;
        private bool strikeBonusActive = false;

        public void Roll(int score)
        {
            currentFrameScores.Add(score);

            if (spareBonusActive)
            {
                scoreSet[currentFrame - 1] += score;
                spareBonusActive = false;
            }

            if (strikeBonusActive && currentFrameScores.Count == 2)
            {
                scoreSet[currentFrame - 1] += currentFrameScores.Sum();
                strikeBonusActive = false;
            }

            if (currentFrameScores.Sum() == 10)
            {
                if (currentFrameScores.Count > 1)
                {
                    spareBonusActive = true;
                }
                else
                {
                    UpdateCurrentFrame();
                    strikeBonusActive = true;
                }
            }

            if (currentFrameScores.Count > 1)
            {
                UpdateCurrentFrame();
            }
        }

        public int Score()
        {
            var totalScore = 0;

            foreach (var s in scoreSet)
            {
                totalScore += s.Value;
            }
            return totalScore;
        }

        private void UpdateCurrentFrame()
        {
            scoreSet.Add(currentFrame, currentFrameScores.Sum());
            currentFrame++;
            currentFrameScores.Clear();
        }

    }
}
