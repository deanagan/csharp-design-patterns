namespace Laptop
{
    
    public class SolidStateDrive : IStorage
    {
        public string HardwareType()
        {
            return "ssd";
        }
        public int ReadSpeedInMBytesPerSec()
        {
            return 250;
        }
    }
}