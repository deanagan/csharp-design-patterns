namespace AbstractFactory
{
    public class LenovoPartsFactory : ILaptopPartsFactory
    {
        public IStorage CreateStorage()
        {
            return new HardDrive();
        }
        public IProcessor CreateProcessor()
        {
            return new AmdProcessor();
        }
    }
}
