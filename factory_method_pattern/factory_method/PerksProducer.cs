using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequentFlyers
{
    public class PerksProducer
    {
        private List<(int, string)> thresholds;

        public PerksProducer(List<(int, string)> thresholds)
        {
            this.thresholds = thresholds;
        }
        public IPerks GetPerks(int totalMiles)
        {
            var perks = thresholds.Where(th => totalMiles >= th.Item1)
                                  .Select(p=>p.Item2)
                                  .DefaultIfEmpty("BasicPerks")
                                  .First();
                                   
            var fullyQualifiedPerksName = $"FrequentFlyers.{perks}";

            var perksType = Type.GetType(fullyQualifiedPerksName);
            return (IPerks) Activator.CreateInstance(perksType);
        }
    }
}
