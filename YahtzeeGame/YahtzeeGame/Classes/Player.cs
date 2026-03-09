using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahtzeeGame;

namespace YahtzeeGame
{
    public class Player
    {

        //PlayerPos will start at 0.
        private int playerPos;
        private string playerName;
        private bool computerPlayer;
        private ScoreCard playerScores;
        private int selectedScore;

        //constructors
        public Player(int Pos) 
        { 
            PlayerPos = Pos;
            PlayerName = ($"Player {Pos}");
            ComputerPlayer = false;
            PlayerScores = new ScoreCard();
        }

        public Player(int Pos, string Name)
        {
            PlayerPos = Pos;
            PlayerName = Name;
            ComputerPlayer = false;
            PlayerScores = new ScoreCard();
        }

        public Player(int Pos, string Name, bool Comp)
        {
            PlayerPos = Pos;
            PlayerName = Name;
            ComputerPlayer = Comp; 
            PlayerScores = new ScoreCard();
        }

        //Set and get  the name property 
        public string PlayerName
        {
            get { return PlayerName1; }
            set { PlayerName1 = value; }
        }

        public int PlayerPos { get => playerPos; set => playerPos = value; }
        public string PlayerName1 { get => playerName; set => playerName = value; }
        public bool ComputerPlayer { get => computerPlayer; set => computerPlayer = value; }
        public ScoreCard PlayerScores { get => playerScores; set => playerScores = value; }
        public int SelectedScore { get => selectedScore; set => selectedScore = value; }



        //void set()
        //{
        //    List<int> ScoreList = new List<int>();
        //    ScoreList.Add(1);
        //    if (ScoreList.Contains(1)
        //        {

        //        int Score = ScoreList.
        //    }
        //    ;
        //}


        private void DisplayScore()
        {
            
        }


        private void UpdateScoreColumn()
        {
            
        }



    }
}

public abstract class CPUPlayer
{


    #region Bot Decisions

    /// <summary>
    /// /// Decides which dice to keep (true = hold / keep, false = reroll).
    /// </summary>
    /// <param name="diceValues"></param>
    /// <param name="rollsLeft"></param>
    /// <returns></returns>
    public abstract bool[] ChooseDice(int[] diceValues, int rollsLeft);

    /// <summary>
    /// Chooses the best scoring category available on the scorecard.
    /// </summary>
    /// <param name="dice"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    public abstract string ChooseCategory(int[] dice, ScoreCard card);

    /// <summary>
    /// Applies the chosen category to the ScoreCard.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="dice"></param>
    /// <param name="card"></param>
    public virtual void ApplyScore(string category, int[] dice, ScoreCard card)
    {
        /// Update the score preview values from the current dice before locking in the score.
        UpdateScorePreview(dice, card);

        /// Apply the chosen category using the ScoreCard computer path.
        switch (category)
        {
            case "aces":
                card.AcesSelected(true);
                break;

            case "twos":
                card.TwosSelected(true);
                break;

            case "threes":
                card.ThreesSelected(true);
                break;

            case "fours":
                card.FoursSelected(true);
                break;

            case "fives":
                card.FivesSelected(true);
                break;

            case "sixes":
                card.SixesSelected(true);
                break;

            case "threeKind":
                card.ThreeOfAKindSelected(true);
                break;

            case "fourKind":
                card.FourOfAKindSelected(dice, true);
                break;

            case "fullHouse":
                card.FullHouseSelected(true);
                break;

            case "smallStraight":
                card.SmallStraightSelected(true);
                break;

            case "largeStraight":
                card.LargeStraightSelected(true);
                break;

            case "yahtzee":
                card.YahtzeeSelected(true);
                break;

            case "chance":
                card.ChanceSelected(dice, true);
                break;
        }

        /// Show only the locked in scores on the card.
        card.ShowSelectedScores();
    }

    #endregion

    #region Standard Category Keys

    /// <summary>
    /// Standard category keys for bots to use.
    /// </summary>
    public static class Categories
    {
        public const string Aces = "aces";
        public const string Twos = "twos";
        public const string Threes = "threes";
        public const string Fours = "fours";
        public const string Fives = "fives";
        public const string Sixes = "sixes";

        public const string ThreeKind = "threeKind";
        public const string FourKind = "fourKind";
        public const string FullHouse = "fullHouse";
        public const string SmallStraight = "smallStraight";
        public const string LargeStraight = "largeStraight";
        public const string Yahtzee = "yahtzee";
        public const string Chance = "chance";
    }

    #endregion

    #region Dice Helpers

    /// <summary>
    /// Returns the sum of all dice.
    /// </summary>
    /// <param name="dice"></param>
    /// <returns></returns>
    public int SumAll(int[] dice)
    {
        int sum = 0;
        for (int i = 0; i < dice.Length; i++)
        {
            sum += dice[i];
        }
        return sum;
    }

