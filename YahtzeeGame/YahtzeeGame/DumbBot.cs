using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YahtzeeGame
{
    public class DumbBot : Player
    {
        public DumbBot (int Pos, string Name, bool Comp) : base(Pos, Name, Comp)
        {

        }

        public void DecisionTree(int[] dice)
        {
            if (this.PlayerScores.YahtzeeValidation(dice) && !this.PlayerScores.yahtzeeScored)
            {
                this.PlayerScores.YahtzeeSelected(dice, true);
            }
            else if (this.PlayerScores.LargeStraightValidation(dice) && !this.PlayerScores.largeStraightScored)
            {
                this.PlayerScores.LargeStraightSelected(dice, true);
            }
            else if (this.PlayerScores.SmallStraightValidation(dice) && !this.PlayerScores.smallStraightScored)
            {
                this.PlayerScores.SmallStraightSelected(dice, true);
            }
            else if (this.PlayerScores.FourKindValidation(dice) && !this.PlayerScores.fourOfAKindScored)
            {
                this.PlayerScores.FourOfAKindSelected(dice, true);
            }
            else if (this.PlayerScores.ThreeKindValidation(dice) && !this.PlayerScores.threeOfAKindScored)
            {
                this.PlayerScores.ThreeOfAKindSelected(dice, true);
            }
            else if (this.PlayerScores.FullHouseValidation(dice) && !this.PlayerScores.fullHouseScored)
            {
                this.PlayerScores.FullHouseSelected(dice, true);
            }
            else if (!this.PlayerScores.sixesScored && this.PlayerScores.sixes > 0)
            {
                this.PlayerScores.SixesSelected(dice, true);
            }
            else if (!this.PlayerScores.fivesScored && this.PlayerScores.fives > 0)
            {
                this.PlayerScores.FivesSelected(dice, true);
            }
            else if (!this.PlayerScores.foursScored && this.PlayerScores.fours > 0)
            {
                this.PlayerScores.FoursSelected(dice, true);
            }
            else if (!this.PlayerScores.threesScored && this.PlayerScores.threes > 0)
            {
                this.PlayerScores.ThreesSelected(dice, true);
            }
            else if (!this.PlayerScores.twosScored && this.PlayerScores.twos > 0)
            {
                this.PlayerScores.TwosSelected(dice, true);
            }
            else if (!this.PlayerScores.acesScored && this.PlayerScores.aces > 0)
            {
                this.PlayerScores.AcesSelected(dice, true);
            }
            else if (!this.PlayerScores.chanceScored && this.PlayerScores.chance > 0)
            {
                this.PlayerScores.ChanceSelected(dice, true);
            }
            else if (!this.PlayerScores.acesScored)
            {
                this.PlayerScores.AcesSelected(dice, true);
            }
            else if (!this.PlayerScores.twosScored)
            {
                this.PlayerScores.TwosSelected(dice, true);
            }
            else if (!this.PlayerScores.threesScored)
            {
                this.PlayerScores.ThreesSelected(dice, true);
            }
            else if (!this.PlayerScores.foursScored)
            {
                this.PlayerScores.FoursSelected(dice, true);
            }
            else if (!this.PlayerScores.fivesScored)
            {
                this.PlayerScores.FivesSelected(dice, true);
            }
            else if (!this.PlayerScores.sixesScored)
            {
                this.PlayerScores.SixesSelected(dice, true);
            }
            else if (!this.PlayerScores.threeOfAKindScored)
            {
                this.PlayerScores.ThreeOfAKindSelected(dice, true);
            }
            else if (!this.PlayerScores.fourOfAKindScored)
            {
                this.PlayerScores.FourOfAKindSelected(dice, true);
            }
            else if (!this.PlayerScores.fullHouseScored)
            {
                this.PlayerScores.FullHouseSelected(dice, true);
            }
            else if (!this.PlayerScores.smallStraightScored)
            {
                this.PlayerScores.SmallStraightSelected(dice, true);
            }
            else if (!this.PlayerScores.largeStraightScored)
            {
                this.PlayerScores.LargeStraightSelected(dice, true);
            }
            else if (!this.PlayerScores.yahtzeeScored)
            {
                this.PlayerScores.YahtzeeSelected(dice, true);
            }
            else
            {
                MessageBox.Show("This computer is done making decisions");
            }
            
        }
    }
}
