using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace YahtzeeGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public ObservableCollection<Player> Players { get; } = new ObservableCollection<Player>();


        public GameManager game;
        public Player currentPlayer;
        public DumbBot bot;
        public int tester = 0;
        public bool gameEnd = false;

        public GameWindow(List<Player> players)
        {
            InitializeComponent();


            Players.Clear();
            foreach (var p in players)
                Players.Add(p);


            game = new GameManager();
            game.players = Players;
            currentPlayer = Players[0];
            tbCurrentPlayer.Text = currentPlayer.PlayerName;
            tbTotalScore.Text = currentPlayer.PlayerScores.TotalScore.ToString();
            ScoreCardActivated(false);
            tester = players.Count;
            DataContext = this;





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
        #endregion

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
            currentPlayer.PlayerScores.ShowSelectedScores();
            fillScoresOpcion(game.Pool.diceValue);

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //Upon starting, the start button is disabled, and the turn controls are activated.
            DiceActivation(true);

            /// If CPU is current player, start CPU immediately after pressing Start. - EasyModeBot
            _ = PlayCpuTurnIfNeededAsync();
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
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            //Program Information
            MessageBox.Show("Yahtzee Version 0.1. Made By Marcus Cantrall, Bradye Vanderheyden, Connor Orton, Nicole Gonzalez Rodriguez and Beau Baker. ");
        }



        #endregion

        #region  ScoreCard Clicks
        private void btnAces_Click(object sender, RoutedEventArgs e)
        {
            //Update: I deleted the argument for dice the dice is no longer needed as an argument
            //because the scores are updated in real time as the dice are rolled,
            //so the current score options are always available in the ScoreCard object.
            currentPlayer.PlayerScores.AcesSelected();

            if (currentPlayer.PlayerScores.AcesScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnTwos_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.TwosSelected();

            if (currentPlayer.PlayerScores.TwosScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnThrees_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.ThreesSelected();

            if (currentPlayer.PlayerScores.ThreesScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnFours_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FoursSelected();

            if (currentPlayer.PlayerScores.FoursScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnFives_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FivesSelected();

            if (currentPlayer.PlayerScores.FivesScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnSixes_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.SixesSelected();

            if (currentPlayer.PlayerScores.SixesScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnThreeKind_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.ThreeOfAKindSelected();

            if (currentPlayer.PlayerScores.ThreeOfAKindScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnFourKind_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FourOfAKindSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.FourOfAKindScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnFullHouse_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.FullHouseSelected();

            if (currentPlayer.PlayerScores.FullHouseScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnSmallStraight_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.SmallStraightSelected();

            if (currentPlayer.PlayerScores.SmallStraightScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnLargeStraight_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.LargeStraightSelected();

            if (currentPlayer.PlayerScores.LargeStraightScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnYahtzee_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.YahtzeeSelected();

            if (currentPlayer.PlayerScores.YahtzeeScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }
        private void btnChance_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.PlayerScores.ChanceSelected(game.Pool.diceValue);

            if (currentPlayer.PlayerScores.ChanceScored)
            {
                currentPlayer.PlayerScores.ShowSelectedScores();
                NextTurn();

            }
        }


        #endregion

        #region Voids


        #region Dice Void Methods

        private void DiceState(bool state)
        {
            cbDie1.IsEnabled = state;
            cbDie2.IsEnabled = state;
            cbDie3.IsEnabled = state;
            cbDie4.IsEnabled = state;
            cbDie5.IsEnabled = state;

        }

        private void CheckState(bool state)
        {
            cbDie1.IsChecked = state;
            cbDie2.IsChecked = state;
            cbDie3.IsChecked = state;
            cbDie4.IsChecked = state;
            cbDie5.IsChecked = state;
        }

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


        #endregion



        #endregion

        #region ScoreCard Void Methods

        private void RefactorBoard()
        {
            btnAces.IsEnabled = !currentPlayer.PlayerScores.AcesScored;

            btnTwos.IsEnabled = !currentPlayer.PlayerScores.TwosScored;

            btnThrees.IsEnabled = !currentPlayer.PlayerScores.ThreesScored;

            btnFours.IsEnabled = !currentPlayer.PlayerScores.FoursScored;

            btnFives.IsEnabled = !currentPlayer.PlayerScores.FivesScored;

            btnSixes.IsEnabled = !currentPlayer.PlayerScores.SixesScored;

            btnThreeKind.IsEnabled = !currentPlayer.PlayerScores.ThreeOfAKindScored;

            btnFourKind.IsEnabled = !currentPlayer.PlayerScores.FourOfAKindScored;

            btnFullHouse.IsEnabled = !currentPlayer.PlayerScores.FullHouseScored;

            btnSmallStraight.IsEnabled = !currentPlayer.PlayerScores.SmallStraightScored;

            btnLargeStraight.IsEnabled = !currentPlayer.PlayerScores.LargeStraightScored;

            btnYahtzee.IsEnabled = !currentPlayer.PlayerScores.YahtzeeScored;

            btnChance.IsEnabled = !currentPlayer.PlayerScores.ChanceScored;

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



        /// This method will be used to fill the score options with the potential scores for the current dice roll,  
        /// This method will be used to fill the score options with the potential scores for the current dice roll, 
        ///so the player can make an informed decision.
        private void fillScoresOpcion(int[] diceInPool)

        {
            foreach (int Die in diceInPool)
            {
                if (Die == 1 && !currentPlayer.PlayerScores.AcesScored)
                {
                    currentPlayer.PlayerScores.Aces += Die;
                }
                if (Die == 2 && !currentPlayer.PlayerScores.TwosScored)
                {
                    currentPlayer.PlayerScores.Twos += Die;
                }
                if (Die == 3 && !currentPlayer.PlayerScores.ThreesScored)
                {
                    currentPlayer.PlayerScores.Threes += Die;
                }
                if (Die == 4 && !currentPlayer.PlayerScores.FoursScored)
                {
                    currentPlayer.PlayerScores.Fours += Die;
                }
                if (Die == 5 && !currentPlayer.PlayerScores.FivesScored)
                {
                    currentPlayer.PlayerScores.Fives += Die;
                }
                if (Die == 6 && !currentPlayer.PlayerScores.SixesScored)
                {
                    currentPlayer.PlayerScores.Sixes += Die;
                }
                if (!currentPlayer.PlayerScores.ThreeOfAKindScored)
                {
                    if (currentPlayer.PlayerScores.ThreeKindValidation(diceInPool))
                    {
                        currentPlayer.PlayerScores.ThreeOfAKind += Die;
                    }
                }
                if (!currentPlayer.PlayerScores.FourOfAKindScored)
                {
                    if (currentPlayer.PlayerScores.FourKindValidation(diceInPool))
                    {
                        currentPlayer.PlayerScores.FourOfAKind += Die;
                    }
                }
                if (!currentPlayer.PlayerScores.ChanceScored)
                {
                    currentPlayer.PlayerScores.Chance += Die;
                }
            }

            if (!currentPlayer.PlayerScores.FullHouseScored)
            {
                if (currentPlayer.PlayerScores.FullHouseValidation(diceInPool))
                {
                    currentPlayer.PlayerScores.FullHouse += 25;
                }
            }
            if (!currentPlayer.PlayerScores.SmallStraightScored)
            {
                if (currentPlayer.PlayerScores.SmallStraightValidation(diceInPool))
                {
                    currentPlayer.PlayerScores.SmallStraight += 30;
                }
            }
            if (!currentPlayer.PlayerScores.LargeStraightScored)
            {
                if (currentPlayer.PlayerScores.LargeStraightValidation(diceInPool))
                {
                    currentPlayer.PlayerScores.LargeStraight += 40;
                }
            }
            if (!currentPlayer.PlayerScores.YahtzeeScored)
            {
                if (currentPlayer.PlayerScores.YahtzeeValidation(diceInPool))
                {
                    currentPlayer.PlayerScores.Yahtzee += 50;
                }
            }


        }


        private void FillBoxes()
        {


            tbCurrentPlayer.Text = currentPlayer.PlayerName;
            tbTotalScore.Text = currentPlayer.PlayerScores.TotalScore.ToString();


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

            if (game.IsGameOver())
            {
                EndGame();
            }

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
            fillScoresOpcion(game.Pool.diceValue);


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

