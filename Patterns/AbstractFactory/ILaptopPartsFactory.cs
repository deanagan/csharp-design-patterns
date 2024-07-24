
namespace AbstractFactory
{
    // TODO: Use Generic Constraints
    public interface ILaptopPartsFactory
    {
        IStorage CreateStorage();
        IProcessor CreateProcessor();
    }
}
