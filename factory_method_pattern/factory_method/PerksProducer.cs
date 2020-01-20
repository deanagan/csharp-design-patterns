using System;

namespace FrequentFlyers
{
    public class PerksProducer
    {
        public IPerks GetPerks(int totalMiles)
        {
            if (totalMiles >= 20000)
            {
                return new GoldPerks();
            }
            else if (totalMiles >= 10000)
            {
                return new SilverPerks();
            }
            else
            {
                return new BasicPerks();
            }
        }
    }
}
