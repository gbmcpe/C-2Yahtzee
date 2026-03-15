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
        private HardAIV2 _hardAI = new HardAIV2();
        #endregion

        private CPUPlayer _bot;

        private bool _cpuTurnRunning = false;
        private int _cpuStepDelayMs = 600;
        private bool _queueNextCpuTurn = false;

        private readonly GameManager _game;
        private readonly Func<Player> _getCurrentPlayer;

        private readonly GroupBox _gbPlayerBlocker;
        private readonly Label _lblTimesRolled;
        private readonly TextBox _tbCurrentPlayer;

        private readonly CheckBox _cbDie1;
        private readonly CheckBox _cbDie2;
        private readonly CheckBox _cbDie3;
        private readonly CheckBox _cbDie4;
        private readonly CheckBox _cbDie5;

        private readonly Func<bool[]> _checkDice;
        private readonly Action _displayDiceSet;
        private readonly Action<bool> _diceActivation;
        private readonly Action<bool> _scoreCardActivated;
        private readonly Action _refactorBoard;
        private readonly Action _fillBoxes;
        private readonly Action _nextTurn;

        private readonly Dispatcher _dispatcher;

        #endregion

        #region Constructor

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

        private bool IsHardAiPlayer(Player p)
        {
            return p != null
                   && p.PlayerName != null
                   && p.PlayerName.Contains("Hard AI");
        }

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

        public bool IsCpuPlayer(Player p)
        {
            return p != null && p.ComputerPlayer;
        }

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
