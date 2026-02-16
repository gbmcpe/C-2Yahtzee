using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

///Class for the scoreboard to show values Beau's part.
namespace YahtzeeGame
{
    #region Classes

    /// <summary>
    /// Represents one row on the scoreboard, and defines a type used to hold the data for a single scoreboard row.
    /// </summary>
    public class ScoreRow 
    {
        /// <summary>
        /// The category name shown in the first column.
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// allows null so the grid can show blank instead of 0 on player column.
        /// </summary>
        public int? Player { get; set; }

        /// <summary>
        /// Tracks if category is already used to prevent scoring twice.
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Sets category label in the first column, makes player column blank until score is selected.
        /// </summary>
        /// <param name="score"></param>
        public ScoreRow(string score)
        {
            Score = score;   ///stores category name.
            Player = null;   ///starts blank.
            IsUsed = false;  ///category not used yet.
        }
    }

    /// <summary>
    /// Represents the entire scoreboard.
    /// </summary>
    public class ScoreBoard
    {
        /// <summary>
        /// List of score rows and bind the grid.
        /// </summary>
        public List<ScoreRow> Rows { get; set; }

        /// <summary>
        /// store current players name.
        /// </summary>
        public string PlayerName { get; private set; }

        /// <summary>
        /// timer checks if turn finished.
        /// </summary>
        private readonly DispatcherTimer uiTimer;

        /// <summary>
        /// flag prevents re-applying scores multiple times while rolls left stays at 0.
        /// </summary>
        private bool appliedThisTurn;

        /// <summary>
        /// builds the scoreboard
        /// </summary>
        public ScoreBoard()
        {
            /// Make default score rows.
            Rows = CreateDefaultRows();

            /// create timer to poll UI state.
            uiTimer = new DispatcherTimer();

            /// set how often ui is checked.
            uiTimer.Interval = System.TimeSpan.FromMilliseconds(250);
            
            /// attach tick event so code runs each interval.
            uiTimer.Tick += UiTimer_Tick;

            /// start timer so scoreboard can update automatically.
            uiTimer.Start();

            /// allow first scoring apply once roll counter reaches 0.
            appliedThisTurn = false;
        }

        /// <summary>
        /// creates and returns the standard scoreboard layout
        /// </summary>
        /// <returns></returns>
        private List<ScoreRow> CreateDefaultRows()
        {
            /// Creates a new list that will contain all scoreboard rows in order.
            return new List<ScoreRow>
            {
                new ScoreRow("Ones"),
                new ScoreRow("Twos"),
                new ScoreRow("Threes"),
                new ScoreRow("Fours"),
                new ScoreRow("Fives"),
                new ScoreRow("Sixes"),

                new ScoreRow("Sum"),
                new ScoreRow("Bonus"),

                new ScoreRow("Three of a Kind"),
                new ScoreRow("Four of a Kind"),
                new ScoreRow("Full House"),
                new ScoreRow("Small Straight"),
                new ScoreRow("Large Straight"),
                new ScoreRow("Chance"),
                new ScoreRow("YAHTZEE"),

                new ScoreRow("Total Score")
            };
        }

        /// <summary>
        /// initialized scoreboard for player when a game starts.
        /// </summary>
        /// <param name="playerName"></param>
        public void InitializePlayer(string playerName)
        {
            ///stores player name so we know whos playing.
            PlayerName = playerName;

            foreach (ScoreRow row in Rows)
            {
                ///clear existing score so the board is empty at start.
                row.Player = null;

                ///reset category usage so all categorys are availible.
                row.IsUsed = false;
            }

            /// reset apply flag so the next turn can apply again.
            appliedThisTurn = false;
        }

        /// <summary>
        /// When roll counter hits 0 calculate scores and populate the board.
        /// </summary>
        private void UiTimer_Tick(object sender, System.EventArgs e)
        {
            /// get the current MainWindow (this is your window where controls exist).
            Window window = Application.Current?.MainWindow;

            /// if window not ready yet, do nothing.
            if (window == null) return;

            /// find the rolls left label by its x:Name in MainWindow.xaml.
            Label rollLabel = window.FindName("lblTimesRolled") as Label;

            /// if label not found, do nothing.
            if (rollLabel == null) return;

            /// read roll counter from ContentStringFormat because MainWindow sets that too.
            string rollsLeft = rollLabel.ContentStringFormat;

            /// if rollsLeft is not "0", then turn is not finished yet.
            if (rollsLeft != "0")
            {
                /// allow apply again when it eventually reaches 0.
                appliedThisTurn = false;
                return;
            }

            /// if already applied while rollsLeft is 0, do nothing.
            if (appliedThisTurn) return;

            /// read the 5 dice values.
            int[] dice = ReadDiceValuesFromUI(window);

            /// if dice failed to read, do nothing.
            if (dice == null) return;

            /// apply all category scores into scoreboard.
            ApplyScoresFromDice(dice);

            /// mark applied only once per turn end.
            appliedThisTurn = true;

            /// refresh the bound view so DataGrid updates.
            CollectionViewSource.GetDefaultView(Rows)?.Refresh();
        }

