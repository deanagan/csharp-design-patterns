namespace AbstractFactory
{
    //TODO: Use Generic Constraints
    public interface IStorage
    {
        string HardwareType();
        int ReadSpeedInMBytesPerSec();
    }
}
