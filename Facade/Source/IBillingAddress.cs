namespace facade
{
    public interface IBillingAddress
    {
        string FirstName {get;set;}
        string LastName {get; set;}
        string Address {get;set;}
        string City {get;set;}
        string ZipCode{get;set;}
    }
}