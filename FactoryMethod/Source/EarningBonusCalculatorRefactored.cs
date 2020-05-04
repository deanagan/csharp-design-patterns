using System;

namespace FactoryMethod
{
    public class EarningBonusCalculator
    {
        private PerksFactory perksFactory;

        public EarningBonusCalculator(PerksFactory perksFactory)
        {
            this.perksFactory = perksFactory;
        }
        public int UpdatedMiles(int currentTotalMiles, int newMilesEarned)
        {
            var perks = perksFactory.GetPerks(currentTotalMiles);
            return currentTotalMiles + newMilesEarned + Convert.ToInt32(Math.Floor(newMilesEarned * perks.EarningBonus()));
        }
    }
}
