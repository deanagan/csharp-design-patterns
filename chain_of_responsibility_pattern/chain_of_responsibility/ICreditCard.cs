using System;

namespace ChainOfResponsibility
{
    // Credit card numbers are created in a consistent way. 
    // American Express cards start with either 34 or 37. 
    // Mastercard numbers begin with 51–55. 
    // Visa cards start with 4
    public interface ICreditCard
    {
        string Number { get; set; }

    }
}
