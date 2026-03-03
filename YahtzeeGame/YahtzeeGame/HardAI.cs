using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace YahtzeeGame
{
    public class HardAI : Player
    {

        //Constructor
        public HardAI(int Pos, string Name, bool Comp) : base(Pos, Name, Comp)
        {
        }

        private int PairCounter(int[] count)
        {
            int tracker = 0;
            foreach (int pip in count)
            {
                if (pip == 2)
                {
                    tracker++;
                }
            }

            return tracker;
        }

        private bool YahtzeeCheck(int[] dice)
        {
            bool scored = false;

            if (this.PlayerScores.YahtzeeValidation(dice))
            {
                if (!this.PlayerScores.yahtzeeScored)
                {
                    this.PlayerScores.YahtzeeSelected(dice, true);
                    scored = true;
                }
            }
            return scored;
        }

        private bool LargeStraightCheck(int[] dice)
        {
            bool scored = false;
            if (this.PlayerScores.LargeStraightValidation(dice))
            {
                if (!this.PlayerScores.LargeStraightValidation(dice))
                {
                    this.PlayerScores.LargeStraightSelected(dice, true);
                    scored = true;
                }
            }
            return scored;
        }

        private int SmallStraightCheck(int[] dice, int rollNumber)
        {
            // 0 = move to next case, 1 = scored, 2 = lock in dice and reroll
            int result = 0;
            if (this.PlayerScores.smallStraightScored)
            {
                if (!this.PlayerScores.smallStraightScored && rollNumber == 0)
                {
                    this.PlayerScores.SmallStraightSelected(dice, true);
                    result = 1;
                }
                else if (!this.PlayerScores.smallStraightScored && rollNumber > 0)
                {
                    result = 2;
                }
            }
            return result;
        }

        private int FourKindCheck(int[] dice, int rollNumber, int[] count)
        {
            //0 = move to next case, 1 = scored, 2 = lock in dice and reroll
            int result = 0;
            if (this.PlayerScores.FourKindValidation(dice))
            {
                if (!this.PlayerScores.fourOfAKindScored && rollNumber == 0)
                {
                    this.PlayerScores.FourOfAKindSelected(dice, true);
                    result = 1;
                }
                else if (count[5] == 4 || count[4] == 4 || count[3] == 4)
                {
                    this.PlayerScores.FourOfAKindSelected(dice, true);
                    result = 1;
                }
                else
                {
                    result = 2;
                }
            }
            return result;
        }

        private int ThreeKindCheck(int[] dice, int rollNumber, int[] count)
        {
            // 0 = move to next case, 1 = scored, 2 = lock in dice and reroll
            int result = 0;

            if (this.PlayerScores.ThreeKindValidation(dice))
            {

                //Check if Four of a Kind is Scored

                //if true, lock in if 4, 5, or 6
                //else, preserve three of a kind and roll again if not 3rd roll 

                if (!this.PlayerScores.threeOfAKindScored && rollNumber == 0)
                {
                    this.PlayerScores.ThreeOfAKindSelected(dice, true);
                    result = 1;
                }
                else if (!this.PlayerScores.fourOfAKindScored && (count[5] == 3 || count[4] == 3 || count[3] == 3))
                {
                    result = 2;
                }
                
            }
            return result;
        }
        public bool[] DecisionTree(int[] dice, int rollNumber)
        {
            int[] count = this.PlayerScores.DieCounter(dice);
            int pairCount = PairCounter(count);
            bool[] heldDice = new [] {false, false, false, false, false};
            int result = 0;

            //If Yahtzee, lock in.
            if (YahtzeeCheck(dice)) { return heldDice; }

            //If Large Straight, lock in
            if (LargeStraightCheck(dice)) { return heldDice; }
            
            //If Small straight, roll for Large Straight. If Large Straight locked in, lock in Small Straight
            result = SmallStraightCheck(dice, rollNumber);

            if (result == 1)
            {
                return heldDice;
            }

            if (result == 2)
            {
                //heldDice = SmallStraightDiceHold(dice)
                return heldDice;
            }

            //If Four of a Kind rolled, roll for Yahtzee.If Yahtzee is locked in, lock in for 4s, 5s or 6s. If 4, 5 or 6 is already locked in, lock in for Four of a Kind.
            result = FourKindCheck(dice, rollNumber, count);

            if (result == 1)
            {
                return heldDice;
            }

            if (result == 2)
            {
                //heldDice = FourKindDiceHold(dice)
                return heldDice;
            }

            //If Three of a Kind is rolled, roll for Four of a Kind. If Four of a Kind is Locked in, lock in Three of a Kind if 4, 5, or 6. If not, roll for Full House.

            result = ThreeKindCheck(dice, rollNumber, count);

            

            //If Two Pairs, roll for Full House. If it doesn’t work, mark lowest pair or Chance if total is more than 15
            
            if (pairCount == 2)
            {
                if (this.PlayerScores.FullHouseValidation(dice))
                {
                    if (!this.PlayerScores.fullHouseScored && rollNumber == 0)
                    {
                        this.PlayerScores.FullHouseSelected(dice, true);
                    }
                    return heldDice;
                }
                else if(!this.PlayerScores.chanceScored && this.PlayerScores.chance > 15 && rollNumber == 0)
                {
                    this.PlayerScores.ChanceSelected(dice, true);
                    return heldDice;
                }
                else if (rollNumber == 0)
                {
                    
                    if (!this.PlayerScores.acesScored)
                    {
                        this.PlayerScores.AcesSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.twosScored)
                    {
                        this.PlayerScores.TwosSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.threesScored)
                    {
                        this.PlayerScores.ThreesSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.foursScored)
                    {
                        this.PlayerScores.FoursSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.fivesScored)
                    {
                        this.PlayerScores.FivesSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.sixesScored)
                    {
                        this.PlayerScores.SixesSelected(dice, true);
                        return heldDice;
                    }
                }
                else //rollTurn != 0
                {
                    //preserve two pairs and roll again
                    return heldDice;
                }
            }

            //If Pair is rolled, roll for Small Straight. If Small Straight is Locked in, roll for Large Straight. If Large Straight is Locked in, roll for Full House.
            //If Full House Locked in, roll for Yahtzee. If Yahtzee is locked in, roll for three of a Kind if 4, 5, 6. If 1, 2, 3, roll for more if not locked in. 


            if (pairCount == 1)
            {
                //preserve pair

                if (rollNumber == 0)
                {
                    if (!this.PlayerScores.acesScored)
                    {
                        this.PlayerScores.AcesSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.twosScored)
                    {
                        this.PlayerScores.TwosSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.threesScored)
                    {
                        this.PlayerScores.ThreesSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.foursScored)
                    {
                        this.PlayerScores.FoursSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.fivesScored)
                    {
                        this.PlayerScores.FivesSelected(dice, true);
                        return heldDice;
                    }
                    else if (!this.PlayerScores.sixesScored)
                    {
                        this.PlayerScores.SixesSelected(dice, true);
                        return heldDice;
                    }
                }
                else
                {
                    return heldDice;
                }
            }
            

            //If nothing is rolled, because both straights are already marked, look for highest unscored upper section and roll for pairs of that.

            if (this.PlayerScores.smallStraightScored && this.PlayerScores.largeStraightScored && rollNumber != 0)
            {
                if (!this.PlayerScores.sixesScored)
                {
                    //preserve six
                    return heldDice;
                }
                else if (!this.PlayerScores.fivesScored)
                {
                    //preserve five
                    return heldDice;
                }
                else if (!this.PlayerScores.foursScored)
                {
                    //preserve four
                    return heldDice;
                }
                else if (!this.PlayerScores.threesScored)
                {
                    //preserve three
                    return heldDice;
                }
                else if (!this.PlayerScores.twosScored)
                {
                    //preserve two
                    return heldDice;
                }
                else if (!this.PlayerScores.acesScored)
                {
                    //preserve ace
                    return heldDice;
                }
            }

            //In case of roll 3, 0 points, Least to most important boxes to mark: Aces, Twos, Chance, Threes, Fours, 3 of a Kind, Full House, Fives, Four of a Kind, Sixes, Small straight,
            //Large Straight, Yahtzee

            if (!this.PlayerScores.ScoreCardNotFinished() && rollNumber == 0)
            {
                if (!this.PlayerScores.acesScored)
                {
                    this.PlayerScores.AcesSelected(dice, true);
                }
                else if (!this.PlayerScores.twosScored)
                {
                    this.PlayerScores.TwosSelected(dice, true);
                }
                else if (!this.PlayerScores.chanceScored)
                {
                    this.PlayerScores.ChanceSelected(dice, true);
                }
                else if (!this.PlayerScores.threesScored)
                {
                    this.PlayerScores.ThreesSelected(dice, true);
                }
                else if (!this.PlayerScores.foursScored)
                {
                    this.PlayerScores.FoursSelected(dice, true);
                }
                else if (!this.PlayerScores.threeOfAKindScored)
                {
                    this.PlayerScores.ThreeOfAKindSelected(dice, true);
                }
                else if (!this.PlayerScores.fullHouseScored)
                {
                    this.PlayerScores.FullHouseSelected(dice, true);
                }
                else if (!this.PlayerScores.fivesScored)
                {
                    this.PlayerScores.FivesSelected(dice, true);
                }
                else if (!this.PlayerScores.fourOfAKindScored)
                {
                    this.PlayerScores.FoursSelected(dice, true);
                }
                else if (!this.PlayerScores.sixesScored)
                {
                    this.PlayerScores.SixesSelected(dice, true);
                }
                else if (!this.PlayerScores.smallStraightScored)
                {
                    this.PlayerScores.SmallStraightSelected(dice, true);
                }
                else if (!this.PlayerScores.largeStraightScored)
                {
                    this.PlayerScores.LargeStraightSelected(dice, true);
                }
                else if (!this.PlayerScores.yahtzeeScored)
                {
                    this.PlayerScores.YahtzeeSelected(dice, true);
                }

                return heldDice;
            }

            MessageBox.Show("No decision reached, you fucked up. The current die pool is " + dice);
            return heldDice;
        }




    }
}
