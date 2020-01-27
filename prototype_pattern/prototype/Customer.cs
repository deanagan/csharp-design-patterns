using System;

namespace Billing
{
    public class Customer : BasicCustomer
    {
        public override object Clone()
        {
            return this.MemberwiseClone() as BasicCustomer;
        }
        public override Customer DeepClone()
        {
            var customer = this.MemberwiseClone() as Customer;

            customer.BillingAddress = new Address
            {
                StreetAddress = this.BillingAddress.StreetAddress,    
                City = this.BillingAddress.City,
                State = this.BillingAddress.State,
                Country = this.BillingAddress.Country,
                PostCode = this.BillingAddress.PostCode
            };

            customer.HomeAddress = new Address
            {
                StreetAddress = this.HomeAddress.StreetAddress,    
                City = this.HomeAddress.City,
                State = this.HomeAddress.State,
                Country = this.HomeAddress.Country,
                PostCode = this.HomeAddress.PostCode
            };

            return customer;
        }
    }
}