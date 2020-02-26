namespace BuilderPattern
{
    public class SkuCodeStartDiscountStrategyBuilder : DiscountStrategyBuilder
    {
        public SkuCodeStartDiscountStrategyBuilder() 
            : base(new SkuCodeStartDiscountStrategy())
        {
        }
    }   
}