using System;

namespace FrequentFlyers
{
    public class EarningBonusCalculator
    {
        public int UpdatedMiles(int currentTotalMiles, int newMilesEarned)
        {
            var perkProducer = new PerksProducer();
            var perks = perkProducer.GetPerks(currentTotalMiles);

            return currentTotalMiles + newMilesEarned + Convert.ToInt32(Math.Floor(newMilesEarned * perks.EarningBonus()));
        }
    }
}