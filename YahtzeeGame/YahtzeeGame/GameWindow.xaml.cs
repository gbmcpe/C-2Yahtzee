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

        public GameManager game;
        public Player currentPlayer;
        public DumbBot bot;
        public int tester = 0;
        public bool gameEnd = false;

        public GameWindow(List<Player> players)
        {
            InitializeComponent();
            game = new GameManager();
            game.players = players;
            currentPlayer = players[0];
            tbCurrentPlayer.Text = currentPlayer.PlayerName;
            tbTotalScore.Text = currentPlayer.PlayerScores.totalScore.ToString();
            ScoreCardActivated(false);
            tester = players.Count;
            

            /// If the first player is CPU, run CPU turn after the window loads. - EasyModeBot
            this.Loaded += async (_, __) => await PlayCpuTurnIfNeededAsync();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentPlayer.GetType() == typeof(DumbBot))
            {
                bot = (DumbBot)currentPlayer;
                BotTurn();
            }
        }

        #region Click Events


        #region Main Form Click Events

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

            lblTimesRolled.Content = (game.Rolls).ToString();

            if (game.Rolls == 0)
            {
                DiceActivation(false);
            }

            if (game.Rolls == 2)
            {
                DiceActivation(true);
            }
            ScoreCardActivated(true);
            RefactorBoard();
        }




        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            /*
            //Resets the Turn Space and Deactivates the Turn
            Reset();
            DiceActivation(false);
            */

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        private void btnViewScoreCards(object sender, RoutedEventArgs e)
        {
            //TODO: Implement this

            GameReview pausereview = new GameReview(game.players, false);
            pausereview.ShowDialog();
        }


        #endregion

        #region  ScoreCard Clicks
        private void btnAces_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.AcesSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.acesScored)
            {
                NextTurn();
            }
        }
        private void btnTwos_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.TwosSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.twosScored)
            {
                NextTurn();
            }
        }
        private void btnThrees_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.ThreesSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.threesScored)
            {
                NextTurn();
            }
        }
        private void btnFours_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FoursSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.foursScored)
            {
                NextTurn();
            }
        }
        private void btnFives_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FivesSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.fivesScored)
            {
                NextTurn();
            }
        }
        private void btnSixes_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.SixesSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.sixesScored)
            {
                NextTurn();
            }
        }
        private void btnThreeKind_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.ThreeOfAKindSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.threeOfAKindScored)
            {
                NextTurn();
            }
        }
        private void btnFourKind_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FourOfAKindSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.fourOfAKindScored)
            {
                NextTurn();
            }
        }
        private void btnFullHouse_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FullHouseSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.fullHouseScored)
            {
                NextTurn();
            }
        }
        private void btnSmallStraight_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.SmallStraightSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.smallStraightScored)
            {
                NextTurn();
            }
        }
        private void btnLargeStraight_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.LargeStraightSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.largeStraightScored)
            {
                NextTurn();
            }
        }
        private void btnYahtzee_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.YahtzeeSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.yahtzeeScored)
            {
                NextTurn();
            }
        }
        private void btnChance_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.ChanceSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.chanceScored)
            {
                NextTurn();
            }
        }
        #endregion
    

        #endregion

        #region Voids


        #region Dice Void Methods

        private void DisplayDiceSet()
        {
            for (int c = 0; c < 5; c++)
            {
                DisplayDice(c, game.Pool.diceValue[c]);
            }
        }

        private void ResetDice()
        {
            //Resets the dice images and Turn Roll Counter

            DisplayDice(0, 1);
            DisplayDice(1, 2);
            DisplayDice(2, 3);
            DisplayDice(3, 4);
            DisplayDice(4, 5);
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

            DiceState(b);
            BtnRollDice.IsEnabled = b;
            CheckState(false);

        }

        private void DiceState(bool state)
        {
            Die1.IsEnabled = state;
            Die2.IsEnabled = state;
            Die3.IsEnabled = state;
            Die4.IsEnabled = state;
            Die5.IsEnabled = state;
        }

        private void CheckState(bool state)
        {
            cbDie1.IsChecked = state;
            cbDie2.IsChecked = state;
            cbDie3.IsChecked = state;
            cbDie4.IsChecked = state;
            cbDie5.IsChecked = state;
        }

        #endregion

        #region ScoreCard Void Methods

        private void RefactorBoard()
        {
                btnAces.IsEnabled = !currentPlayer.PlayerScores.acesScored;

                btnTwos.IsEnabled = !currentPlayer.PlayerScores.twosScored;

                btnThrees.IsEnabled = !currentPlayer.PlayerScores.threesScored;

                btnFours.IsEnabled = !currentPlayer.PlayerScores.foursScored;
     
                btnFives.IsEnabled = currentPlayer.PlayerScores.fivesScored;

                btnSixes.IsEnabled = !currentPlayer.PlayerScores.sixesScored;

                btnThreeKind.IsEnabled = !currentPlayer.PlayerScores.threeOfAKindScored;

                btnFourKind.IsEnabled = !currentPlayer.PlayerScores.fourOfAKindScored;
 
                btnFullHouse.IsEnabled = !currentPlayer.PlayerScores.fullHouseScored;

                btnSmallStraight.IsEnabled = !currentPlayer.PlayerScores.smallStraightScored;

                btnLargeStraight.IsEnabled = !currentPlayer.PlayerScores.largeStraightScored;
     
                btnYahtzee.IsEnabled = !currentPlayer.PlayerScores.yahtzeeScored;

                btnChance.IsEnabled = !currentPlayer.PlayerScores.chanceScored;
     
        }

        private void ScoreCardActivated(bool state)
        {
            //When LockScoreCard(true), the scorecard buttons are disabled. When false, they are enabled.
            btnAces.IsEnabled = state;
            btnTwos.IsEnabled = state;
            btnThrees.IsEnabled = state;
            btnFours.IsEnabled = state;
            btnFives.IsEnabled = state;
            btnSixes.IsEnabled = state;
            btnThreeKind.IsEnabled = state;
            btnFourKind.IsEnabled = state;
            btnFullHouse.IsEnabled = state;
            btnSmallStraight.IsEnabled = state;
            btnLargeStraight.IsEnabled = state;
            btnYahtzee.IsEnabled = state;
            btnChance.IsEnabled = state;
        }



        private void FillBoxes()
        {
            tbAces.Text = "";
            tbTwos.Text = "";
            tbThrees.Text = "";
            tbFours.Text = "";
            tbFives.Text = "";
            tbSixes.Text = "";
            tbThreeKind.Text = "";
            tbFourKind.Text = "";
            tbFullHouse.Text = "";
            tbSmallStraight.Text = "";
            tbLargeStraight.Text = "";
            tbYahtzee.Text = "";
            tbChance.Text = "";

            tbCurrentPlayer.Text = currentPlayer.PlayerName;
            tbTotalScore.Text = currentPlayer.PlayerScores.totalScore.ToString();

            if (currentPlayer.PlayerScores.acesScored)
            {
                tbAces.Text = currentPlayer.PlayerScores.aces.ToString();
            }

            if (currentPlayer.PlayerScores.twosScored)
            {
                tbTwos.Text = currentPlayer.PlayerScores.twos.ToString();
            }

            if (currentPlayer.PlayerScores.threesScored)
            {
                tbThrees.Text = currentPlayer.PlayerScores.threes.ToString();
            }

            if (currentPlayer.PlayerScores.foursScored)
            {
                tbFours.Text = currentPlayer.PlayerScores.fours.ToString();
            }

            if (currentPlayer.PlayerScores.fivesScored)
            {
                tbFives.Text = currentPlayer.PlayerScores.fives.ToString();
            }

            if (currentPlayer.PlayerScores.sixesScored)
            {
                tbSixes.Text = currentPlayer.PlayerScores.sixes.ToString();
            }

            /*
            if (currentPlayer.PlayerScores.bonusScored)
            {
                tbBonus.Text = currentPlayer.PlayerScores.bonus.ToString();
            }
            */
            if (currentPlayer.PlayerScores.threeOfAKindScored)
            {
                tbThreeKind.Text = currentPlayer.PlayerScores.threeOfAKind.ToString();
            }

            if (currentPlayer.PlayerScores.fourOfAKindScored)
            {
                tbFourKind.Text = currentPlayer.PlayerScores.fourOfAKind.ToString();
            }

            if (currentPlayer.PlayerScores.fullHouseScored)
            {
                tbFullHouse.Text = currentPlayer.PlayerScores.fullHouse.ToString();
            }

            if (currentPlayer.PlayerScores.smallStraightScored)
            {
                tbSmallStraight.Text = currentPlayer.PlayerScores.smallStraight.ToString();
            }

            if (currentPlayer.PlayerScores.largeStraightScored)
            {
                tbLargeStraight.Text = currentPlayer.PlayerScores.largeStraight.ToString();
            }

            if (currentPlayer.PlayerScores.yahtzeeScored)
            {
                tbYahtzee.Text = currentPlayer.PlayerScores.yahtzee.ToString();
            }

            if (currentPlayer.PlayerScores.chanceScored)
            {
                tbChance.Text = currentPlayer.PlayerScores.chance.ToString();
            }
        }
        #endregion

        #region Game Void Methods

        public void BotTurn()
        {
            bool stillBot = true;

            while (stillBot && !gameEnd)
            {
            
                game.RollUsed(CheckDice());
                bot.DecisionTree(game.Pool.diceValue);
                game.EndTurn();
                currentPlayer = game.currentPlayer;
                if (currentPlayer.GetType() == typeof(DumbBot))
                {
                    bot = (DumbBot)game.currentPlayer;
                }
                FillBoxes();
                CheckState(false);
                game.RollUsed(CheckDice());
                DisplayDiceSet();
                game.Rolls = 2;
                lblTimesRolled.Content = 2.ToString();
                DiceActivation(true);
                ScoreCardActivated(true);
                RefactorBoard();
                if (currentPlayer.GetType() == typeof(Player))
                {
                    stillBot = false;
                }

            }
        }

        public void NextTurn()
        {

                    game.EndTurn();
                    currentPlayer = game.currentPlayer;

                    FillBoxes();
                    CheckState(false);
                    game.RollUsed(CheckDice());
                    DisplayDiceSet();
                    game.Rolls = 2;
                    lblTimesRolled.Content = 2.ToString();
                    DiceActivation(true);
                    ScoreCardActivated(true);
                    RefactorBoard();

                    if (currentPlayer.GetType() == typeof(DumbBot))
                    {
                        bot = (DumbBot)game.currentPlayer;
                        BotTurn();

                    }
                    else
                    {



                        BtnRollDice.IsEnabled = true;





                        /// If the next player is CPU, let the CPU play automatically. - EasyModeBot
                        _ = PlayCpuTurnIfNeededAsync();
                    }

            if (game.IsGameOver())
            {
                EndGame();
            }
        }
        

        private void EndGame()
        {
            MessageBox.Show("The game has ended. Generating final scores now.");

            int x = game.players.Count - 1;

            gameEnd = true;

            GameReview gReview = new GameReview(game.players, true);
            gReview.Show();
            this.Close();



            //  while (x >= 0)
            //{
            //     MessageBox.Show(game.players[x].PlayerName + " achieved " + game.players[x].PlayerScores.totalScore +
            //    " Points.");
            //     x--;
            //  }


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


        #endregion

        #region EasyModeBot

        /// <summary>
        /// Holds the easy mode bot logic object used for CPU decisions.
        /// </summary>
        private readonly EasyModeBot _bot = new EasyModeBot();

        /// <summary>
        /// Prevents the CPU from executing multiple turns at the same time.
        /// </summary>
        private bool _cpuTurnRunning = false;

        /// <summary>
        /// Controls how long the CPU waits between visible actions.
        /// </summary>
        private int _cpuStepDelayMs = 600;

        /// <summary>
        /// Tracks whether the next CPU turn should be queued after the current CPU turn fully releases.
        /// </summary>
        private bool _queueNextCpuTurn = false;

        /// <summary>
        /// Determines if the given player is an Easy CPU.
        /// </summary>
        private bool IsCpuPlayer(Player p)
        {
            return p != null
                   && p.PlayerName != null
                   && p.PlayerName.EndsWith("(CPU)");
        }

        /// <summary>
        /// Executes a full CPU turn visibly so the user can watch.
        /// </summary>
        public async Task PlayCpuTurnIfNeededAsync()
        {
            /// If current player is not CPU, exit.
            if (!IsCpuPlayer(currentPlayer)) return;

            /// Prevent duplicate CPU execution.
            if (_cpuTurnRunning) return;

            /// Lock CPU execution.
            _cpuTurnRunning = true;

            /// Clear any previous queued state at the start of a CPU turn.
            _queueNextCpuTurn = false;

            try
            {
                /// Show CPU playing inside tbCurrentPlayer textbox.
                tbCurrentPlayer.Text = currentPlayer.PlayerName;

                /// Enable dice controls for CPU turn.
                DiceActivation(true);

                /// Clear holds so user can see CPU choose them.
                cbDie1.IsChecked = false;
                cbDie2.IsChecked = false;
                cbDie3.IsChecked = false;
                cbDie4.IsChecked = false;
                cbDie5.IsChecked = false;

                /// Short pause so UI updates.
                await Task.Delay(_cpuStepDelayMs);

                /// Roll once so the CPU makes a decision.
                if (game.Rolls > 0)
                {
                    /// Roll dice.
                    game.RollUsed(CheckDice());

                    /// Update dice images.
                    DisplayDiceSet();

                    /// Update rolls remaining label.
                    lblTimesRolled.Content = game.Rolls.ToString();

                    /// Preserve existing enable/disable behavior.
                    if (game.Rolls == 0) DiceActivation(false);
                    if (game.Rolls == 2) DiceActivation(true);

                    /// Enable scoring buttons and disable used ones.
                   ScoreCardActivated(true);
                    RefactorBoard();

                    /// Pause so roll results are visible.
                    await Task.Delay(_cpuStepDelayMs);
                }

                /// Continue rolling while rolls remain.
                while (game.Rolls > 0)
                {
                    /// Ask bot which dice to keep.
                    bool[] holds = _bot.ChooseDice(game.Pool.diceValue, game.Rolls);

                    /// Apply hold decisions to UI.
                    cbDie1.IsChecked = holds[0];
                    cbDie2.IsChecked = holds[1];
                    cbDie3.IsChecked = holds[2];
                    cbDie4.IsChecked = holds[3];
                    cbDie5.IsChecked = holds[4];

                    /// Pause so holds are visible.
                    await Task.Delay(_cpuStepDelayMs);

                    /// Roll dice.
                    game.RollUsed(CheckDice());

                    /// Update dice images.
                    DisplayDiceSet();

                    /// Update rolls remaining label.
                    lblTimesRolled.Content = game.Rolls.ToString();

                    /// Preserve existing enable/disable behavior.
                    if (game.Rolls == 0) DiceActivation(false);
                    if (game.Rolls == 2) DiceActivation(true);

                    /// Enable scoring buttons and disable used ones.
                    ScoreCardActivated(true);
                    RefactorBoard();

                    /// Pause so roll results are visible.
                    await Task.Delay(_cpuStepDelayMs);
                }

                /// Choose best scoring category.
                string category = _bot.ChooseCategory(game.Pool.diceValue, currentPlayer.PlayerScores);

                /// Apply score silently.
                _bot.ApplyScore(category, game.Pool.diceValue, currentPlayer.PlayerScores);

                /// Refresh score display.
                FillBoxes();

                /// Restore textbox to normal player name.
                tbCurrentPlayer.Text = currentPlayer.PlayerName;

                /// Pause briefly before ending turn.
                await Task.Delay(_cpuStepDelayMs);

                /// End CPU turn.
                NextTurn();

                /// If the next player is CPU, queue the next CPU turn after this one fully completes.
                _queueNextCpuTurn = IsCpuPlayer(currentPlayer) && currentPlayer.PlayerScores.ScoreCardNotFinished();
            }
            finally
            {
                /// Release CPU execution lock.
                _cpuTurnRunning = false;

                /// Run the next CPU turn on the dispatcher so it starts after this turn fully completes.
                if (_queueNextCpuTurn)
                {
                    Dispatcher.BeginInvoke(new Action(async () => await PlayCpuTurnIfNeededAsync()));
                }
            }
        }

        #endregion

       
    }
}

