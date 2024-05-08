using System;

namespace FactoryMethod
{
    public class EarningBonusCalculator
    {
        private readonly PerksFactory
        perksFactory;

        public EarningBonusCalculator(PerksFactory perksFactory)
        {
            this.perksFactory = perksFactory;
        }
        public int UpdatedMiles(int currentTotalMiles, int newMilesEarned)
        {
            var perks = perksFactory.GetPerks(currentTotalMiles);
            return currentTotalMiles + newMilesEarned + Convert.ToInt32(Math.Floor(newMilesEarned * perks?.EarningBonus() ?? 0));
        }
    }
}
