using System.Collections.Generic;

namespace Flyweight;

public class LoanProductFlyweightFactory
{
    private readonly Dictionary<string, ILoanProductFlyweight> _cache = [];

    public ILoanProductFlyweight GetFlyweight(string productCode)
    {
        if (!_cache.TryGetValue(productCode, out var flyweight))
        {
            flyweight = productCode switch
            {
                "SME-WORKING-CAPITAL" => new LoanProductTemplateFlyweight(
                    productCode,
                    "Truth in Lending Act disclosures apply. Variable daily remittance terms.",
                    0.085M),
                "EQUIPMENT-LEASE" => new LoanProductTemplateFlyweight(
                    productCode,
                    "Secured equipment lease terms. UCC filing required.",
                    0.060M),
                _ => throw new ArgumentException($"Unknown product code: {productCode}")
            };

            _cache[productCode] = flyweight;
        }

        return flyweight;
    }

    public int GetCachedInstanceCount() => _cache.Count;
}

