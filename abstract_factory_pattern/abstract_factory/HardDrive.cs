namespace Laptop
{
    
    public class HardDrive : IStorage
    {
        public string HardwareType()
        {
            return "hdd";
        }
        public int ReadSpeedInMBytesPerSec()
        {
            return 50;
        }
    }
}