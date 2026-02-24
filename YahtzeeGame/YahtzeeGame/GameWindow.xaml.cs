using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YahtzeeGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        public GameManager game;

        #region Click Events

        /// <summary>
        /// Closes Program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>'
        /// 

     

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRollDice_Click(object sender, RoutedEventArgs e)
        {


            game.RollUsed(CheckDice());

            DisplayDiceSet();


            lblRollsLeft.Content = (game.Rolls).ToString();


            ////Function that handles rolling the dice pool and displaying matching visuals.

            ////Counter for times rolled in one Turn. Starts at 3, and decreases by 1 every time the dice are rerolled.
            //int counter = int.Parse(lblTimesRolled.ContentStringFormat);

            //lblTimesRolled.ContentStringFormat = (counter - 1).ToString();
            //lblTimesRolled.Content = (counter - 1).ToString();

            ////Gets the current input state.
            //bool[] DiceState = CheckDice();

            ////RNG for dice.
            //Random random = new Random();

            ////For loop to take us through each of the 5 dice sequentially.

            //for (int c = 0; c < 5; c++)
            //{
            //    //If Statement Triggers if a die is unchecked. Checked Dice do not reroll.

            //    if (DiceState[c] == false)
            //    {
            //        int DieValue = random.Next(1, 7);

            //        //Displays rolled die face. c = die face.

            //        DisplayDice(c, DieValue);

            //    }
            //}

            //// If Statement fires once the roll counter hits 0, marking the end of a turn. 

            //if (int.Parse(lblTimesRolled.ContentStringFormat) == 0)
            //{
            //    TurnActivation(false);
            //}
            if (game.Rolls == 0)
            {
                DiceActivation(false);
            }

        }

<<<<<<< Updated upstream
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //Upon starting, the start button is disabled, and the turn controls are activated.
           DiceActivation(true);
            btnStart.IsEnabled = false;
        }


        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            //Resets the Turn Space and Deactivates the Turn
            Reset();
            DiceActivation(false);

            ///Clear unlocked scores on button press. - Beau
            scoreBoard.ClearNonLockedScores();

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            //Program Information
            MessageBox.Show("Yahtzee Version 0.1. Made By Marcus Cantrall, Bradye Vanderheyden,Connor Orton, Nicole Gonzalez Rodriguez and Beau Baker. ");
=======

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            
>>>>>>> Stashed changes
        }
        #endregion

        private void DisplayDiceSet()
        {
            for (int c = 0; c < 5; c++)
            {
                DisplayDice(c, game.Pool.diceValue[c]);
            }
        }

        #region Voids
        private void LoadScores()
        {
            Dictionary<int, string> Scores = new Dictionary<int, string>();

            List<int> ScoreNum = new List<int>();

            String[] LineSplit = new string[2];

            StreamReader Input = File.OpenText("HighScores.txt");

            while (!Input.EndOfStream)
            {
                LineSplit = Input.ReadLine().Split(' ');


                Scores.Add(int.Parse(LineSplit[0]), LineSplit[1]);

                ScoreNum.Add(int.Parse(LineSplit[0]));

            }

            ScoreNum.OrderByDescending(score => score);

            int[] topFive = new int[5];

            for (int i = 0; i < 5; i++)
            {
                topFive[i] = ScoreNum[i];
            }

            MessageBox.Show($"1. {topFive[0]}, {Scores[topFive[0]]} \n2.  {topFive[1]}, {Scores[topFive[1]]} \n3. {topFive[2]}, {Scores[topFive[2]]}\n4. {topFive[3]}, {Scores[topFive[3]]}\n5. {topFive[4]}, {Scores[topFive[4]]}  ");




        }

        private void Reset()
        {
            //Resets the dice images and Turn Roll Counter

            DisplayDice(0, 1);
            DisplayDice(1, 2);
            DisplayDice(2, 3);
            DisplayDice(3, 4);
            DisplayDice(4, 5);
<<<<<<< Updated upstream
         
    
            btnStart.IsEnabled = true;

=======

            lblTimesRolled.Content = "3";
            lblTimesRolled.ContentStringFormat = "3";
>>>>>>> Stashed changes
        }

        private void DisplayDice(int DicePos, int DiceValue)
        {
            //This method displays the results of dice rolls in the form dynamically, so that the code is reused.

            //Currently we hold the dice's numeric value in the ContentStringFormat property.
            //Change when Dice Class is fully implemented?

            //Find Controls
            CheckBox DieCheck = (CheckBox)this.FindName($"cbDie{DicePos + 1}");
            Image DieImage = (Image)this.FindName($"Die{DicePos + 1}");

            //Record Die Value
            DieCheck.ContentStringFormat = DiceValue.ToString();
            //Set new Die Image
            DieImage.Source = new BitmapImage(new Uri($@"{DiceValue}Die.bmp", UriKind.Relative));
        }

        private void DiceActivation(bool b)
        {

            // Taking in a Bool Argument b, this method enables the turn controls
            // if b = true, and disables them if b = false.

            cbDie1.IsEnabled = b;
            cbDie2.IsEnabled = b;
            cbDie3.IsEnabled = b;
            cbDie4.IsEnabled = b;
            cbDie5.IsEnabled = b;
            BtnRollDice.IsEnabled = b;

            cbDie1.IsChecked = false;
            cbDie2.IsChecked = false;
            cbDie3.IsChecked = false;
            cbDie4.IsChecked = false;
            cbDie5.IsChecked = false;


        }

        #endregion

        #region value-returning
        private bool[] CheckDice()
        {
            //This function packages the control data from the dice.
            //It packages the checked state of the dice into an array
            //that shows what dice are selected.

            bool[] dice = new bool[5];
            dice[0] = cbDie1.IsChecked ?? false;
            dice[1] = cbDie2.IsChecked ?? false;
            dice[2] = cbDie3.IsChecked ?? false;
            dice[3] = cbDie4.IsChecked ?? false;
            dice[4] = cbDie5.IsChecked ?? false;
            return dice;
        }

        #endregion

        private void DiceState(bool state)
        {
            Die1.IsEnabled = state;
            Die2.IsEnabled = state;
            Die3.IsEnabled = state;
            Die4.IsEnabled = state;
            Die5.IsEnabled = state;
        }

        private void btnHighScore_Click(object sender, RoutedEventArgs e)
        {
            LoadScores();
        }

        private void btnAces_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTwos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThrees_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

