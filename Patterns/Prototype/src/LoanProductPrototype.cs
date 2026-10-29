using System;

namespace Prototype;

public sealed class LoanProductPrototype : ICloneable
{
    public LoanProductPrototype(
        string productCode,
        string productName,
        LoanProductTerms terms)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentNullException.ThrowIfNull(terms);

        ProductCode = productCode;
        ProductName = productName;
        Terms = terms;
    }

    public string ProductCode { get; }

    public string ProductName { get; }

    public LoanProductTerms Terms { get; private set; }

    public object Clone() => MemberwiseClone();

    public LoanProductPrototype DeepClone()
    {
        var clone = (LoanProductPrototype)MemberwiseClone();
        clone.Terms = Terms.Copy();
        return clone;
    }
}
