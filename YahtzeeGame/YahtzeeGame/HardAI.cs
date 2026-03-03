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

        public bool DecisionTree(int[] dice)
        {
            //If Yahtzee, lock in.

            if (this.PlayerScores.YahtzeeValidation(dice))
            {
                if (!this.PlayerScores.yahtzeeScored)
                {
                    //score yahtzee
                    return true;
                }
            }
            
            //If Large Straight, lock in

            if (this.PlayerScores.LargeStraightValidation(dice))
            {
                if (!this.PlayerScores.LargeStraightValidation(dice))
                {
                    //score Large Straight
                    return true;
                }
            }

            //If Small straight, roll for Large Straight. If Large Straight locked in, lock in Small Straight

            if (this.PlayerScores.SmallStraightValidation(dice))
            {
                //preserve small straight and roll again if not on 3rd roll

                //if on 3rd roll
                if (!this.PlayerScores.SmallStraightValidation(dice))
                {
                    //score Small Straight
                    return true;
                }
            }
            //If Four of a Kind rolled, roll for Yahtzee.If Yahtzee is locked in, lock in for 4s, 5s or 6s. If 4, 5 or 6 is already locked in, lock in for Four of a Kind.

            if (this.PlayerScores.FourKindValidation(dice))
            {
                //Check if it is 4s, 5s, or 6s

                //if true, lock in
                //else, preserve four of kind and roll again if not on 3rd roll

                //if on 3rd roll
                if (!this.PlayerScores.fourOfAKindScored)
                {
                    //score Four of a Kind
                    return true;
                }
            }

            if (this.PlayerScores.FullHouseValidation(dice))
            {
                if (!this.PlayerScores.fullHouseScored)
                {
                    //score Full House
                    return true;
                }
            }

            //If Three of a Kind is rolled, roll for Four of a Kind. If Four of a Kind is Locked in, lock in Three of a Kind if 4, 5, or 6. If not, roll for Full House.

            if (this.PlayerScores.ThreeKindValidation(dice))
            {
                //Check if Four of a Kind is Scored

                //if true, lock in if 4, 5, or 6
                //else, preserve three of a kind and roll again if not 3rd roll 

                //else, If on 3rd roll
                if (!this.PlayerScores.threeOfAKindScored)
                {
                    //score three of a kind
                    return true;
                }
            }

            //If Two Pairs, roll for Full House. If it doesn’t work, mark lowest pair or Chance if total is more than 15

            int[] count = this.PlayerScores.DieCounter(dice);
            int pairTracker = 0;

            foreach (int pip in count)
            {
                if (pip == 2)
                {
                    pairTracker++;
                }
            }

            if (pairTracker == 2)
            {
                if (this.PlayerScores.FullHouseValidation(dice))
                {
                    if (!this.PlayerScores.fullHouseScored)
                    //score full house
                    return true;
                }
                else if(!this.PlayerScores.chanceScored && this.PlayerScores.chance > 15)
                {
                    //score chance
                    return true;
                }
                else
                {
                    //score lowest pair
                    return true;
                }
            }

            //If Pair is rolled, roll for Small Straight. If Small Straight is Locked in, roll for Large Straight. If Large Straight is Locked in, roll for Full House.
            //If Full House Locked in, roll for Yahtzee. If Yahtzee is locked in, roll for three of a Kind if 4, 5, 6. If 1, 2, 3, roll for more if not locked in. 


            if (pairTracker == 1)
            {
                //if not on third roll and Small Straight not locked in, preserve pair and roll for Small Straight
                //if not on third roll and Small Straight locked in, preserve pair and roll for Large Straight
                //If not on third roll and large straight locked in, preserve pair and roll for a Full House
                //If not on third Roll and Full House Locked in, preserve pair and roll for Yahtzee
                //if not on third roll and yahtzee locked in, preserve pair and roll for three of kind if 4, 5, 6
                //if not on third roll and is 1, 2, or 3 pair, roll for more of that pair if not locked in upper section
            }
            

            //If nothing is rolled, because both straights are already marked, look for highest unscored upper section and roll for pairs of that.
            

            //In case of roll 3, 0 points, Least to most important boxes to mark: Aces, Twos, Chance, Threes, Fours, 3 of a Kind, Full House, Fives, Four of a Kind, Sixes, Small straight,
            //Large Straight, Yahtzee

            MessageBox.Show("No decision reached, you fucked up. The current die pool is " + dice);
            return true;
        }




    }
}
