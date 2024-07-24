namespace AbstractFactory
{
    public class AmdProcessor : IProcessor
    {
        public string BrandName()
        {
            return "AMD";
        }
        public double SpeedInGigaHertz()
        {
            return 2.1;
        }
    }
}