using System;

namespace FactoryMethod
{
    public class EarningBonusCalculatorLegacy
    {
        public int UpdatedMiles(int currentTotalMiles, int newMilesEarned)
        {
            if (currentTotalMiles >= 20000)
            {
                return currentTotalMiles + newMilesEarned + Convert.ToInt32(Math.Floor(newMilesEarned * 1.0));
            }
            else if (currentTotalMiles >= 10000)
            {
                return currentTotalMiles + newMilesEarned + Convert.ToInt32(Math.Floor(newMilesEarned * 0.5));
            }
            else
            {
                return currentTotalMiles + newMilesEarned;
            }
        }
    }
}
