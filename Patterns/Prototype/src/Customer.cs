using System;

namespace Prototype
{
    public class Customer : BasicCustomer
    {
        public override object Clone()
        {
            // Below is with pattern matching. If unfamiliar, this is how it reads:
            /*
                var clonedCustomer = this.MemberwiseClone() as BasicCustomer;

                if (clonedCustomer == null)
                {
                    throw new InvalidOperationException("memberwise clone returned null");
                }

                return clonedCustomer;
            */
            if (this.MemberwiseClone() is not BasicCustomer clonedCustomer)
            {
                throw new InvalidOperationException("memberwise clone returned null");
            }

            return clonedCustomer;
        }

        public override Customer DeepClone()
        {
            var customer = this.MemberwiseClone() as Customer ?? throw new InvalidOperationException("memberwise clone returned null");

            customer.BillingAddress = this.BillingAddress != null ? new Address
            {
                StreetAddress = this.BillingAddress.StreetAddress,
                City = this.BillingAddress.City,
                State = this.BillingAddress.State,
                Country = this.BillingAddress.Country,
                PostCode = this.BillingAddress.PostCode
            } : null;

            customer.HomeAddress = this.HomeAddress != null ? new Address
            {
                StreetAddress = this.HomeAddress.StreetAddress,
                City = this.HomeAddress.City,
                State = this.HomeAddress.State,
                Country = this.HomeAddress.Country,
                PostCode = this.HomeAddress.PostCode
            } : null;

            return customer;
        }
    }
}
