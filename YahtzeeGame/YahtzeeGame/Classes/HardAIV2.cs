using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    public class HardAIV2
    {
        public int[] RollingStrategy(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];

            string currentHand = DetermineHand(scoreCard, dice);

            #region currentHand Decision Check

            if (currentHand == "Yahtzee")
            {
                decision = YahtzeeHolds(scoreCard, dice);
            }

            if (currentHand == "Four Match")
            {
                decision = FourMatchHolds(scoreCard, dice);
            }

            if (currentHand == "Three Match")
            {
                decision = ThreeMatchHolds(scoreCard, dice);
            }
            
            if (currentHand == "Full House")
            {
                decision = FullHouseHolds(scoreCard, dice);
            }

            if (currentHand == "Two Pair")
            {
                decision = TwoPairHolds(scoreCard, dice);
            }

            if (currentHand == "Large Straight")
            {
                decision = LargeStraightHolds(scoreCard, dice);
            }

            if (currentHand == "Small Straight")
            {
                decision = SmallStraightHolds(scoreCard, dice);
            }

            if (currentHand == "Pair")
            {
                decision = PairHolds(scoreCard, dice);
            }

            if (currentHand == "Three Straight")
            {
                decision = ThreeStraightHolds(scoreCard, dice);
            }
            #endregion  

            //Decision is based as an array with a counter. If the array says [0, 0, 2, 3, 0, 0], that means the bot wants to hold 2 threes and 3 fours.

            return decision;
        }

        #region Rolling Strategies

        private int[] YahtzeeHolds(ScoreCard scoreCard, int[] dice)
        {
            int x = dice[0];

            int[] decision = new int[6];

            decision[x - 1] = 5; 

            return decision;
        }

        private int[] FourMatchHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);
            int x = 0;

            foreach (int num in count)
            {
                if (num == 4)
                {
                    decision[x] = 4;
                }

                x++;
            }

            return decision;
        }

        private int[] ThreeMatchHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);
            int x = 0;

            foreach (int num in count)
            {
                if (num == 3)
                {
                    decision[x] = 3;
                }

                x++;
            }


            return decision;
        }

        private int[] FullHouseHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);
            int iterator = 0;
            int threeMatchNumber = 0;
            int highestKeep = 2;

            if (scoreCard.ThreesScored)
            {
                highestKeep++;

                if (scoreCard.FoursScored)
                {
                    highestKeep++;

                    if (scoreCard.FivesScored)
                    {
                        highestKeep++;
                    }
                }
            }
            
            foreach (int num in count)
            {
                if (num == 3)
                {
                    threeMatchNumber = iterator;
                    break;
                }
                iterator++;
            }

            if (threeMatchNumber <= highestKeep)
            {
                decision = count;
            }
            else
            {
                decision[threeMatchNumber] = 3;
            }

            return decision;
        }

        private int[] TwoPairHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);

            if ((count[0] == 2 && (count[1] == 2 || count[2] == 2)) && !scoreCard.FullHouseScored)
            {
                decision[0] = 2;

                if (count[1] == 2)
                {
                    decision[1] = 2;
                }

                if (count[2] == 2)
                {
                    decision[2] = 2;
                }
            }
            else 
            {
                int x = 0;
                foreach (int num in count)
                {
                    if (num == 2)
                    {
                        decision = new int[6];
                        decision[x] = 2;
                    }

                    x++;
                }
            }
            
            return decision;
        }

        private int[] LargeStraightHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);

            if (!scoreCard.LargeStraightScored)
            {
                decision = count;
            }
            else if (!scoreCard.SmallStraightScored && (count[1] == 1 && count[2] == 1 && count[3] == 1 && count[4] == 1))
            {
                decision[1]= 1;
                decision[2]= 1;
                decision[3]= 1;
                decision[4]= 1;
            }
            else if (!scoreCard.SmallStraightScored && (count[0] == 1 && count[1] == 1 && count[2] == 1 && count[3] == 1))
            {
                decision[0] = 1;
                decision[1] = 1;
                decision[2] = 1;
                decision[3] = 1;
            }
            else if (!scoreCard.SmallStraightScored && (count[2] == 1 && count[3] == 1 && count[4] == 1 && count[5] == 1))
            {
                decision[2] = 1;
                decision[3] = 1;
                decision[4] = 1;
                decision[5] = 1;
            }
            else if (count[4] == 1)
            {
                decision[4] = 1;
            }
            else
            {
                decision[3] = 1;
            }

                return decision;
        }

        private int[] SmallStraightHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);

            if (!scoreCard.LargeStraightScored || !scoreCard.SmallStraightScored)
            {
                decision = count;
            }
            else if (count.Contains(2))
            {
                int x = 0;
                foreach (int num in count)
                {
                    if (num == 2)
                    {
                        decision = new int[6];
                        decision[x] = 2;
                    }

                    x++;
                }
            }
            else
            {
                int x = 0;
                foreach (int num in count)
                {
                    if (num == 1)
                    {
                        decision = new int[6];
                        decision[x] = 1;
                    }

                    x++;
                }
            }
            
            return decision;
        }

        private int[] PairHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);

            if ((count[1] == 2 && !scoreCard.TwosScored) ||
                (count[2] == 2 && !scoreCard.ThreesScored) ||
                (count[3] == 2 && !scoreCard.FoursScored) ||
                (count[4] == 2 && !scoreCard.FivesScored) ||
                (count[5] == 2 && !scoreCard.SixesScored))
            {
                int x = 0;
                foreach (int num in count)
                {
                    if (num == 2)
                    {
                        decision = new int[6];
                        decision[x] = 2;
                    }

                    x++;
                }
            }
            else if ((!scoreCard.SmallStraightScored || !scoreCard.LargeStraightScored) &&
                     (count[2] > 0 && count[3] > 0 && count[4] > 0))
            {
                decision[2] = 1;
                decision[3] = 1;
                decision[4] = 1;
            }
            else if (count[4] == 1)
            {
                decision[4] = 1;
            }
            else if (count[3] == 1)
            {
                decision[3] = 1;
            }
            else
            {
                decision[5] = 1;
            }

            return decision;
        }

        private int[] ThreeStraightHolds(ScoreCard scoreCard, int[] dice)
        {
            int[] decision = new int[6];
            int[] count = scoreCard.DieCounter(dice);

            if (count[1] == 1 && count[2] == 1 && count[4] == 1)
            {
                decision[1] = 1;
                decision[2] = 1;
                decision[4] = 1;
            }
            else if (count[3] == 1 && count[4] == 1 && count[5] == 1)
            {
                decision[3] = 1;
                decision[4] = 1;
                decision[5] = 1;
            }
            else if (count[4] == 1)
            {
                decision[4] = 1;
            }
            else
            {
                decision[3] = 1;
            }

            return decision;
        }

        #endregion

        public int ScoringStrategy(ScoreCard scoreCard, int[] dice)
        {
            int decision = 0;

            string currentHand = DetermineHand(scoreCard, dice);

            #region currentHand Decision Check

            if (currentHand == "Yahtzee")
            {
                decision = YahtzeePick(scoreCard, dice);
            }

            if (currentHand == "Four Match")
            {
                decision = FourMatchPick(scoreCard, dice);
            }

            if (currentHand == "Three Match")
            {
                decision = ThreeMatchPick(scoreCard, dice);
            }

            if (currentHand == "Full House")
            {
                decision = FullHousePick(scoreCard, dice);
            }

            if (currentHand == "Two Pair")
            {
                decision = TwoPairPick(scoreCard, dice);
            }

            if (currentHand == "Large Straight")
            {
                decision = LargeStraightPick(scoreCard, dice);
            }

            if (currentHand == "Small Straight")
            {
                decision = SmallStraightPick(scoreCard, dice);
            }

            if (currentHand == "Pair")
            {
                decision = PairPick(scoreCard, dice);
            }

            if (currentHand == "Three Straight")
            {
                decision = ThreeStraightPick(scoreCard, dice);
            }
            #endregion  

            return decision;
        }

        #region Scoring Strategies

        //1 = Aces, 2 = Twos, 3 = Threes, 4 = Fours, 5 = Fives, 6 = Sixes, 
        //7 = Three of a Kind, 8 = Four of a Kind, 9 = Full House,
        //10 = Small Straight, 11 = Large Straight, 12 = Yahtzee, 
        //13 = Chance

        
        private int YahtzeePick(ScoreCard scoreCard, int[] dice)
        {
            int decision = 0;
            int[] count = scoreCard.DieCounter(dice);

            if (!scoreCard.YahtzeeScored)
            {
                decision = 12;
            }
            else
            {
                decision = JokerPriority(scoreCard, count);
            }

            return decision;
        }
        private int FourMatchPick(ScoreCard scoreCard, int[] dice)
        {
            int decision = DecideUpperSection(scoreCard, dice);
            
            if (!scoreCard.FourOfAKindScored && decision == 0)
            {
                decision = 8;
            }
            else if (decision == 0)
            {
                decision = LeastPriority(scoreCard);
            }

            return decision;
        }
        private int ThreeMatchPick(ScoreCard scoreCard, int[] dice)
        {
            int decision = DecideUpperSection(scoreCard, dice);

            if (scoreCard.ThreeOfAKind >= 21)
            {
                decision = 7;
            }
            else if (decision == 0)
            {
                decision = LeastPriority(scoreCard);
            }

            return decision;
        }
        private int FullHousePick(ScoreCard scoreCard, int[] dice)
        {
            int decision = 0;

            if (!scoreCard.FullHouseScored)
            {
                decision = 9;
            }
            else
            {
                decision = LeastPriority(scoreCard);
            }

            return decision;
        }
        private int TwoPairPick(ScoreCard scoreCard, int[] dice)
        {
            int decision = 0;
            int[] count = scoreCard.DieCounter(dice);

            if (count[1] == 2 && !scoreCard.TwosScored)
            {
                decision = 2;
            }
            else if (count[0] == 2 && !scoreCard.AcesScored)
            {
                decision = 1;
            }
            else if (!scoreCard.ChanceScored && scoreCard.Chance >= 20)
            {
                decision = 13;
            }
            else if (!scoreCard.AcesScored)
            {
                decision = 1;
            }
            else
            {
                decision = LeastPriority(scoreCard);
            } 
            
            return decision;
        }
        private int LargeStraightPick(ScoreCard scoreCard, int[] dice)
        {
            int decision = 0;

            if (!scoreCard.LargeStraightScored)
            {
                decision = 11;
            }
            else if (!scoreCard.SmallStraightScored)
            {
                decision = 10;
            }
            else 
            {
                decision = LeastPriority(scoreCard);
            } 
            
            return decision;
        }
        private int SmallStraightPick(ScoreCard scoreCard, int[] dice)
        {
            int decision = 0;
            int[] count = scoreCard.DieCounter(dice);

            if (!scoreCard.SmallStraightScored)
            {
                decision = 10;
            }
            else
            {
                decision = LeastPriority(scoreCard);
            }

            return decision;
        }
        private int PairPick(ScoreCard scoreCard, int[] dice)
        {
            int decision = 0;
            int[] count = scoreCard.DieCounter(dice);

            if (scoreCard.Chance >= 20 && !scoreCard.ChanceScored)
            {
                decision = 13;
            }
            else if ((count[0] == 2 || count[1] == 2 || count[2] == 2) &&
                     count[3] <= 2 && count[4] <= 2 && count[5] <= 2)
            {
                decision = DecideUpperSection(scoreCard, dice);
            }
            else if (count[0] == 1 && !scoreCard.AcesScored)
            {
                decision = 1;
            }
            else if (!scoreCard.ChanceScored)
            {
                decision = 13;
            }
            else
            {
                decision = LeastPriority(scoreCard);
            }

            return decision;
        }
        private int ThreeStraightPick(ScoreCard scoreCard, int[] dice)
        {
            return LeastPriority(scoreCard);
        }
        private int LeastPriority(ScoreCard scoreCard)
        {
            if (!scoreCard.AcesScored)
            {
                return 1;
            }
            else if (!scoreCard.TwosScored)
            {
                return 2;
            }
            else if (!scoreCard.ThreesScored)
            {
                return 3;
            }
            else if (!scoreCard.FoursScored)
            {
                return 4;
            }
            else if (!scoreCard.FourOfAKindScored)
            {
                return 8;
            }
            else if (!scoreCard.FivesScored)
            {
                return 5;
            }
            else if (!scoreCard.YahtzeeScored)
            {
                return 12;
            }
            else if (!scoreCard.SixesScored)
            {
                return 6;
            }
            else if (!scoreCard.ThreeOfAKindScored)
            {
                return 7;
            }
            else if (!scoreCard.ChanceScored)
            {
                return 13;
            }
            else if (!scoreCard.FullHouseScored)
            {
                return 9;
            }
            else if (!scoreCard.SmallStraightScored)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }
        private int JokerPriority(ScoreCard scoreCard, int[] count)
        {
            if (count[0] == 5)
            {
                if (!scoreCard.LargeStraightScored)
                {
                    return 11;
                }
                if (!scoreCard.SmallStraightScored)
                {
                    return 10;
                }
                if (!scoreCard.FullHouseScored)
                {
                    return 9;
                }
                if (!scoreCard.FourOfAKindScored)
                {
                    return 8;
                }
                if (!scoreCard.ThreeOfAKindScored)
                {
                    return 7;
                }
                if (!scoreCard.ChanceScored)
                {
                    return 13;
                }
                if (!scoreCard.TwosScored)
                {
                    return 2;
                }
                if (!scoreCard.ThreesScored)
                {
                    return 3;
                }
                if (!scoreCard.FoursScored)
                {
                    return 4;
                }
                if (!scoreCard.FivesScored)
                {
                    return 5;
                }
                if (!scoreCard.SixesScored)
                {
                    return 6;
                }
            }

            if (count[1] == 5)
            {
                if (!scoreCard.LargeStraightScored)
                {
                    return 11;
                }
                if (!scoreCard.SmallStraightScored)
                {
                    return 10;
                }
                if (!scoreCard.FullHouseScored)
                {
                    return 9;
                }
                if (!scoreCard.FourOfAKindScored)
                {
                    return 8;
                }
                if (!scoreCard.ThreeOfAKindScored)
                {
                    return 7;
                }
                if (!scoreCard.ChanceScored)
                {
                    return 13;
                }
                if (!scoreCard.AcesScored)
                {
                    return 1;
                }
                if (!scoreCard.ThreesScored)
                {
                    return 3;
                }
                if (!scoreCard.FoursScored)
                {
                    return 4;
                }
                if (!scoreCard.FivesScored)
                {
                    return 5;
                }
                if (!scoreCard.SixesScored)
                {
                    return 6;
                }
            }

            if (count[2] == 5)
            {
                if (!scoreCard.LargeStraightScored)
                {
                    return 11;
                }
                if (!scoreCard.SmallStraightScored)
                {
                    return 10;
                }
                if (!scoreCard.FullHouseScored)
                {
                    return 9;
                }
                if (!scoreCard.FourOfAKindScored)
                {
                    return 8;
                }
                if (!scoreCard.ThreeOfAKindScored)
                {
                    return 7;
                }
                if (!scoreCard.ChanceScored)
                {
                    return 13;
                }
                if (!scoreCard.AcesScored)
                {
                    return 1;
                }
                if (!scoreCard.TwosScored)
                {
                    return 2;
                }
                if (!scoreCard.FoursScored)
                {
                    return 4;
                }
                if (!scoreCard.FivesScored)
                {
                    return 5;
                }
                if (!scoreCard.SixesScored)
                {
                    return 6;
                }
            }

            if (count[3] == 5)
            {
                if (!scoreCard.FourOfAKindScored)
                {
                    return 8;
                }
                if (!scoreCard.ThreeOfAKindScored)
                {
                    return 7;
                }
                if (!scoreCard.LargeStraightScored)
                {
                    return 11;
                }
                if (!scoreCard.SmallStraightScored)
                {
                    return 10;
                }
                if (!scoreCard.FullHouseScored)
                {
                    return 9;
                }
                if (!scoreCard.ChanceScored)
                {
                    return 13;
                }

                if (!scoreCard.AcesScored)
                {
                    return 1;
                }
                if (!scoreCard.TwosScored)
                {
                    return 2;
                }
                if (!scoreCard.ThreesScored)
                {
                    return 3;
                }
                if (!scoreCard.FivesScored)
                {
                    return 5;
                }
                if (!scoreCard.SixesScored)
                {
                    return 6;
                }
            }

            if (count[4] == 5)
            {
                if (!scoreCard.FourOfAKindScored)
                {
                    return 8;
                }
                if (!scoreCard.ThreeOfAKindScored)
                {
                    return 7;
                }
                if (!scoreCard.LargeStraightScored)
                {
                    return 11;
                }
                if (!scoreCard.SmallStraightScored)
                {
                    return 10;
                }
                if (!scoreCard.FullHouseScored)
                {
                    return 9;
                }
                if (!scoreCard.ChanceScored)
                {
                    return 13;
                }

                if (!scoreCard.AcesScored)
                {
                    return 1;
                }
                if (!scoreCard.TwosScored)
                {
                    return 2;
                }
                if (!scoreCard.ThreesScored)
                {
                    return 3;
                }
                if (!scoreCard.FoursScored)
                {
                    return 4;
                }
                if (!scoreCard.SixesScored)
                {
                    return 6;
                }
            }

            if (count[5] == 5)
            {
                if (!scoreCard.FourOfAKindScored)
                {
                    return 8;
                }
                if (!scoreCard.ThreeOfAKindScored)
                {
                    return 7;
                }
                if (!scoreCard.LargeStraightScored)
                {
                    return 11;
                }
                if (!scoreCard.SmallStraightScored)
                {
                    return 10;
                }
                if (!scoreCard.FullHouseScored)
                {
                    return 9;
                }
                if (!scoreCard.ChanceScored)
                {
                    return 13;
                }

                if (!scoreCard.AcesScored)
                {
                    return 1;
                }
                if (!scoreCard.TwosScored)
                {
                    return 2;
                }
                if (!scoreCard.ThreesScored)
                {
                    return 3;
                }
                if (!scoreCard.FoursScored)
                {
                    return 4;
                }
                if (!scoreCard.FivesScored)
                {
                    return 5;
                }
            }

            return 0;
        }
        private int DecideUpperSection(ScoreCard scoreCard, int[] dice)
        {
            int[] count = scoreCard.DieCounter(dice);
            int iterator = 0;
            int matchNumber = 0;

            foreach (int num in count)
            {
                if (num == 4)
                {
                    matchNumber = iterator;
                }
                iterator++;
            }

            if (matchNumber == 0 && !scoreCard.AcesScored)
            {
                return 1;
            }
            else if (matchNumber == 1 && !scoreCard.TwosScored)
            {
                return 2;
            }
            else if (matchNumber == 2 && !scoreCard.ThreesScored)
            {
                return 3;
            }
            else if (matchNumber == 3 && !scoreCard.FoursScored)
            {
                return 4;
            }
            else if (matchNumber == 4 && !scoreCard.FivesScored)
            {
                return 5;
            }
            else if (matchNumber == 5 && !scoreCard.SixesScored)
            {
                return 6;
            }

            return 0;
        }
        #endregion

        private string DetermineHand(ScoreCard scoreCard, int[] dice)
        {
            int[] dieCount = scoreCard.DieCounter(dice);

            //Yahtzee

            if (scoreCard.YahtzeeValidation(dice))
            {
                return "Yahtzee";
            }

            //Four Match

            if (scoreCard.FourKindValidation(dice))
            {
                return "Four Match";
            }

            //Three Match

            if (scoreCard.ThreeKindValidation(dice))
            {
                return "Three Match";
            }
            
            //Full House

            if (scoreCard.FullHouseValidation(dice))
            {
                return "Full House";
            }

            //Two Pair

            foreach (int count in dieCount)
            {
                if (count == 4)
                {
                    return "Two Pair";
                }
            }

            //Large Straight

            if (scoreCard.LargeStraightValidation(dice))
            {
                return "Large Straight";
            }

            //Small Straight

            if (scoreCard.SmallStraightValidation(dice))
            {
                return "Small Straight";
            }

            //Pair

            foreach (int count in dieCount)
            {
                if (count == 2)
                {
                    return "Pair";
                }
            }

            //Three Straight

            return "Three Straight";
        }
    }
}