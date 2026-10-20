using System.Collections.Frozen;

namespace Flyweight;

public class LoanProductFlyweightFactory
{
    // Summary Rule of Thumb
    // If your cache grows dynamically at runtime: Use ConcurrentDictionary.
    // If your cache is populated once at startup and never changes: Use FrozenDictionary.
    private readonly FrozenDictionary<string, ILoanProductFlyweight> _cache;

    public LoanProductFlyweightFactory()
    {
        // Pre-populate the cache with known product templates
        _cache = new Dictionary<string, ILoanProductFlyweight>
        {
            ["SME-WORKING-CAPITAL"] = new LoanProductTemplateFlyweight(
                "SME-WORKING-CAPITAL",
                "Truth in Lending Act disclosures apply. Variable daily remittance terms.",
                0.085M),
            ["EQUIPMENT-LEASE"] = new LoanProductTemplateFlyweight(
                "EQUIPMENT-LEASE",
                "Secured equipment lease terms. UCC filing required.",
                0.060M)
        }.ToFrozenDictionary();
    }

    public ILoanProductFlyweight GetFlyweight(string productCode)
    {
        if (_cache.TryGetValue(productCode, out var flyweight))
        {
            return flyweight;
        }

        throw new ArgumentException($"Unknown product code: {productCode}");
    }

    public int GetCachedInstanceCount() => _cache.Count;
}

