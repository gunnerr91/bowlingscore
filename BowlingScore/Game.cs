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
        private bool consecutiveStrikeActive = false;
        private int frameToUpdateForConsecutiveStrikeBonus = 0;

        public void Roll(int score)
        {
            if (scoreSet.Count == 10) return;
            currentFrameScores.Add(score);

            if (spareBonusActive)
            {
                scoreSet[currentFrame - 1] += score;
                spareBonusActive = false;
            }

            if (consecutiveStrikeActive)
            {
                if (currentFrameScores.Count == 2)
                {
                    scoreSet[frameToUpdateForConsecutiveStrikeBonus] += currentFrameScores.Sum();
                    consecutiveStrikeActive = false;
                    strikeBonusActive = false;
                }
                if (currentFrameScores.Count == 1 && strikeBonusActive)
                {
                    if (score == 10)
                    {
                        scoreSet[frameToUpdateForConsecutiveStrikeBonus] += 20;
                    }
                    else
                    {
                        scoreSet[frameToUpdateForConsecutiveStrikeBonus] += 10 + score;
                    }
                    consecutiveStrikeActive = false;
                }
            }

            if (strikeBonusActive)
            {
                if (currentFrameScores.Count == 1 && currentFrameScores.Sum() == 10)
                {
                    UpdateScoreAndClearCurrentFrame();
                    consecutiveStrikeActive = true;
                    frameToUpdateForConsecutiveStrikeBonus = currentFrame - 2;
                }
                else if (currentFrameScores.Count == 2)
                {
                    scoreSet[currentFrame - 1] += currentFrameScores.Sum();
                    strikeBonusActive = false;
                }

            }

            if (currentFrameScores.Sum() == 10)
            {
                if (currentFrameScores.Count > 1)
                {
                    spareBonusActive = true;
                }
                else
                {
                    UpdateScoreAndClearCurrentFrame();
                    strikeBonusActive = true;
                }
            }

            if (currentFrameScores.Count > 1)
            {
                UpdateScoreAndClearCurrentFrame();
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

        private void UpdateScoreAndClearCurrentFrame()
        {
            scoreSet.Add(currentFrame, currentFrameScores.Sum());
            currentFrame++;
            currentFrameScores.Clear();
        }

    }
}