    /// <summary>
    /// /// Returns the maximum die value.
    /// </summary>
    /// <param name="dice"></param>
    /// <returns></returns>
    public int MaxDie(int[] dice)
    {
        int max = dice[0];
        for (int i = 1; i < dice.Length; i++)
        {
            if (dice[i] > max) max = dice[i];
        }
        return max;
    }

    /// <summary>
    /// Counts how many of each face appears.
    /// </summary>
    /// <param name="dice"></param>
    /// <returns></returns>
    public int[] CountFaces(int[] dice)
    {
        int[] counts = new int[7]; /// 1..6
        for (int i = 0; i < dice.Length; i++)
        {
            counts[dice[i]]++;
        }
        return counts;
    }

    /// <summary>
    /// Returns a face 1-6 that occurs at least x times otherwise -1.
    /// </summary>
    /// <param name="counts"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public int FaceWithAtLeast(int[] counts, int x)
    {
        for (int f = 6; f >= 1; f--)
        {
            if (counts[f] >= x) return f;
        }
        return -1;
    }

    /// <summary>
    /// Returns faces that occur exactly x times.
    /// </summary>
    /// <param name="counts"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public List<int> FacesWithExact(int[] counts, int x)
    {
        var list = new List<int>();
        for (int f = 1; f <= 6; f++)
        {
            if (counts[f] == x) list.Add(f);
        }
        return list;
    }

    #endregion

    #region Pattern Helpers

    /// <summary>
    /// Checks whether a full house exists using a counts array.
    /// </summary>
    /// <param name="counts"></param>
    /// <returns></returns>
    public bool IsFullHouse(int[] counts)
    {
        bool has3 = false;
        bool has2 = false;

        for (int i = 1; i <= 6; i++)
        {
            if (counts[i] == 3) has3 = true;
            if (counts[i] == 2) has2 = true;
        }

        return has3 && has2;
    }

    /// <summary>
    /// Checks whether a small straight exists (any 4-length run).
    /// </summary>
    /// <param name="dice"></param>
    /// <returns></returns>
    public bool IsSmallStraight(int[] dice)
    {
        bool[] seen = new bool[7];

        for (int i = 0; i < dice.Length; i++)
        {
            seen[dice[i]] = true;
        }

        bool a = seen[1] && seen[2] && seen[3] && seen[4];
        bool b = seen[2] && seen[3] && seen[4] && seen[5];
        bool c = seen[3] && seen[4] && seen[5] && seen[6];

        return a || b || c;
    }

    /// <summary>
    /// Checks whether a large straight exists (1-5 or 2-6).
    /// </summary>
    /// <param name="dice"></param>
    /// <returns></returns>
    public bool IsLargeStraight(int[] dice)
    {
        bool[] seen = new bool[7];
        int unique = 0;

        for (int i = 0; i < dice.Length; i++)
        {
            int d = dice[i];
            if (!seen[d])
            {
                seen[d] = true;
                unique++;
            }
        }

        if (unique != 5) return false;

        bool a = seen[1] && seen[2] && seen[3] && seen[4] && seen[5];
        bool b = seen[2] && seen[3] && seen[4] && seen[5] && seen[6];

        return a || b;
    }

    #endregion

    #region Hold Helpers

    /// <summary>
    /// Returns a hold array that keeps all dice.
    /// </summary>
    /// <returns></returns>
    public bool[] KeepAll()
    {
        return new bool[] { true, true, true, true, true };
    }

    /// <summary>
    /// Returns a hold array that keeps no dice.
    /// </summary>
    /// <returns></returns>
    public bool[] KeepNone()
    {
        return new bool[] { false, false, false, false, false };
    }

