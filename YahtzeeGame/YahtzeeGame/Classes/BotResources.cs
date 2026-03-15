using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace YahtzeeGame.Classes
{
    public class BotResources
    {
        #region Fields

        #region HardAI

        /// <summary>
        /// Stores the hard bot strategy helper. This is used only when a player is identified as the hard AI.
        /// </summary>
        private HardAIV2 _hardAI = new HardAIV2();

        #endregion

        /// <summary>
        /// Holds the currently selected CPU bot for the active turn.
        /// </summary>
        private CPUPlayer _bot;

        /// <summary>
        /// Prevents the same CPU turn from running more than once at the same time.
        /// </summary>
        private bool _cpuTurnRunning = false;

        /// <summary>
        /// Controls the delay between visible CPU actions.
        /// </summary>
        private int _cpuStepDelayMs = 600;

        /// <summary>
        /// Tracks whether another CPU turn should start immediately after the current one ends.
        /// </summary>
        private bool _queueNextCpuTurn = false;

        /// <summary>
        /// Stores the game manager so this class can use the current dice pool, roll count, and turn flow.
        /// </summary>
        private readonly GameManager _game;

        /// <summary>
        /// Gets the current player from GameWindow when needed.
        /// </summary>
        private readonly Func<Player> _getCurrentPlayer;

        /// <summary>
        /// Blocks player interaction while the CPU is taking its turn.
        /// </summary>
        private readonly GroupBox _gbPlayerBlocker;

        /// <summary>
        /// Displays the number of rolls remaining in the current turn.
        /// </summary>
        private readonly Label _lblTimesRolled;

        /// <summary>
        /// Displays the current player's name on screen.
        /// </summary>
        private readonly TextBox _tbCurrentPlayer;

        /// <summary>
        /// Represents the bot die hold checkbox's 1-5.
        /// </summary>
        private readonly CheckBox _cbDie1;
        private readonly CheckBox _cbDie2;
        private readonly CheckBox _cbDie3;
        private readonly CheckBox _cbDie4;
        private readonly CheckBox _cbDie5;

        /// <summary>
        /// Gets the current checkbox hold state from the window. 
        /// </summary>
        private readonly Func<bool[]> _checkDice;

        /// <summary>
        /// Refreshes the dice images after a roll. 
        /// </summary>
        private readonly Action _displayDiceSet;

        /// <summary>
        /// Enables or disables the dice controls in the UI. 
        /// </summary>
        private readonly Action<bool> _diceActivation;

        /// <summary>
        /// Enables or disables the scorecard buttons in the UI. 
        /// </summary>
        private readonly Action<bool> _scoreCardActivated;

        /// <summary>
        /// Refreshes scorecard button availability based on what has already been scored.
        /// </summary>
        private readonly Action _refactorBoard;

        /// <summary>
        /// Refreshes the main player/score display fields in the UI.
        /// </summary>
        private readonly Action _fillBoxes;

        /// <summary>
        /// Advances the game to the next turn.
        /// </summary>
        private readonly Action _nextTurn;

        /// <summary>
        /// Gives access to the UI dispatcher so queued CPU turns can start.
        /// </summary>
        private readonly Dispatcher _dispatcher;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new bot resource manager and stores the UI/game references it needs to run CPU turns.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="getCurrentPlayer"></param>
        /// <param name="gbPlayerBlocker"></param>
        /// <param name="lblTimesRolled"></param>
        /// <param name="tbCurrentPlayer"></param>
        /// <param name="cbDie1"></param>
        /// <param name="cbDie2"></param>
        /// <param name="cbDie3"></param>
        /// <param name="cbDie4"></param>
        /// <param name="cbDie5"></param>
        /// <param name="checkDice"></param>
        /// <param name="displayDiceSet"></param>
        /// <param name="diceActivation"></param>
        /// <param name="scoreCardActivated"></param>
        /// <param name="refactorBoard"></param>
        /// <param name="fillBoxes"></param>
        /// <param name="nextTurn"></param>
        /// <param name="dispatcher"></param>
        public BotResources(
            GameManager game,
            Func<Player> getCurrentPlayer,
            GroupBox gbPlayerBlocker,
            Label lblTimesRolled,
            TextBox tbCurrentPlayer,
            CheckBox cbDie1,
            CheckBox cbDie2,
            CheckBox cbDie3,
            CheckBox cbDie4,
            CheckBox cbDie5,
            Func<bool[]> checkDice,
            Action displayDiceSet,
            Action<bool> diceActivation,
            Action<bool> scoreCardActivated,
            Action refactorBoard,
            Action fillBoxes,
            Action nextTurn,
            Dispatcher dispatcher)
        {
            _game = game;
            _getCurrentPlayer = getCurrentPlayer;

            _gbPlayerBlocker = gbPlayerBlocker;
            _lblTimesRolled = lblTimesRolled;
            _tbCurrentPlayer = tbCurrentPlayer;

            _cbDie1 = cbDie1;
            _cbDie2 = cbDie2;
            _cbDie3 = cbDie3;
            _cbDie4 = cbDie4;
            _cbDie5 = cbDie5;

            _checkDice = checkDice;
            _displayDiceSet = displayDiceSet;
            _diceActivation = diceActivation;
            _scoreCardActivated = scoreCardActivated;
            _refactorBoard = refactorBoard;
            _fillBoxes = fillBoxes;
            _nextTurn = nextTurn;

            _dispatcher = dispatcher;
        }

        #endregion

        #region BotResources

        /// <summary>
        /// Checks whether the current player should use the hard AI helper to apply Hard Ai strategies.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool IsHardAiPlayer(Player p)
        {
            return p != null
                   && p.PlayerName != null
                   && p.PlayerName.Contains("Hard AI");
        }

        /// <summary>
        /// Converts HardAIV2 hold counts.
        /// </summary>
        /// <param name="holdCounts"></param>
        /// <param name="diceValues"></param>
        /// <returns></returns>
        private bool[] ConvertHardAiHolds(int[] holdCounts, int[] diceValues)
        {
            bool[] holds = new bool[5];
            int[] remaining = new int[6];

            for (int i = 0; i < 6; i++)
            {
                remaining[i] = holdCounts[i];
            }

            for (int i = 0; i < 5; i++)
            {
                int face = diceValues[i];

                if (face >= 1 && face <= 6 && remaining[face - 1] > 0)
                {
                    holds[i] = true;
                    remaining[face - 1]--;
                }
                else
                {
                    holds[i] = false;
                }
            }

            return holds;
        }

        /// <summary>
        /// Converts the hard AI numeric scoring result into the string category key used by CPUPlayer.
        /// </summary>
        /// <param name="decision"></param>
        /// <returns></returns>
        private string ConvertHardAiCategory(int decision)
        {
            if (decision == 1) return "aces";
            if (decision == 2) return "twos";
            if (decision == 3) return "threes";
            if (decision == 4) return "fours";
            if (decision == 5) return "fives";
            if (decision == 6) return "sixes";
            if (decision == 7) return "threeKind";
            if (decision == 8) return "fourKind";
            if (decision == 9) return "fullHouse";
            if (decision == 10) return "smallStraight";
            if (decision == 11) return "largeStraight";
            if (decision == 12) return "yahtzee";
            if (decision == 13) return "chance";

            return "chance";
        }

        /// <summary>
        /// Checks whether a player is controlled by the computer.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsCpuPlayer(Player p)
        {
            return p != null && p.ComputerPlayer;
        }

        /// <summary>
        /// Returns the correct bot for the active player.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private CPUPlayer GetCpuBot(Player p)
        {
            if (p == null) return new MediumBot();

            switch (p.botType)
            {
                case BotType.Easy:
                    return new ActuallyEasyBot();

                case BotType.Medium:
                    return new MediumBot();

                case BotType.Hard:
                    return new MediumBot();

                default:
                    return new MediumBot();
            }
        }

        /// <summary>
        /// Runs a full visible CPU turn from start to finish. This method rolls dice, chooses holds, previews scores and applies score, and goes onto next turn.
        /// </summary>
        /// <returns></returns>
        public async Task PlayCpuTurnIfNeededAsync()
        {
            if (_getCurrentPlayer == null)
            {
                _gbPlayerBlocker.Visibility = Visibility.Hidden;
                return;
            }

            Player currentPlayer = _getCurrentPlayer();

            if (currentPlayer == null)
            {
                _gbPlayerBlocker.Visibility = Visibility.Hidden;
                return;
            }

            if (!IsCpuPlayer(currentPlayer))
            {
                _gbPlayerBlocker.Visibility = Visibility.Hidden;
                return;
            }

            if (_cpuTurnRunning)
            {
                return;
            }

            _gbPlayerBlocker.Visibility = Visibility.Visible;

            _bot = GetCpuBot(currentPlayer);

            _game.Rolls = 3;
            _lblTimesRolled.Content = _game.Rolls.ToString();

            _cpuTurnRunning = true;
            _queueNextCpuTurn = false;

            try
            {
                currentPlayer = _getCurrentPlayer();
                if (currentPlayer == null)
                {
                    _gbPlayerBlocker.Visibility = Visibility.Hidden;
                    return;
                }

                _tbCurrentPlayer.Text = currentPlayer.PlayerName;

                _diceActivation(true);

                _cbDie1.IsChecked = false;
                _cbDie2.IsChecked = false;
                _cbDie3.IsChecked = false;
                _cbDie4.IsChecked = false;
                _cbDie5.IsChecked = false;

                await Task.Delay(_cpuStepDelayMs);

                if (_game.Rolls > 0)
                {
                    _game.RollUsed(_checkDice());
                    _displayDiceSet();

                    currentPlayer = _getCurrentPlayer();
                    if (currentPlayer == null)
                    {
                        _gbPlayerBlocker.Visibility = Visibility.Hidden;
                        return;
                    }

                    _bot.UpdateScorePreview(_game.Pool.diceValue, currentPlayer.PlayerScores);

                    _lblTimesRolled.Content = _game.Rolls.ToString();

                    if (_game.Rolls == 0) _diceActivation(false);
                    if (_game.Rolls == 2) _diceActivation(true);

                    _scoreCardActivated(true);
                    _refactorBoard();

                    await Task.Delay(_cpuStepDelayMs);
                }

                while (_game.Rolls > 0)
                {
                    currentPlayer = _getCurrentPlayer();
                    if (currentPlayer == null)
                    {
                        _gbPlayerBlocker.Visibility = Visibility.Hidden;
                        return;
                    }

                    bool[] holds;

                    if (IsHardAiPlayer(currentPlayer))
                    {
                        int[] hardDecision = _hardAI.RollingStrategy(currentPlayer.PlayerScores, _game.Pool.diceValue);
                        holds = ConvertHardAiHolds(hardDecision, _game.Pool.diceValue);
                    }
                    else
                    {
                        holds = _bot.ChooseDice(_game.Pool.diceValue, _game.Rolls);
                    }

                    _cbDie1.IsChecked = holds[0];
                    _cbDie2.IsChecked = holds[1];
                    _cbDie3.IsChecked = holds[2];
                    _cbDie4.IsChecked = holds[3];
                    _cbDie5.IsChecked = holds[4];

                    await Task.Delay(_cpuStepDelayMs);

                    _game.RollUsed(_checkDice());
                    _displayDiceSet();

                    currentPlayer = _getCurrentPlayer();
                    if (currentPlayer == null)
                    {
                        _gbPlayerBlocker.Visibility = Visibility.Hidden;
                        return;
                    }

                    _bot.UpdateScorePreview(_game.Pool.diceValue, currentPlayer.PlayerScores);

                    _lblTimesRolled.Content = _game.Rolls.ToString();

                    if (_game.Rolls == 0) _diceActivation(false);
                    if (_game.Rolls == 2) _diceActivation(true);

                    _scoreCardActivated(true);
                    _refactorBoard();

                    await Task.Delay(_cpuStepDelayMs);
                }

                currentPlayer = _getCurrentPlayer();
                if (currentPlayer == null)
                {
                    _gbPlayerBlocker.Visibility = Visibility.Hidden;
                    return;
                }

                string category;

                if (IsHardAiPlayer(currentPlayer))
                {
                    int hardPick = _hardAI.ScoringStrategy(currentPlayer.PlayerScores, _game.Pool.diceValue);
                    category = ConvertHardAiCategory(hardPick);
                }
                else
                {
                    category = _bot.ChooseCategory(_game.Pool.diceValue, currentPlayer.PlayerScores);
                }

                _bot.ApplyScore(category, _game.Pool.diceValue, currentPlayer.PlayerScores);

                _fillBoxes();

                _tbCurrentPlayer.Text = currentPlayer.PlayerName;

                await Task.Delay(_cpuStepDelayMs);

                _nextTurn();

                currentPlayer = _getCurrentPlayer();
                if (currentPlayer == null)
                {
                    _gbPlayerBlocker.Visibility = Visibility.Hidden;
                    return;
                }

                _queueNextCpuTurn = IsCpuPlayer(currentPlayer) && currentPlayer.PlayerScores.ScoreCardNotFinished();
            }
            finally
            {
                _gbPlayerBlocker.Visibility = Visibility.Hidden;
                _cpuTurnRunning = false;

                if (_queueNextCpuTurn)
                {
                    _dispatcher.BeginInvoke(new Action(async () => await PlayCpuTurnIfNeededAsync()));
                }
            }
        }

        #endregion
    }
}
