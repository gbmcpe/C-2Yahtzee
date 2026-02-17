using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YahtzeeGame
{
    public class ScoreCard
    {
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

        //This flag triggers when the entire scorecard is finished. Further attempts to flip one of the other flags will fire off ScoreCardFilledWarning()
        public bool isScoreCardFinished;

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
            isScoreCardFinished = false;
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

        //Card Checking Methods
        /*
         * These checks are run at the start and end of every Scoring Method. First an If statement runs ScoreCardNotFinished. By default, it returns True.
         * It loads all the current flags into a list for a convenient foreach Loop that increments a variable if that flag is true. If all 13 are true,
         * the method returns a false instead, which prevents the Scoring Method from being run.
         *
         * If the Scoring Method is not run, an Else statement fires off ScoreCardFilled. This current holds a generic popup message, but can be filled
         * with code to throw a flag or something else later. 
         */

        #region Checking Methods

        public bool ScoreCardNotFinished()
        {
            bool result = true;
            int x = 0;

            List<bool> scores = new List<bool>()
            {
                acesScored, twosScored, threesScored,
                foursScored, fivesScored, sixesScored,
                threeOfAKindScored, fourOfAKindScored, fullHouseScored,
                smallStraightScored, largeStraightScored, yahtzeeScored,
                chanceScored
            };

            foreach (bool s in scores)
            {
                if (s == true)
                {
                    x++;
                }
            }

            if (x == 13)
            {
                result = false;
            }

            return result;
        }

        public void ScoreCardFilled()
        {
            MessageBox.Show("The card has already been filled. Your game is over. You scored " + totalScore + "points");
        }

        #endregion
        
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
         *
         * Update 2: Each Scoring Method now checks if the Score Card has been filled. If not, it fires off ScoreCardFilled()
         */

        #region Scoring Methods
        public void AcesSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (acesScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        if (Die == 1)
                        {
                            aces += Die;
                        }
                    }

                    MessageBoxResult choice =
                        MessageBox.Show("Do you want to score Aces? You will gain " + aces + " points.", "Confirmation",
                            MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        acesScored = true;
                        totalScore += aces;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + aces + " points.");
                }
            }
            else
            {
                ScoreCardFilled();
            }
        }

        public void TwosSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (twosScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        if (Die == 2)
                        {
                            twos += Die;
                        }
                    }

                    MessageBoxResult choice =
                        MessageBox.Show("Do you want to score Twos? You will gain " + twos + " points.", "Confirmation",
                            MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        twosScored = true;
                        totalScore += twos;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + twos + " points.");
                }

            }
            else { ScoreCardFilled(); }
}

        public void ThreesSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (threesScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        if (Die == 3)
                        {
                            threes += Die;
                        }
                    }

                    MessageBoxResult choice =
                        MessageBox.Show("Do you want to score Threes? You will gain " + threes + " points.",
                            "Confirmation", MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        threesScored = true;
                        totalScore += threes;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + threes + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void FoursSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (foursScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        if (Die == 4)
                        {
                            fours += Die;
                        }
                    }

                    MessageBoxResult choice =
                        MessageBox.Show("Do you want to score Fours? You will gain " + fours + " points.",
                            "Confirmation", MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        foursScored = true;
                        totalScore += fours;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + fours + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void FivesSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (fivesScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        if (Die == 5)
                        {
                            fives += Die;
                        }
                    }

                    MessageBoxResult choice =
                        MessageBox.Show("Do you want to score Fives? You will gain " + fives + " points.",
                            "Confirmation", MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        fivesScored = true;
                        totalScore += fives;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + fives + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void SixesSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (sixesScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        if (Die == 6)
                        {
                            sixes += Die;
                        }
                    }

                    MessageBoxResult choice =
                        MessageBox.Show("Do you want to score Sixes? You will gain " + sixes + " points.",
                            "Confirmation", MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        sixesScored = true;
                        totalScore += sixes;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + sixes + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void ThreeOfAKindSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (threeOfAKindScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        threeOfAKind += Die;
                    }

                    MessageBoxResult choice = MessageBox.Show("Do you want to score Three of a Kind? You will gain "
                                                              + threeOfAKind + " points.", "Confirmation",
                        MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        threeOfAKindScored = true;
                        totalScore += threeOfAKind;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + threeOfAKind + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void FourOfAKindSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (fourOfAKindScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        fourOfAKind += Die;
                    }

                    MessageBoxResult choice = MessageBox.Show("Do you want to score Four of a Kind? You will gain "
                                                              + fourOfAKind + " points.", "Confirmation",
                        MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        fourOfAKindScored = true;
                        totalScore += fourOfAKind;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + fourOfAKind + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void FullHouseSelected()
        {
            if (ScoreCardNotFinished())
            {
                if (fullHouseScored == false)
                {
                    MessageBoxResult choice = MessageBox.Show("Do you want to score Full House? You will gain "
                                                              + fullHouse + " points.", "Confirmation",
                        MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        fullHouseScored = true;
                        totalScore += fullHouse;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + fullHouse + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void SmallStraightSelected()
        {
            if (ScoreCardNotFinished())
            {
                if (smallStraightScored == false)
                {
                    MessageBoxResult choice = MessageBox.Show("Do you want to score Small Straight? You will gain "
                                                              + smallStraight + " points.", "Confirmation",
                        MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        smallStraightScored = true;
                        totalScore += smallStraight;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + smallStraight + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void LargeStraightSelected()
        {
            if (ScoreCardNotFinished())
            {
                if (largeStraightScored == false)
                {
                    MessageBoxResult choice = MessageBox.Show("Do you want to score Large Straight? You will gain "
                                                              + largeStraight + " points.", "Confirmation",
                        MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        largeStraightScored = true;
                        totalScore += largeStraight;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + largeStraight + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void YahtzeeSelected()
        {
            if (ScoreCardNotFinished())
            {
                if (yahtzeeScored == false)
                {
                    MessageBoxResult choice = MessageBox.Show("Do you want to score Yahtzee? You will gain "
                                                              + yahtzee + " points.", "Confirmation",
                        MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        yahtzeeScored = true;
                        totalScore += yahtzee;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + yahtzee + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void ChanceSelected(int[] Dice)
        {
            if (ScoreCardNotFinished())
            {
                if (chanceScored == false)
                {
                    foreach (int Die in Dice)
                    {
                        chance += Die;
                    }

                    MessageBoxResult choice = MessageBox.Show("Do you want to score Chance? You will gain "
                                                              + chance + " points.", "Confirmation",
                        MessageBoxButton.YesNo);

                    if (choice == MessageBoxResult.Yes)
                    {
                        chanceScored = true;
                        totalScore += chance;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + chance + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }
        #endregion
    }
}