using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YahtzeeGame
{
    public class ScoreCard
    {
        //TODO: Add logic to the lower section boxes that will check if it's valid to score in those boxes with the dice the player has.
        //TODO: Put Try-Catches into the scoring methods

        //Fields Notes
        /*
         * Quick note about the fields, each variable is public and can be changed without affecting totalScore. So you can call scores 
         * from previously filled in boxes for displaying to the player and other UI elements. fullHouse, smallStraight, largeStraight,
         * and yahtzee are public Readonly because their point total isn't affected by the dice, but can still be called if needed.
         */

        #region Fields
        //These are flags that keep track of whether a score has been locked in
        public bool acesScored;
        public bool twosScored;
        public bool threesScored;
        public bool foursScored;
        public bool fivesScored;
        public bool sixesScored;
        public bool threeOfAKindScored;
        public bool fourOfAKindScored;
        public bool fullHouseScored;
        public bool smallStraightScored;
        public bool largeStraightScored;
        public bool yahtzeeScored;
        public bool chanceScored;

        //These hold the actual values that will be added to totalScore when locked in. These are all separate values so that they can be called later for
        //displaying on the game card. If we find a better way of doing it, these can be erased.
        public int aces;
        public int twos;
        public int threes;
        public int fours;
        public int fives;
        public int sixes;
        public int threeOfAKind;
        public int fourOfAKind;
        public readonly int fullHouse;
        public readonly int smallStraight;
        public readonly int largeStraight;
        public readonly int yahtzee;
        public int chance;

        //This is the final score of the player
        public int totalScore;
        #endregion

        //Constructor
        public ScoreCard()
        {
            acesScored = false;
            twosScored = false;
            threesScored = false;
            foursScored = false;
            fivesScored = false;
            sixesScored = false;
            threeOfAKindScored = false;
            fourOfAKindScored = false;
            fullHouseScored = false;
            smallStraightScored = false;
            largeStraightScored = false;
            yahtzeeScored = false;
            chanceScored = false;
            aces = 0;
            twos = 0;
            threes = 0;
            fours = 0;
            fives = 0;
            sixes = 0;
            threeOfAKind = 0;
            fourOfAKind = 0;
            fullHouse = 25;
            smallStraight = 30;
            largeStraight = 40;
            yahtzee = 50;
            chance = 0;
            totalScore = 0;
        }

        //Scoring Methods Notes
        /* 
         * These are all really similar, so I'm documenting them as a region because I'm lazy.
         
         * Each Method is intended to be called by a click event, when the player clicks on a box to score.
         * Just slide in the right Method and ScoreCard will first check to see if the that box is already scored by
         * checking the associated Bool flag. If not, it adds all the appropriate dice together for the score and 
         * then prompts the player with a message box if they're sure they want to perform that action while displaying 
         * the points they'll get. If they select yes, the flag gets flipped to true and the score gets added to totalScore.
         * If the flag was already set to true, then a message box tells the player that the box has already been scored and
         * for the amount of points logged.
         
         * Note that when you call any method besides the ones for Full House, Small Straight, Large Straight, or Yahtzee, 
         * you'll need to feed it an array with all five dice. It won't throw an error, but the logic won't work right.
         */
        #region Scoring Methods
        public void AcesSelected(int[] Dice)
        {
            if (acesScored == false)
            {
                foreach (int Die in Dice)
                {
                    if (Die == 1) { aces += Die; }
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Aces? You will gain " + aces + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    acesScored = true;
                    totalScore += aces;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + aces + " points."); }
        }

        public void TwosSelected(int[] Dice)
        {
            if (twosScored == false)
            {
                foreach (int Die in Dice)
                {
                    if (Die == 2) { twos += Die; }
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Twos? You will gain " + twos + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    twosScored = true;
                    totalScore += twos;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + twos + " points."); }
        }

        public void ThreesSelected(int[] Dice)
        {
            if (threesScored == false)
            {
                foreach (int Die in Dice)
                {
                    if (Die == 3) { threes += Die; }
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Threes? You will gain " + threes + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    threesScored = true;
                    totalScore += threes;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + threes + " points."); }
        }

        public void FoursSelected(int[] Dice)
        {
            if (foursScored == false)
            {
                foreach (int Die in Dice)
                {
                    if (Die == 4) { fours += Die; }
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Fours? You will gain " + fours + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    foursScored = true;
                    totalScore += fours;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + fours + " points."); }
        }

        public void FivesSelected(int[] Dice)
        {
            if (fivesScored == false)
            {
                foreach (int Die in Dice)
                {
                    if (Die == 5) { fives += Die; }
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Fives? You will gain " + fives + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    fivesScored = true;
                    totalScore += fives;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + fives + " points."); }
        }

        public void SixesSelected(int[] Dice)
        {
            if (sixesScored == false)
            {
                foreach (int Die in Dice)
                {
                    if (Die == 6) { sixes += Die; }
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Sixes? You will gain " + sixes + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    sixesScored = true;
                    totalScore += sixes;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + sixes + " points."); }
        }

        public void ThreeOfAKindSelected(int[] Dice)
        {
            if (threeOfAKindScored == false)
            {
                foreach (int Die in Dice)
                {
                    threeOfAKind += Die;
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Three of a Kind? You will gain "
                    + threeOfAKind + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    threeOfAKindScored = true;
                    totalScore += threeOfAKind;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + threeOfAKind + " points."); }
        }

        public void FourOfAKindSelected(int[] Dice)
        {
            if (fourOfAKindScored == false)
            {
                foreach (int Die in Dice)
                {
                    fourOfAKind += Die;
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Four of a Kind? You will gain "
                    + fourOfAKind + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    fourOfAKindScored = true;
                    totalScore += fourOfAKind;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + fourOfAKind + " points."); }
        }

        public void FullHouseSelected()
        {
            if (fullHouseScored == false)
            {
                MessageBoxResult choice = MessageBox.Show("Do you want to score Full House? You will gain "
                    + fullHouse + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    fullHouseScored = true;
                    totalScore += fullHouse;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + fullHouse + " points."); }
        }

        public void SmallStraightSelected()
        {
            if (smallStraightScored == false)
            {
                MessageBoxResult choice = MessageBox.Show("Do you want to score Small Straight? You will gain "
                    + smallStraight + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    smallStraightScored = true;
                    totalScore += smallStraight;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + smallStraight + " points."); }
        }

        public void LargeStraightSelected()
        {
            if (largeStraightScored == false)
            {
                MessageBoxResult choice = MessageBox.Show("Do you want to score Large Straight? You will gain "
                    + largeStraight + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    largeStraightScored = true;
                    totalScore += largeStraight;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + largeStraight + " points."); }
        }

        public void YahtzeeSelected()
        {
            if (yahtzeeScored == false)
            {
                MessageBoxResult choice = MessageBox.Show("Do you want to score Yahtzee? You will gain "
                    + yahtzee + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    yahtzeeScored = true;
                    totalScore += yahtzee;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + yahtzee + " points."); }
        }

        public void ChanceSelected(int[] Dice)
        {
            if (chanceScored == false)
            {
                foreach (int Die in Dice)
                {
                    chance += Die;
                }

                MessageBoxResult choice = MessageBox.Show("Do you want to score Chance? You will gain "
                    + chance + " points.", "Confirmation", MessageBoxButton.YesNo);

                if (choice == MessageBoxResult.Yes)
                {
                    chanceScored = true;
                    totalScore += chance;
                }
            }
            else { MessageBox.Show("This box has already been scored. It contains " + chance + " points."); }
        }
        #endregion
    }
}