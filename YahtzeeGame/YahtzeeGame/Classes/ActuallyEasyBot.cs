using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YahtzeeGame
{
    /// <summary>
    /// Main files for ActuallyEasyBot.
    /// </summary>
    internal class ActuallyEasyBot : CPUPlayer
    {
        #region Methods

        /// <summary>
        /// Decides which dice to keep.
        /// </summary>
        /// <param name="diceValues"></param>
        /// <param name="rollsLeft"></param>
        /// <returns></returns>
        public override bool[] ChooseDice(int[] diceValues, int rollsLeft)
        {
            /// Count face value of dice.
            int[] counts = CountFaces(diceValues);

            /// Find a pair or better.
            int face2 = FaceWithAtLeast(counts, 2);

            /// If found, keep only that face.
            if (face2 != -1) return KeepFace(diceValues, face2);

            /// If no pair exists, keep only the highest die if it is 5 or 6.
            int max = diceValues.Max();
            if (max >= 5) return KeepFace(diceValues, max);

            /// Otherwise reroll everything.
            return KeepNone();
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

            /// Check upper section first in a simple order.
            string[] uppers =
            {
                Categories.Sixes,
                Categories.Fives,
                Categories.Fours,
                Categories.Threes,
                Categories.Twos,
                Categories.Aces
            };

            /// Try upper section categories first if they score anything.
            foreach (string upper in uppers)
            {
                /// Skip if category already used.
                if (!available.Contains(upper)) continue;

                /// If the score is positive, take it.
                if (PreviewScore(upper, dice) > 0) return upper;
            }

            /// Use Chance early as a simple fallback.
            if (available.Contains(Categories.Chance))
            {
                return Categories.Chance;
            }

            /// Try lower section categories only if they score.
            string[] lowers =
            {
                Categories.ThreeKind,
                Categories.FourKind,
                Categories.FullHouse,
                Categories.SmallStraight,
                Categories.LargeStraight,
                Categories.Yahtzee
            };

            /// Check lower section categories.
            foreach (string lower in lowers)
            {
                /// Skip if category already used.
                if (!available.Contains(lower)) continue;

                /// If the score is positive, take it.
                if (PreviewScore(lower, dice) > 0) return lower;
            }

            /// Define a dump order for when everything scores 0.
            string[] dumps =
            {
                Categories.Aces,
                Categories.Twos,
                Categories.Threes,
                Categories.ThreeKind,
                Categories.FourKind,
                Categories.FullHouse,
                Categories.SmallStraight,
                Categories.LargeStraight,
                Categories.Yahtzee,
                Categories.Fours,
                Categories.Fives,
                Categories.Sixes,
                Categories.Chance
            };

            /// Choose the first available dump category.
            foreach (string dump in dumps)
            {
                /// If it is still available, return it.
                if (available.Contains(dump)) return dump;
            }

            /// If nothing else works, return the first available category.
            return available[0];
        }
        #endregion
    }
}