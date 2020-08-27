namespace Laptop
{
    public class DellPartsFactory : ILaptopPartsFactory
    {
        public IStorage CreateStorage()
        {
            return new SolidStateDrive();
        }
        public IProcessor CreateProcessor()
        {
            return new IntelProcessor();
        }
    }

}