        /// <summary>
        /// Reads dice values from the UI where DisplayDice stores the values.
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        private int[] ReadDiceValuesFromUI(Window window)
        {
            /// make array for dice.
            int[] values = new int[5];

            /// loop through dice 1-5.
            for (int i = 1; i <= 5; i++)
            {
                /// find checkbox.
                CheckBox cb = window.FindName($"cbDie{i}") as CheckBox;

                /// if checkbox missing, fail.
                if (cb == null) return null;

                /// read the die value stored as text.
                string text = cb.ContentStringFormat;

                /// parse the number safely.
                if (!int.TryParse(text, out int dieValue)) return null;

                /// store into array.
                values[i - 1] = dieValue;
            }

            /// return the dice values.
            return values;
        }

        /// <summary>
        /// Calculates and fills the scoreboard rows using the final dice results.
        /// </summary>
        /// <param name="dice"></param>
        private void ApplyScoresFromDice(int[] dice)
        {
            /// loop each category row and calculate score.
            foreach (ScoreRow row in Rows)
            {
                /// do not overwrite locked rows.
                if (row.IsUsed) continue;

                /// set player column to the calculated score for that category.
                row.Player = CalculateCategoryScore(row.Score, dice);
            }

            /// calculate upper section sum.
            int upperSum =
                GetRowValue("Ones") +
                GetRowValue("Twos") +
                GetRowValue("Threes") +
                GetRowValue("Fours") +
                GetRowValue("Fives") +
                GetRowValue("Sixes");
            
            /// calculate bonus based on Yahtzee rule.
            int bonus = upperSum >= 63 ? 35 : 0;

            /// calculate lower section sum.
            int lowerSum =
                GetRowValue("Three of a Kind") +
                GetRowValue("Four of a Kind") +
                GetRowValue("Full House") +
                GetRowValue("Small Straight") +
                GetRowValue("Large Straight") +
                GetRowValue("Chance") +
                GetRowValue("YAHTZEE");

            /// write calculated rows directly.
            SetRowDirect("Sum", upperSum);
            SetRowDirect("Bonus", bonus);
            SetRowDirect("Total Score", upperSum + bonus + lowerSum);
        }

        /// <summary>
        /// Calculates score for category using the dice values.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="dice"></param>
        /// <returns></returns>
        private int CalculateCategoryScore(string category, int[] dice)
        {
            /// Sum of all dice values.
            int sumAll = dice.Sum();

            /// count how many dice match a specific face value.
            int Count(int face)
            {
                int count = 0;
                foreach (int d in dice)
                {
                    if (d == face)
                        count++;
                }
                return count;
            }

            /// checks if there are dice of the same value.
            bool HasOfAKind(int n)
            {
                return dice.GroupBy(d => d).Any(g => g.Count() >= n);
            }

            ///checks for full house pattern.
            bool IsFullHouse()
            {
                var groups = dice.GroupBy(d => d).Select(g => g.Count()).OrderBy(x => x).ToArray();
                return groups.Length == 2 && groups[0] == 2 && groups[1] == 3;
            }

            ///checks for any valid small straight.
            bool IsSmallStraight()
            {
                var set = dice.Distinct().OrderBy(x => x).ToArray();
                int[][] straights =
                {
            new int[] { 1, 2, 3, 4 },
            new int[] { 2, 3, 4, 5 },
            new int[] { 3, 4, 5, 6 }
        };

                foreach (var straight in straights)
                {
                    bool found = true;
                    foreach (int value in straight)
                    {
                        if (!set.Contains(value))
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                        return true;
                }
                return false;
            }

            ///checks for large straight.
            bool IsLargeStraight()
            {
                var set = dice.Distinct().OrderBy(x => x).ToArray();
                return set.SequenceEqual(new int[] { 1, 2, 3, 4, 5 }) ||
                       set.SequenceEqual(new int[] { 2, 3, 4, 5, 6 });
            }

            /// He said not to do it but im doing it.
            switch (category)
            {
                case "Ones":
                    return Count(1) * 1;

                case "Twos":
                    return Count(2) * 2;

                case "Threes":
                    return Count(3) * 3;

                case "Fours":
                    return Count(4) * 4;

                case "Fives":
                    return Count(5) * 5;

                case "Sixes":
                    return Count(6) * 6;

                case "Three of a Kind":
                    return HasOfAKind(3) ? sumAll : 0;

                case "Four of a Kind":
                    return HasOfAKind(4) ? sumAll : 0;

                case "Full House":
                    return IsFullHouse() ? 25 : 0;

                case "Small Straight":
                    return IsSmallStraight() ? 30 : 0;

                case "Large Straight":
                    return IsLargeStraight() ? 40 : 0;

                case "Chance":
                    return sumAll;

                case "YAHTZEE":
                    return dice.Distinct().Count() == 1 ? 50 : 0;

                case "Sum":
                case "Bonus":
                case "Total Score":
                    return 0;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets row Player value.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private int GetRowValue(string category)
        {
            /// find row by its Score name.
            ScoreRow row = Rows.FirstOrDefault(r => r.Score == category);

            /// return 0 if row missing or Player is null.
            return row?.Player ?? 0;
        }

        /// <summary>
        /// Sets a row Player value directly.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        private void SetRowDirect(string category, int value)
        {
            /// find row by name.
            ScoreRow row = Rows.FirstOrDefault(r => r.Score == category);

            /// if not found, do nothing.
            if (row == null) return;

            /// set the player value.
            row.Player = value;
        }
    }

    #endregion
}
/*need to fix score card counting already kept dice and dice rolls in background of the already checked die.
 * make player header change to inputed player name but need player initilization.
 */