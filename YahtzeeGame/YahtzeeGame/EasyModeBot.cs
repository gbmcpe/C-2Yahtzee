using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{ 
    /// <summary>
    /// Main files for EasyModeBot.
    /// </summary>
    internal class EasyModeBot
    {
        #region Methods

        /// <summary>
        /// Decides which dice to keep
        /// </summary>
        /// <param name="diceValues"></param>
        /// <param name="rollsLeft"></param>
        /// <returns></returns>
        public bool[] ChooseDice(int[] diceValues, int rollsLeft)
        {
            /// Count face value of dice.
            int[] counts = CountFaces(diceValues);

            /// Keep everything if yahtzee.
            if (counts.Any(c => c == 5)) return KeepAll();
            /// Keep if Large straight.
            if (IsLargeStraight(diceValues)) return KeepAll();
            /// Keep if full house.
            if (IsFullHouse(counts)) return KeepAll();

            /// Find if 4 dice have the same face.
            int face4 = FaceWithAtLeast(counts, 4);
            /// If found, keep those dice.
            if (face4 != -1) return KeepFace(diceValues, face4);

            /// Find a dice with 3 of the same face.
            int face3 = FaceWithAtLeast(counts, 3);
            /// If found, keep those dice.
            if (face3 != -1) return KeepFace(diceValues, face3);

            /// Try to hold a 4-length straight pattern.
            bool[] straightHold = HoldFor4Straight(diceValues);
            /// If a straight hold exists keep.
            if (straightHold != null) return straightHold;

            /// Find all pairs.
            List<int> pairs = FacesWithExact(counts, 2);
            /// If 2 pairs exist keep.
            if (pairs.Count >= 2) return KeepFaces(diceValues, pairs);
            /// If 1 pair exists keep.
            if (pairs.Count == 1) return KeepFace(diceValues, pairs[0]);

            /// keep the highest die as standard.
            int max = diceValues.Max();
            /// Hold only the dice matching that highest value.
            return KeepFace(diceValues, max);
        }

        /// <summary>
        /// Chooses the best scoring category available on the scorecard.
        /// </summary>
        /// <param name="dice"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public string ChooseCategory(int[] dice, ScoreCard card)
        {
            /// Get a list of categories that are not yet scored.
            List<string> available = GetAvailableCategories(card);

            /// Prepare a list of pairs.
            List<Tuple<string, int>> scored = new List<Tuple<string, int>>();
            /// Loop through each available category.
            foreach (string cat in available)
            {
                /// Add and store the score for that category.
                scored.Add(Tuple.Create(cat, PreviewScore(cat, dice)));
            }

            /// Sort options by highest score.
            scored = scored.OrderByDescending(x => x.Item2).ToList();

            /// Preferred high-value categories.
            string[] prefs = { "yahtzee", "largeStraight", "fullHouse", "fourKind", "smallStraight", "threeKind" };
            /// Try to take preferred category if it scores > 0.
            foreach (string pref in prefs)
            {
                /// Scan each scored option.
                foreach (var item in scored)
                {
                    /// keep if score is worht points and matches preferred design.
                    if (item.Item1 == pref && item.Item2 > 0) return pref;
                }
            }

            /// Define upper section categories to try next.
            string[] uppers = { "sixes", "fives", "fours", "threes", "twos", "aces" };
            /// Loop through uppers looking for a decent score.
            foreach (string upper in uppers)
            {
                /// Skip if category already used.
                if (!available.Contains(upper)) continue;

                /// Compute projected score for this upper category.
                int s = PreviewScore(upper, dice);
                /// Get the minimum threshold for “good enough.”
                int threshold = UpperThreshold(upper);

                /// If the score meets requirements keep it.
                if (s >= threshold) return upper;
            }

            /// If best available adds points, keep it.
            if (scored.Count > 0 && scored[0].Item2 > 0) return scored[0].Item1;

            /// Define a “dump order” for when everything scores 0.
            string[] dumps =
            {
                /// Dump high difficulty categories first.
                "yahtzee", "largeStraight", "fullHouse", "fourKind", "smallStraight", "threeKind",
                /// Then dump upper section.
                "aces", "twos", "threes", "fours", "fives", "sixes",
                /// Chance is last because it always scores something (but sometimes low).
                "chance"
            };

            /// Choose the first available dump category.
            foreach (string dump in dumps)
            {
                /// If it’s still available, return.
                if (available.Contains(dump)) return dump;
            }

            /// If nothing availible return the first available category.
            return available[0];
        }

        /// <summary>
        /// Applies the chosen category to the ScoreCard without MessageBox
        /// </summary>
        /// <param name="category"></param>
        /// <param name="dice"></param>
        /// <param name="card"></param>
        public void ApplyScore(string category, int[] dice, ScoreCard card)
        {
            /// Calculate points for the selected category.
            int points = PreviewScore(category, dice);

            /// Apply points to the correct field and mark it scored.
            switch (category)
            {
                
                case "aces":
                    card.aces = points; card.acesScored = true; card.totalScore += points; break;
                
                case "twos":
                    card.twos = points; card.twosScored = true; card.totalScore += points; break;
                
                case "threes":
                    card.threes = points; card.threesScored = true; card.totalScore += points; break;
                
                case "fours":
                    card.fours = points; card.foursScored = true; card.totalScore += points; break;
                
                case "fives":
                    card.fives = points; card.fivesScored = true; card.totalScore += points; break;
                
                case "sixes":
                    card.sixes = points; card.sixesScored = true; card.totalScore += points; break;

                
                case "threeKind":
                    card.threeOfAKind = points; card.threeOfAKindScored = true; card.totalScore += points; break;
               
                case "fourKind":
                    card.fourOfAKind = points; card.fourOfAKindScored = true; card.totalScore += points; break;
               
                case "fullHouse":
                    card.fullHouse = points; card.fullHouseScored = true; card.totalScore += points; break;
                
                case "smallStraight":
                    card.smallStraight = points; card.smallStraightScored = true; card.totalScore += points; break;
                
                case "largeStraight":
                    card.largeStraight = points; card.largeStraightScored = true; card.totalScore += points; break;
                
                case "yahtzee":
                    card.yahtzee = points; card.yahtzeeScored = true; card.totalScore += points; break;
                
                case "chance":
                    card.chance = points; card.chanceScored = true; card.totalScore += points; break;
            }
        }

        #endregion

        #region Scoring & Categories

        /// <summary>
        /// Builds a list of all categories not yet scored.
        /// </summary>
        /// <param name="category">The player's ScoreCard object.</param>
        /// <returns>A list of available category names.</returns>
        private static List<string> GetAvailableCategories(ScoreCard category)
        {
            /// Create a list to store available categories.
            var list = new List<string>();

            /// Add each category if it has not been scored yet.
            if (!category.acesScored) list.Add("aces");
            if (!category.twosScored) list.Add("twos");
            if (!category.threesScored) list.Add("threes");
            if (!category.foursScored) list.Add("fours");
            if (!category.fivesScored) list.Add("fives");
            if (!category.sixesScored) list.Add("sixes");
            if (!category.threeOfAKindScored) list.Add("threeKind");
            if (!category.fourOfAKindScored) list.Add("fourKind");
            if (!category.fullHouseScored) list.Add("fullHouse");
            if (!category.smallStraightScored) list.Add("smallStraight");
            if (!category.largeStraightScored) list.Add("largeStraight");
            if (!category.yahtzeeScored) list.Add("yahtzee");
            if (!category.chanceScored) list.Add("chance");

            /// Return the completed list.
            return list;
        }

        /// Returns a minimum “good score” threshold for upper-section categories.
        private static int UpperThreshold(string upper)
        {
            
            if (upper == "sixes") return 12;
            
            if (upper == "fives") return 10;
            
            if (upper == "fours") return 8;
            
            if (upper == "threes") return 6;
            
            if (upper == "twos") return 4;
            
            return 3;
        }

        /// Computes what a category would score for the current dice.
        private static int PreviewScore(string category, int[] dice) 
        {
            /// Count faces for validations.
            int[] counts = CountFaces(dice);

            /// Sum all dice for total-based scoring.
            int sumAll = dice.Sum();
            /// Determine if 3 of kind exists.
            bool three = counts.Any(c => c >= 3);
            /// Determine if 4 of kind exists.
            bool four = counts.Any(c => c >= 4);
            /// Determine if yahtzee exists.
            bool yahtzee = counts.Any(c => c == 5);

            /// Upper section: sum only matching faces.
            if (category == "aces") return dice.Where(d => d == 1).Sum();
            if (category == "twos") return dice.Where(d => d == 2).Sum();
            if (category == "threes") return dice.Where(d => d == 3).Sum();
            if (category == "fours") return dice.Where(d => d == 4).Sum();
            if (category == "fives") return dice.Where(d => d == 5).Sum();
            if (category == "sixes") return dice.Where(d => d == 6).Sum();

            /// Three of a kind.
            if (category == "threeKind") return three ? sumAll : 0;
            /// Four of a kind.
            if (category == "fourKind") return four ? sumAll : 0;
            /// Full house.
            if (category == "fullHouse") return IsFullHouse(counts) ? 25 : 0;
            /// Small straight.
            if (category == "smallStraight") return IsSmallStraight(dice) ? 30 : 0;
            /// Large straight.
            if (category == "largeStraight") return IsLargeStraight(dice) ? 40 : 0;
            /// Yahtzee.
            if (category == "yahtzee") return yahtzee ? 50 : 0;
            /// Chance always sum all dice.
            if (category == "chance") return sumAll;

            /// Unknown category 0 points.
            return 0;
        }

        #endregion

        #region Hold Logic 

        /// Returns a hold array that keeps all dice.
        private static bool[] KeepAll() 
        {
            /// Return an array of five true values.
            return new bool[] { true, true, true, true, true };
        }

        /// Keeps only dice that match a specific face value.
        private static bool[] KeepFace(int[] dice, int face)
        {
            /// Create output hold array.
            bool[] keep = new bool[5];
            /// Loop through each die.
            for (int i = 0; i < 5; i++) keep[i] = (dice[i] == face);
            /// Return hold array.
            return keep;
        }

        /// Keeps dice if their value is in the provided face list.
        private static bool[] KeepFaces(int[] dice, List<int> faces) 
        {
            /// Create output hold array.
            bool[] keep = new bool[5];
            /// Loop through each die and keep if face is in list.
            for (int i = 0; i < 5; i++) keep[i] = faces.Contains(dice[i]);
            /// Return hold array.
            return keep;
        }

        /// Returns holds for a 4-length straight chase (1234/2345/3456), otherwise null.
        private static bool[] HoldFor4Straight(int[] dice)
        {
            /// Build a set of distinct dice for checks.
            HashSet<int> set = new HashSet<int>(dice.Distinct());

            /// Find 1 2 3 4.
            bool has1234 = set.Contains(1) && set.Contains(2) && set.Contains(3) && set.Contains(4);
            /// Find 2 3 4 5.
            bool has2345 = set.Contains(2) && set.Contains(3) && set.Contains(4) && set.Contains(5);
            /// Find 3 4 5 6.
            bool has3456 = set.Contains(3) && set.Contains(4) && set.Contains(5) && set.Contains(6);

            /// If no 4-run exists, return null so other strategies can run.
            if (!has1234 && !has2345 && !has3456) return null;

            /// Create hold array.
            bool[] keep = new bool[5];
            /// Loop through dice positions.
            for (int i = 0; i < 5; i++)
            {
                /// Store current die value.
                int d = dice[i];
                /// If 1234 exists, keep values 1-4.
                if (has1234) keep[i] = (d >= 1 && d <= 4);
                /// Else if 2345 exists, keep values 2-5.
                else if (has2345) keep[i] = (d >= 2 && d <= 5);
                /// Else keep values 3-6.
                else keep[i] = (d >= 3 && d <= 6);
            }

            /// Return straight-chase hold pattern.
            return keep;
        }

        #endregion

        #region Utility 

        /// Counts how many of each face appears; indices 1..6 are used.
        private static int[] CountFaces(int[] dice) 
        {
            /// Create counts array.
            int[] counts = new int[7]; /// 1..6
            
            /// Loop through dice values.
            for (int i = 0; i < dice.Length; i++)
            {
                /// Increment that face.
                counts[dice[i]]++;
            }
            /// Return counts array.
            return counts;
        }

        /// Checks whether a full house exists using a counts array.
        private static bool IsFullHouse(int[] counts)
        {
            /// Find of a triple.
            bool has3 = false;
            /// Find 2 of a pair.
            bool has2 = false;
            /// Scan faces 1 - 6.
            for (int i = 1; i <= 6; i++)
            {
                /// Detect triple.
                if (counts[i] == 3) has3 = true;
                /// Detect pair.
                if (counts[i] == 2) has2 = true;
            }
            /// Full house requires both.
            return has3 && has2;
        }

        /// Checks whether a large straight exists (1-5 or 2-6).
        private static bool IsLargeStraight(int[] dice) 
        {
            /// Distinct + sort dice for sequence check.
            int[] set = dice.Distinct().OrderBy(x => x).ToArray();
            /// Must have five unique numbers.
            if (set.Length != 5) return false;

            /// Check 1 2 3 4 5.
            bool a = set[0] == 1 && set[1] == 2 && set[2] == 3 && set[3] == 4 && set[4] == 5;
            /// Check 2 3 4 5 6.
            bool b = set[0] == 2 && set[1] == 3 && set[2] == 4 && set[3] == 5 && set[4] == 6;
            /// Return if either matches.
            return a || b;
        }

        /// Checks whether a small straight exists (any 4-length run).
        private static bool IsSmallStraight(int[] dice) /// Validates 4-length run.
        {
            /// Build set of unique dice.
            HashSet<int> set = new HashSet<int>(dice.Distinct());
            /// Check 1 2 3 4.
            bool a = set.Contains(1) && set.Contains(2) && set.Contains(3) && set.Contains(4);
            /// Check 2 3 4 5.
            bool b = set.Contains(2) && set.Contains(3) && set.Contains(4) && set.Contains(5);
            /// Check 3 4 5 6.
            bool c = set.Contains(3) && set.Contains(4) && set.Contains(5) && set.Contains(6);
            /// True if any run exists.
            return a || b || c;
        }

        /// Returns a face 1-6 that occurs at least n times  otherwise -1.
        private static int FaceWithAtLeast(int[] counts, int n)
        {
            /// Prefer higher faces first 6 down to 1 .
            for (int f = 6; f >= 1; f--)
                /// Return first face meeting the threshold.
                if (counts[f] >= n) return f;
            /// If none match, return -1.
            return -1;
        }

        /// Returns faces that occur exactly n times.
        private static List<int> FacesWithExact(int[] counts, int n) 
        {
            /// Create list for results.
            var list = new List<int>();
            /// Scan all faces.
            for (int f = 1; f <= 6; f++)
                /// Add face if it matches exact count.
                if (counts[f] == n) list.Add(f);
            /// Return list.
            return list;
        }

        #endregion
    }
}
