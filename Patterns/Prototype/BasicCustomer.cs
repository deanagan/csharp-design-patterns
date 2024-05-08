using System;

namespace Prototype
{
    public abstract class BasicCustomer : ICloneable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Address? HomeAddress { get; set; }
        public Address? BillingAddress { get; set; }

        public abstract object Clone();
        public abstract Customer DeepClone();
    }
}
