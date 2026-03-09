using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    /// <summary>
    /// Main files for MediumBot.
    /// </summary>
    internal class MediumBot : CPUPlayer
    {
        #region Methods

        /// <summary>
        /// Decides which dice to keep
        /// </summary>
        /// <param name="diceValues"></param>
        /// <param name="rollsLeft"></param>
        /// <returns></returns>
        public override bool[] ChooseDice(int[] diceValues, int rollsLeft)
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
            int max = MaxDie(diceValues);
            /// Hold only the dice matching that highest value.
            return KeepFace(diceValues, max);
        }

        /// <summary>
        /// Chooses the best scoring category available on the scorecard.
        /// </summary>
        /// <param name="dice"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public override string ChooseCategory(int[] dice, ScoreCard card)
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
                /// Get the minimum threshold for good enough.
                int threshold = UpperThreshold(upper);

                /// If the score meets requirements keep it.
                if (s >= threshold) return upper;
            }

            /// If best available adds points, keep it.
            if (scored.Count > 0 && scored[0].Item2 > 0) return scored[0].Item1;

            /// Define a dump order for when everything scores 0.
            string[] dumps =
            {
                /// Dump high difficulty categories first.
                "yahtzee", "largeStraight", "fullHouse", "fourKind", "smallStraight", "threeKind",
                /// Then dump upper section.
                "aces", "twos", "threes", "fours", "fives", "sixes","chance"
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
        #endregion
    }
}