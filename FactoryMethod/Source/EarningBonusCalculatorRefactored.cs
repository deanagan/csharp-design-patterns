using System;

namespace FactoryMethod
{
    public class EarningBonusCalculator
    {
        private PerksProducer perksProducer;

        public EarningBonusCalculator(PerksProducer perksProducer)
        {
            this.perksProducer = perksProducer;
        }
        public int UpdatedMiles(int currentTotalMiles, int newMilesEarned)
        {
            var perks = perksProducer.GetPerks(currentTotalMiles);
            return currentTotalMiles + newMilesEarned + Convert.ToInt32(Math.Floor(newMilesEarned * perks.EarningBonus()));
        }
    }
}
