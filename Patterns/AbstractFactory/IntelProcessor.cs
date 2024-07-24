namespace AbstractFactory
{
    public class IntelProcessor : IProcessor
    {
        public string BrandName()
        {
            return "Intel";
        }
        public double SpeedInGigaHertz()
        {
            return 1.8;
        }
    }
}