    /// <summary>
    /// Keeps only dice that match a specific face value.
    /// </summary>
    /// <param name="dice"></param>
    /// <param name="face"></param>
    /// <returns></returns>
    public bool[] KeepFace(int[] dice, int face)
    {
        bool[] keep = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            keep[i] = (dice[i] == face);
        }
        return keep;
    }

    /// <summary>
    /// Keeps dice if their value is in the provided face list.
    /// </summary>
    /// <param name="dice"></param>
    /// <param name="faces"></param>
    /// <returns></returns>
    public bool[] KeepFaces(int[] dice, List<int> faces)
    {
        bool[] keep = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            keep[i] = faces.Contains(dice[i]);
        }
        return keep;
    }

    /// <summary>
    /// Returns holds for a 4-length straight (1234/2345/3456) if it doesn't null.
    /// </summary>
    /// <param name="dice"></param>
    /// <returns></returns>
    public bool[] HoldFor4Straight(int[] dice)
    {
        bool[] seen = new bool[7];
        for (int i = 0; i < dice.Length; i++)
        {
            seen[dice[i]] = true;
        }

        bool has1234 = seen[1] && seen[2] && seen[3] && seen[4];
        bool has2345 = seen[2] && seen[3] && seen[4] && seen[5];
        bool has3456 = seen[3] && seen[4] && seen[5] && seen[6];

        if (!has1234 && !has2345 && !has3456) return null;

        bool[] keep = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            int d = dice[i];

            if (has1234) keep[i] = (d >= 1 && d <= 4);
            else if (has2345) keep[i] = (d >= 2 && d <= 5);
            else keep[i] = (d >= 3 && d <= 6);
        }

        return keep;
    }

    #endregion

    #region Scoring Preview

    /// <summary>
    /// Updates the ScoreCard preview values from the current dice.
    /// </summary>
    /// <param name="dice"></param>
    /// <param name="card"></param>
    public void UpdateScorePreview(int[] dice, ScoreCard card)
    {
        /// Reset upper section preview values.
        card.Aces = SumFace(dice, 1);
        card.Twos = SumFace(dice, 2);
        card.Threes = SumFace(dice, 3);
        card.Fours = SumFace(dice, 4);
        card.Fives = SumFace(dice, 5);
        card.Sixes = SumFace(dice, 6);

        /// Count faces for validations.
        int[] counts = CountFaces(dice);

        /// Sum all dice for total-based categories.
        int sumAll = SumAll(dice);

        /// Update lower section preview values.
        card.ThreeOfAKind = counts.Any(c => c >= 3) ? sumAll : 0;
        card.FourOfAKind = counts.Any(c => c >= 4) ? sumAll : 0;
        card.FullHouse = IsFullHouse(counts) ? 25 : 0;
        card.SmallStraight = IsSmallStraight(dice) ? 30 : 0;
        card.LargeStraight = IsLargeStraight(dice) ? 40 : 0;
        card.Yahtzee = counts.Any(c => c == 5) ? 50 : 0;

        /// Update bonus preview.
        card.BonusConditionsMet();
    }

    /// <summary>
    /// Computes what a category would score for the current dice.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="dice"></param>
    /// <returns></returns>
    public int PreviewScore(string category, int[] dice)
    {
        int[] counts = CountFaces(dice);
        int sumAll = SumAll(dice);

        bool three = false;
        bool four = false;
        bool yahtzee = false;

        for (int i = 1; i <= 6; i++)
        {
            if (counts[i] >= 3) three = true;
            if (counts[i] >= 4) four = true;
            if (counts[i] == 5) yahtzee = true;
        }

        if (category == Categories.Aces) return SumFace(dice, 1);
        if (category == Categories.Twos) return SumFace(dice, 2);
        if (category == Categories.Threes) return SumFace(dice, 3);
        if (category == Categories.Fours) return SumFace(dice, 4);
        if (category == Categories.Fives) return SumFace(dice, 5);
        if (category == Categories.Sixes) return SumFace(dice, 6);

        if (category == Categories.ThreeKind) return three ? sumAll : 0;
        if (category == Categories.FourKind) return four ? sumAll : 0;
        if (category == Categories.FullHouse) return IsFullHouse(counts) ? 25 : 0;
        if (category == Categories.SmallStraight) return IsSmallStraight(dice) ? 30 : 0;
        if (category == Categories.LargeStraight) return IsLargeStraight(dice) ? 40 : 0;
        if (category == Categories.Yahtzee) return yahtzee ? 50 : 0;
        if (category == Categories.Chance) return sumAll;

        return 0;
    }

    /// <summary>
    /// Sums only dice that match the given face.
    /// </summary>
    /// <param name="dice"></param>
    /// <param name="face"></param>
    /// <returns></returns>
    public int SumFace(int[] dice, int face)
    {
        int sum = 0;
        for (int i = 0; i < dice.Length; i++)
        {
            if (dice[i] == face) sum += dice[i];
        }
        return sum;
    }

    /// <summary>
    /// Returns a minimum “good score” threshold for upper-section categories.
    /// </summary>
    /// <param name="upper"></param>
    /// <returns></returns>
    public int UpperThreshold(string upper)
    {
        if (upper == Categories.Sixes) return 12;
        if (upper == Categories.Fives) return 10;
        if (upper == Categories.Fours) return 8;
        if (upper == Categories.Threes) return 6;
        if (upper == Categories.Twos) return 4;
        return 3;
    }

    /// <summary>
    /// Builds a list of all categories not yet scored.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public List<string> GetAvailableCategories(ScoreCard card)
    {
        var list = new List<string>();

        if (!card.AcesScored) list.Add(Categories.Aces);
        if (!card.TwosScored) list.Add(Categories.Twos);
        if (!card.ThreesScored) list.Add(Categories.Threes);
        if (!card.FoursScored) list.Add(Categories.Fours);
        if (!card.FivesScored) list.Add(Categories.Fives);
        if (!card.SixesScored) list.Add(Categories.Sixes);
        if (!card.ThreeOfAKindScored) list.Add(Categories.ThreeKind);
        if (!card.FourOfAKindScored) list.Add(Categories.FourKind);
        if (!card.FullHouseScored) list.Add(Categories.FullHouse);
        if (!card.SmallStraightScored) list.Add(Categories.SmallStraight);
        if (!card.LargeStraightScored) list.Add(Categories.LargeStraight);
        if (!card.YahtzeeScored) list.Add(Categories.Yahtzee);
        if (!card.ChanceScored) list.Add(Categories.Chance);

        return list;
    }

    #endregion
}
