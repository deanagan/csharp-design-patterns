using System;

namespace Laptop
{
    // TODO: Use Generic Constraints
    public interface ILaptopPartsFactory
    {
        IStorage CreateStorage();
        IProcessor CreateProcessor();
    }
}
