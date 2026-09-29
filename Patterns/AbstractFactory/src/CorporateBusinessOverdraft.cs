namespace AbstractFactory;

public class CorporateBusinessOverdraft : IBusinessOverdraft
{
    public decimal GetOverdraftLimit()
    {
        return 100000m;
    }

    public string GetDescription()
    {
        return "Corporate Business overdraft facility";
    }
}