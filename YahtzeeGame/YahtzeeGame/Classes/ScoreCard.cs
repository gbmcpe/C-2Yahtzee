using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YahtzeeGame
{
    //Adding an interface for property changed so that we can bind the scorecard to the UI and have it update
    //when the values change.
    public class ScoreCard : INotifyPropertyChanged
    {
        /*Fields Notes
         * Quick note about the fields, each variable is public and can be changed without affecting totalScore. So you can call scores 
         * from previously filled in boxes for displaying to the player and other UI elements. fullHouse, smallStraight, largeStraight,
         * and yahtzee are public Readonly because their point total isn't affected by the dice, but can still be called if needed.
         */
        #region Fields
        //These are flags that keep track of whether a score has been locked in
        private bool acesScored;
        private bool twosScored;
        private bool threesScored;
        private bool foursScored;
        private bool fivesScored;
        private bool sixesScored;
        private bool bonusScored;
        private bool threeOfAKindScored;
        private bool fourOfAKindScored;
        private bool fullHouseScored;
        private bool smallStraightScored;
        private bool largeStraightScored;
        private bool yahtzeeScored;
        private bool chanceScored;

        //This flag triggers when the entire scorecard is finished. Further attempts to flip one of the other flags will fire off ScoreCardFilledWarning()
        public bool isScoreCardFinished;

        //These hold the actual values that will be added to totalScore when locked in. These are all separate values so that they can be called later for
        //displaying on the game card. 
        private int aces;
        private int twos;
        private int threes;
        private int fours;
        private int fives;
        private int sixes;
        private int bonus;
        private int threeOfAKind;
        private int fourOfAKind;
        private int fullHouse;
        private int smallStraight;
        private int largeStraight;
        private int yahtzee;
        private int chance;

        //This is the final score of the player
        private int totalScore;

        //This is the event handler for the INotifyPropertyChanged interface,
        //which allows us to bind the scorecard to the UI and have it update when values change.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This method is called whenever a property changes, and it raises the PropertyChanged event to notify the UI to update.
        /// The propertyName parameter is the name of the property that changed, which is used by the UI to determine which element to update.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// These are the properties for the score values. They're all public so they can be called by the UI, but they don't affect totalScore when changed. 
        //* They're only changed when the player locks in a score, which is when the associated flag is flipped to true. This way, we can call these values
        //* for display purposes without affecting the actual score of the player.
        /// </summary>
        public int Aces
        {
            get => aces;
            set
            {
                aces = value;
                OnPropertyChanged(nameof(Aces));
            }
        }
        public int Twos
        {
            get => twos;
            set
            {
                twos = value;
                OnPropertyChanged(nameof(Twos));
            }
        }
        public int Threes { 
            get => threes;  
            set
            {
                threes = value;
                OnPropertyChanged(nameof(threes));
            }
        }
        public int Fours
        {
            get => fours;
            set
            {
                fours = value;
                OnPropertyChanged(nameof(Fours));
            }
        }
        public int Fives
        {
            get => fives;
            set
            {
                fives = value;
                OnPropertyChanged(nameof(Fives));
            }
        }
        public int Sixes
        {
            get => sixes;
            set
            {
                sixes = value;
                OnPropertyChanged(nameof(Sixes));
            }
        }
        public int Bonus
        {
            get => bonus;
            set
            {
                bonus = value;
                OnPropertyChanged(nameof(Bonus));
            }
        }

        public int ThreeOfAKind
        {
            get => threeOfAKind;
            set
            {
                threeOfAKind = value;
                OnPropertyChanged(nameof(ThreeOfAKind));
            }
        }

        public int FourOfAKind
        {
            get => fourOfAKind;
            set
            {
                fourOfAKind = value;
                OnPropertyChanged(nameof(FourOfAKind));
            }
        }

        public int FullHouse
        {
            get => fullHouse;
            set
            {
                fullHouse = value;
                OnPropertyChanged(nameof(FullHouse));
            }
        }

        public int SmallStraight
        {
            get => smallStraight;
            set
            {
                smallStraight = value;
                OnPropertyChanged(nameof(SmallStraight));
            }
        }

        public int LargeStraight
        {
            get => largeStraight;
            set
            {
                largeStraight = value;
                OnPropertyChanged(nameof(LargeStraight));
            }
        }

        public int Yahtzee
        {
            get => yahtzee;
            set
            {
                yahtzee = value;
                OnPropertyChanged(nameof(Yahtzee));
            }
        }

        public int Chance
        {
            get => chance;
            set
            {
                chance = value;
                OnPropertyChanged(nameof(Chance));
            }
        }

        public int TotalScore
        {
            get => totalScore;
            set
            {
                totalScore = value;
                OnPropertyChanged(nameof(TotalScore));
            }
        }

        public bool AcesScored
        {
            get => acesScored;
            set
            {
                acesScored = value;
                OnPropertyChanged(nameof(AcesScored));
            }
        }

        public bool TwosScored
        {
            get => twosScored;
            set
            {
                twosScored = value;
                OnPropertyChanged(nameof(TwosScored));
            }
        }
        public bool ThreesScored
        {
            get => threesScored;
            set
            {
                threesScored = value;
                OnPropertyChanged(nameof(ThreesScored));
            }
        }
        public bool FoursScored 
        { 
            get => foursScored;
            set
            {
                foursScored = value;
                OnPropertyChanged(nameof(FoursScored));
            }
        }
        public bool FivesScored
        {
            get => fivesScored;
            set
            {
                fivesScored = value;
                OnPropertyChanged(nameof(FivesScored));
            }
        }
        public bool SixesScored
        {
            get => sixesScored;
            set
            {
                sixesScored = value;
                OnPropertyChanged(nameof(SixesScored));
            }
        }
        public bool BonusScored 
        { 
            get => bonusScored;
            set
            {
                bonusScored = value;
                OnPropertyChanged(nameof(BonusScored));
            }
        }
        public bool ThreeOfAKindScored 
        { 
            get => threeOfAKindScored;
            set
            {
                threeOfAKindScored = value;
                OnPropertyChanged(nameof(ThreeOfAKindScored));
            }
        }
        public bool FourOfAKindScored
        {
            get => fourOfAKindScored;
            set
            {
                fourOfAKindScored = value;
                OnPropertyChanged(nameof(FourOfAKindScored));
            }
        }
        public bool FullHouseScored
        {
            get => fullHouseScored;
            set
            {
                fullHouseScored = value;
                OnPropertyChanged(nameof(FullHouseScored));
            }
        }
        public bool SmallStraightScored 
        { 
            get => smallStraightScored;
            set
            {
                smallStraightScored = value;
                OnPropertyChanged(nameof(SmallStraightScored));
            }
        }
        public bool LargeStraightScored
        {
            get => largeStraightScored;
            set
            {
                largeStraightScored = value;
                OnPropertyChanged(nameof(LargeStraightScored));
            }
        }
        public bool YahtzeeScored 
        {
            get => yahtzeeScored;
            set
            {
                yahtzeeScored = value;
                OnPropertyChanged(nameof(YahtzeeScored));
            }
        }
        public bool ChanceScored 
        { 
            get => chanceScored;
            set
            {
                chanceScored = value;
                OnPropertyChanged(nameof(ChanceScored));
            }
        }

        

        #endregion

        //Constructor
        public ScoreCard()
        {
            AcesScored = false;
            TwosScored = false;
            ThreesScored = false;
            FoursScored = false;
            FivesScored = false;
            SixesScored = false;
            ThreeOfAKindScored = false;
            FourOfAKindScored = false;
            FullHouseScored = false;
            SmallStraightScored = false;
            LargeStraightScored = false;
            YahtzeeScored = false;
            ChanceScored = false;
            isScoreCardFinished = false;
            Aces = 0;
            Twos = 0;
            Threes = 0;
            Fours = 0;
            Fives = 0;
            Sixes = 0;
            Bonus = 0;
            ThreeOfAKind = 0;
            FourOfAKind = 0;
            FullHouse = 0;
            SmallStraight = 0;
            LargeStraight = 0;
            Yahtzee = 0;
            Chance = 0;
            TotalScore = 0;
        }

        /*Card Checking Methods
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
                AcesScored, TwosScored, ThreesScored,
                FoursScored, FivesScored, SixesScored,
                ThreeOfAKindScored, FourOfAKindScored, FullHouseScored,
                SmallStraightScored, LargeStraightScored, YahtzeeScored,
                ChanceScored
            };

            //Checks each score flag and increments x by 1
            foreach (bool s in scores)
            {
                if (s == true)
                {
                    x++;
                }
            }

            //if x was incremented 13 times, which means all 13 flags are thrown, the card is marked as finished.
            if (x == 13)
            {
                result = false;
            }

            return result;
        }

        public void ScoreCardFilled()
        {
            MessageBox.Show("The card has already been filled. Your game is over. You scored " + TotalScore + "points");
        }

        /* DieCounter Note
         * Generates an array where each position represents how many of each die face is present.
         * Ex: The dice are currently showing 1, 2, 2, 3, 5. DieCounter will return [1, 2, 1, 0, 1, 0]
         */
        public int[] DieCounter(int[] dice)
        {
            int[] result = new int[6];

            foreach (int die in dice)
            {
                if (die == 1) { result[0] += 1; }
                if (die == 2) { result[1] += 1; }
                if (die == 3) { result[2] += 1; }
                if (die == 4) { result[3] += 1; }
                if (die == 5) { result[4] += 1; }
                if (die == 6) { result[5] += 1; }
            }

            return result;
        }

        // These methods check to see if the current dice are a valid combination for the Lower Section Boxes. 
        
        public bool ThreeKindValidation(int[] dice)
        {
            int[] dieCount = DieCounter(dice);

            //Checks if there are three of any die type in the current hand
            foreach (int count in dieCount)
            {
                if (count >= 3)
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool FourKindValidation(int[] dice)
        {
            int[] dieCount = DieCounter(dice);

            //Does the same thing as ThreeKindValidation
            foreach (int count in dieCount)
            {
                if (count >= 4)
                {
                    return true;
                }
            }

            return false;
        }

        public bool FullHouseValidation(int[] dice)
        {
            int[] dieCount = DieCounter(dice);
            int checker = 0;
            bool threeCheckFlag = false;
            bool twoCheckFlag = false;
            
            //Checks to see if there is a pair and three of a kind by incrementing Checker when found
            //The bools are for data integrity, to make sure multiple pairs don't cause an false positive.
            foreach (int count in dieCount)
            {
                if (count == 3 && !threeCheckFlag) { checker++; threeCheckFlag = true; }
                if (count == 2 && !twoCheckFlag) { checker++; twoCheckFlag = true; }
            }

            if (checker == 2)
            {
                return true;
            } 
            
            return false;
        }

        public bool SmallStraightValidation(int[] dice)
        {
            int[] count = DieCounter(dice);

            //Checks to see if there is a line of 1s in the array, at that represents a straight
            if (count[0] >= 1 && count[1] >= 1 && count[2] >= 1 && count[3] >= 1 ||
                count[1] >= 1 && count[2] >= 1 && count[3] >= 1 && count[4] >= 1 ||
                count[2] >= 1 && count[3] >= 1 && count[4] >= 1 && count[5] >= 1)
            {
                return true;
            }
            
            return false; 
        }

        public bool LargeStraightValidation(int[] dice)
        {
            int[] count = DieCounter(dice);

            //This is the same thing as the Small Straight validator
            if (count[0] == 1 && count[1] == 1 && count[2] == 1 && count[3] == 1 && count[4] == 1 ||
                count[1] == 1 && count[2] == 1 && count[3] == 1 && count[4] == 1 && count[5] == 1)
            {
                return true;
            }
            
            return false; 
        }

        public bool YahtzeeValidation(int[] dice)
        {
            int[] dieCount = DieCounter(dice);

            //This one checks for if there's a five anywhere in the array
            foreach (int count in dieCount)
            {
                if (count == 5) { return true; }
            }
            
            return false; 
            
        }

        public void BonusConditionsMet()
        {
            if ((Aces + Twos + Threes + Fours + Fives + Sixes >= 63) & !BonusScored)
            {
                Bonus = 35;
                BonusScored = true;
                TotalScore += Bonus;
            }
        }
        #endregion

        /* Scoring Method Notes
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
         *Update 2: Each Scoring Method now checks if the ScoreCard has been filled. If not, it fires off ScoreCardFilled()
         *Update 3: Each scoring was previously calculated, but now the player is prompted to confirm that they want to score 
         *that box for the points it contains. This way, if they accidentally click the wrong box, they can back out without losing points. 
         *The computer player will automatically select yes for all prompts.
         */

        // Update 4: I deleted the argument for dice the dice is no longer needed as an argument
        //because the scores are updated in real time as the dice are rolled,
        //so the current score options are always available in the ScoreCard object.
        #region Scoring Methods

        //The Upper and Lower Section methods are similar, so the first method in each region is annotated as the example for the others

        #region Upper Section

        public void AcesSelected( bool isComputer = false)
        {
            //Checks to see if the card has been completed first. If it is, then a text box gets shown as a catchall and nothing changes on the card
            if (ScoreCardNotFinished())
            {
                //Another check statement, to make sure this box as already not been scored.
                if (AcesScored == false)
                { 
                    MessageBoxResult choice = MessageBoxResult.No;

                    //As long as the player is a human, it will ask for an input via message box
                    if (!isComputer)
                    {
                        choice = MessageBox.Show("Do you want to score Aces? You will gain " + Aces + " points.", "Confirmation", MessageBoxButton.YesNo);
                    }

                    //If the player selects yes, the box gets scored, the flag is flipped, and TotalScore updates.
                    if (choice == MessageBoxResult.Yes || isComputer)
                    {
                        AcesScored = true;
                        TotalScore += Aces;
                    }
                }
                else
                {
                    
                    MessageBox.Show("This box has already been scored. It contains " + Aces + " points.");
                    
                }
            }
            else
            {
                ScoreCardFilled();
            }

            //Finally, the program checks if the bonus is scored.
            BonusConditionsMet();
        }

        public void TwosSelected( bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (TwosScored == false)
                {
                    MessageBoxResult choice = MessageBoxResult.No;

                    if (!isComputer)
                    {
                        choice = MessageBox.Show("Do you want to score Twos? You will gain " + Twos + " points.", "Confirmation", MessageBoxButton.YesNo);
                    }

                    if (choice == MessageBoxResult.Yes || isComputer)
                    {
                        TwosScored = true;
                        TotalScore += Twos;
                        
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + Twos + " points.");
                }

            }
            else { ScoreCardFilled(); }
            BonusConditionsMet();
        }

        public void ThreesSelected(bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (ThreesScored == false)
                {
                   
                    MessageBoxResult choice = MessageBoxResult.No;

                    if (!isComputer)
                    {
                        choice = MessageBox.Show("Do you want to score Threes? You will gain " + Threes + " points.", "Confirmation", MessageBoxButton.YesNo);
                    }

                    if (choice == MessageBoxResult.Yes || isComputer)
                    {
                        ThreesScored = true;
                        TotalScore += Threes;
                        
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + Threes + " points.");
                }
            }
            else { ScoreCardFilled(); }
            BonusConditionsMet();
        }

        public void FoursSelected(bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (FoursScored == false)
                {

                    MessageBoxResult choice = MessageBoxResult.No;

                    if (!isComputer)
                    {
                        choice = MessageBox.Show("Do you want to score Fours? You will gain " + Fours + " points.", "Confirmation", MessageBoxButton.YesNo);
                    }

                    if (choice == MessageBoxResult.Yes || isComputer)
                    {
                        FoursScored = true;
                        TotalScore += Fours;
                        
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + Fours + " points.");
                }
            }
            else { ScoreCardFilled(); }
            BonusConditionsMet();
        }

        public void FivesSelected( bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (FivesScored == false)
                {
                  
                    MessageBoxResult choice = MessageBoxResult.No;

                    if (!isComputer)
                    { 
                        choice = MessageBox.Show("Do you want to score Fives? You will gain " + Fives + " points.", "Confirmation", MessageBoxButton.YesNo);

                    }

                    if (choice == MessageBoxResult.Yes || isComputer)
                    {
                        FivesScored = true;
                        TotalScore += Fives;
                        
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + Fives + " points.");
                }
            }
            else { ScoreCardFilled(); }
            BonusConditionsMet();
        }

        public void SixesSelected( bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (SixesScored == false)
                {
                  

                    MessageBoxResult choice = MessageBoxResult.No;

                    if (!isComputer)
                    {
                        choice = MessageBox.Show("Do you want to score Sixes? You will gain " + Sixes + " points.", "Confirmation", MessageBoxButton.YesNo);

                    }

                    if (choice == MessageBoxResult.Yes || isComputer)
                    {
                        SixesScored = true;
                        TotalScore += Sixes;
                        
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + Sixes + " points.");
                }
            }
            else { ScoreCardFilled(); }
            BonusConditionsMet();
        }

        #endregion

        #region Lower Section

        public void ThreeOfAKindSelected( bool isComputer = false)
        {
            //Checks to see if the card has been completed first. If it is, then a text box gets shown as a catchall and nothing changes on the card
            if (ScoreCardNotFinished())
            {
                //Another check statement, to make sure this box as already not been scored.
                if (ThreeOfAKindScored == false)
                {
                    //As Lower Section Boxes have more particular scoring solutions, first it checks to see if the box will actually score points
                    //If it doesn't, the player may still score, but the card will inform them that they will receive 0 points and configure it properly
                    if (threeOfAKind>0)
                    {
                        MessageBoxResult choice = MessageBoxResult.No;

                        //As long as the player is a human, it will ask for an input via message box
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Three of a Kind? You will gain "
                                            + ThreeOfAKind + " points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        //If the player selects yes, the box gets scored, the flag is flipped, and TotalScore updates.
                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            ThreeOfAKindScored = true;
                            TotalScore += ThreeOfAKind;
                        }
                    }
                    else
                    {

                        MessageBoxResult choice = MessageBoxResult.No;
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Three of a Kind? You will gain 0 points.",
                                "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            ThreeOfAKind = 0;
                            ThreeOfAKindScored = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + ThreeOfAKind + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void FourOfAKindSelected(int[] Dice, bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (FourOfAKindScored == false)
                {
                    if (fourOfAKind>0)
                    {
                       

                        MessageBoxResult choice = MessageBoxResult.No;

                        if (!isComputer)
                        { 
                            choice = MessageBox.Show("Do you want to score Four of a Kind? You will gain "
                                          + FourOfAKind + " points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            FourOfAKindScored = true;
                            TotalScore += FourOfAKind;
                        }
                    }
                    else
                    {

                        MessageBoxResult choice = MessageBoxResult.No;

                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Four of a Kind? You will gain 0 points.",
                                "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            FourOfAKind = 0; 
                            FourOfAKindScored = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + FourOfAKind + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void FullHouseSelected(bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (FullHouseScored == false)
                {
                    if (fullHouse==25)
                    {
                        MessageBoxResult choice = MessageBoxResult.No;

                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Full House? You will gain 25 points.", "Confirmation", MessageBoxButton.YesNo);

                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            FullHouseScored = true;
                            TotalScore += FullHouse;
                        }
                    }
                    else
                    {
                        MessageBoxResult choice = MessageBoxResult.No;
                        
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Full House? You will gain 0 points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            FullHouse = 0;
                            FullHouseScored = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + FullHouse + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void SmallStraightSelected( bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (SmallStraightScored == false)
                {
                    if (smallStraight==30)
                    {

                        MessageBoxResult choice = MessageBoxResult.No;
                        
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Small Straight? You will gain 30 points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            SmallStraightScored = true;
                            TotalScore += SmallStraight;
                        }
                    }
                    else
                    {
                        MessageBoxResult choice = MessageBoxResult.No;
                        
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Small Straight? You will gain 0 points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes)
                        {
                            SmallStraight = 0;
                            SmallStraightScored = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + SmallStraight + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void LargeStraightSelected   ( bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (LargeStraightScored == false)
                {
                    if (largeStraight==40)
                    {

                        MessageBoxResult choice = MessageBoxResult.No;
                        
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Large Straight? You will gain 40 points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            LargeStraightScored = true;
                            TotalScore += LargeStraight;
                        }
                    }
                    else
                    {
                        MessageBoxResult choice = MessageBoxResult.No;
                        
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Large Straight? You will gain 0 points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            LargeStraight = 0;
                            LargeStraightScored = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + LargeStraight + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void YahtzeeSelected( bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (YahtzeeScored == false)
                {
                    if (yahtzee==50)
                    {

                        MessageBoxResult choice = MessageBoxResult.No;
                        
                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Yahtzee? You will gain 50 points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            YahtzeeScored = true;
                            TotalScore += Yahtzee;
                        }
                    }
                    else
                    {

                        MessageBoxResult choice = MessageBoxResult.No;

                        if (!isComputer)
                        {
                            choice = MessageBox.Show("Do you want to score Yahtzee? You will gain 0 points.", "Confirmation", MessageBoxButton.YesNo);
                        }

                        if (choice == MessageBoxResult.Yes || isComputer)
                        {
                            Yahtzee = 0;
                            YahtzeeScored = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + Yahtzee + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        public void ChanceSelected(int[] Dice, bool isComputer = false)
        {
            if (ScoreCardNotFinished())
            {
                if (ChanceScored == false)
                {
                    //Needed to set Chance to 0 because of how the hypothetical display messes with this variable. 
                    // - Marcus
                    Chance = 0;
                    int x = 0;
                    foreach (int Die in Dice)
                    {
                        Chance += Die;
                        x++;
                    }

                    MessageBoxResult choice = MessageBoxResult.No;
                    
                    if (!isComputer)
                    {
                        choice = MessageBox.Show("Do you want to score Chance? You will gain " + Chance + " points.", "Confirmation", MessageBoxButton.YesNo);
                    }

                    if (choice == MessageBoxResult.Yes || isComputer)
                    {
                        ChanceScored = true;
                        TotalScore += Chance;
                    }
                }
                else
                {
                    MessageBox.Show("This box has already been scored. It contains " + Chance + " points.");
                }
            }
            else { ScoreCardFilled(); }
        }

        #endregion

        //method to show just the score selected in the scored card
        //and set the rest to 0 
        public void ShowSelectedScores()
        {
            if (AcesScored == false) { Aces = 0; }
            if (TwosScored == false) { Twos = 0; }
            if (ThreesScored == false) { Threes = 0; }
            if (FoursScored == false) { Fours = 0; }
            if (FivesScored == false) { Fives = 0; }
            if (SixesScored == false) { Sixes = 0; }
            if (BonusScored == false) { Bonus = 0; }
            if (ThreeOfAKindScored == false) { ThreeOfAKind = 0; }
            if (FourOfAKindScored == false) { FourOfAKind = 0; }
            if (FullHouseScored == false) { FullHouse = 0; }
            if (SmallStraightScored == false) { SmallStraight = 0; }
            if (LargeStraightScored == false) { LargeStraight = 0; }
            if (YahtzeeScored == false) { Yahtzee = 0; }
            if (ChanceScored == false) { Chance = 0; }
        }
        #endregion
    }
}