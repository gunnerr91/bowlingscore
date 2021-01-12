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
            if (currentFrame > 10) return;

            currentFrameScores.Add(score);

            if (currentFrame == 10)
            {
                if (score == 10 && currentFrameScores.Count == 1 && strikeBonusActive)
                {
                    consecutiveStrikeActive = true;
                    frameToUpdateForConsecutiveStrikeBonus = currentFrame - 1;
                    return;
                }

                if (currentFrameScores.Count == 2)
                {
                    scoreSet.Add(currentFrame, currentFrameScores.Sum());

                    if (currentFrameScores.Sum() == 10)
                    {
                        if (strikeBonusActive)
                        {
                            scoreSet[currentFrame - 1] += currentFrameScores.Sum();
                            strikeBonusActive = false;
                        }

                        spareBonusActive = true;
                    }
                }

                if (currentFrameScores.Count == 3 && currentFrameScores.Sum() > 10)
                {
                    scoreSet[currentFrame] = currentFrameScores.Sum();
                    currentFrame++;
                    return;
                }

            }

            if (spareBonusActive)
            {
                if (currentFrame == 10 && currentFrameScores.Count > 1)
                {
                    scoreSet[currentFrame] += score;
                    return;
                }
                else
                {
                    scoreSet[currentFrame - 1] += score;
                }
                spareBonusActive = false;
            }

            if (consecutiveStrikeActive)
            {
                if (currentFrameScores.Count == 2)
                {
                    scoreSet[frameToUpdateForConsecutiveStrikeBonus] += currentFrameScores.Sum();
                    consecutiveStrikeActive = false;
                    if (currentFrame != 10)
                    {
                        strikeBonusActive = false;
                    }
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
                    if (currentFrame != 10)
                    {
                        scoreSet[currentFrame - 1] += currentFrameScores.Sum();
                    }
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

            if (currentFrameScores.Count > 1 && currentFrame != 10)
            {
                UpdateScoreAndClearCurrentFrame();
            }
        }

        public int Score() => scoreSet.Sum(x => x.Value);

        private void UpdateScoreAndClearCurrentFrame()
        {
            scoreSet.Add(currentFrame, currentFrameScores.Sum());
            currentFrame++;
            currentFrameScores.Clear();
        }

    }
}
