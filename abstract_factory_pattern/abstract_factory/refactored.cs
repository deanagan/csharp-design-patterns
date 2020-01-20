using System;


// Product 1
public class StorageDevice
{
    public string Type {get;set;}
    public int ReadSpeed{get;set;}

}

// Product 2
public class Processor
{
    public string Brand {get;set;}
    public double SpeedInGhz{get;set;}
}

public class MainProgram
{
    private static Processor GetProcessor(string source)
    {
        if (source == "Lenovo")
        {
            return new Processor {
                Brand = "AMD",
                SpeedInGhz = 2.1
            };
        }
        else if (source == "Dell")
        {
            return new Processor {
                Brand = "Intel",
                SpeedInGhz = 1.8
            };
        }
        return null;
    }

    private static StorageDevice GetStorageDevice(string source)
    {
        if (source == "Lenovo")
        {
            return new StorageDevice {
                Type = "ssd",
                ReadSpeed = 250
            };

        }
        else if (source == "Dell")
        {
            return new StorageDevice {
                Type = "hdd",
                ReadSpeed = 50
            };
        }
        return null;
    }
    public static int Main()
    {
        var source = "Lenovo";
        var proc = GetProcessor(source);
        var storage = GetStorageDevice(source);
        Console.WriteLine($"{source} Specifications");
        Console.WriteLine($"Processor Brand: {proc.Brand}");
        Console.WriteLine($"Processor Speed: {proc.SpeedInGhz}GHz");
        Console.WriteLine($"Storage Type: {storage.Type}");
        Console.WriteLine($"Storage ReadSpeed: {storage.ReadSpeed} Mbps");
        return 0;
    }
}

// For scriptcs to work
MainProgram.Main();