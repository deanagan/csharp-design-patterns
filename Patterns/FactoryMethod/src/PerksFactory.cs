using System;
using System.Collections.Generic;
using System.Linq;

namespace FactoryMethod
{
    public class PerksFactory
    {
        private readonly List<(int, string)> thresholds;
        private const string DEFAULT_PERKS_TYPE = "BasicPerks";

        public PerksFactory(List<(int, string)> thresholds)
        {
            this.thresholds = thresholds;
        }
        public IPerks? GetPerks(int totalMiles)
        {
            var perks = thresholds.Where(th => totalMiles >= th.Item1)
                                  .Select(p => p.Item2)
                                  .DefaultIfEmpty("BasicPerks")
                                  .First();

            var fullyQualifiedPerksName = $"FactoryMethod.{perks}";

            var perksType = Type.GetType(fullyQualifiedPerksName);

            if (perksType == null)
            {
                return null;
            }

            return (IPerks?)Activator.CreateInstance(perksType);
        }
    }
}
