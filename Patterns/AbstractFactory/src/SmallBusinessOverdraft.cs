namespace AbstractFactory;

public class SmallBusinessOverdraft : IBusinessOverdraft
{
    public decimal GetOverdraftLimit()
    {
        return 10000m;
    }

    public string GetDescription()
    {
        return "Small Business overdraft facility";
    }
}

    
