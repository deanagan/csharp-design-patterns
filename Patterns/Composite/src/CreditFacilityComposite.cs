using System.Collections.Generic;
using System.Linq;

namespace Composite
{
    public class CreditFacilityComposite : ICreditFacilityComponent
    {
        private readonly List<ICreditFacilityComponent> _components = new();

        public string Name { get; }

        public CreditFacilityComposite(string name)
        {
            Name = name;
        }

        public void Add(ICreditFacilityComponent component)
        {
            _components.Add(component);
        }

        public decimal GetTotalPrincipalBalance() =>
            _components.Sum(component => component.GetTotalPrincipalBalance());

        public decimal GetTotalAccruedInterest() =>
            _components.Sum(component => component.GetTotalAccruedInterest());

        public decimal GetTotalAvailableCreditLimit() =>
            _components.Sum(component => component.GetTotalAvailableCreditLimit());
    }
}