using System;
using System.Device.Gpio;
using System.Threading;

class Program
{
    static void Main()
    {
        int sensor1Pin = 24; // BCM numbering
        int sensor2Pin = 28;

        using var gpio = new GpioController(PinNumberingScheme.Logical);
        gpio.OpenPin(sensor1Pin, PinMode.Input);
        gpio.OpenPin(sensor2Pin, PinMode.Input);

        Console.WriteLine("Watching sensors... Press Ctrl+C to stop.");

        while (true)
        {
            bool s1 = gpio.Read(sensor1Pin) == PinValue.High;
            bool s2 = gpio.Read(sensor2Pin) == PinValue.High;
            Console.WriteLine($"S1: {s1}   S2: {s2}");
            Thread.Sleep(300);
        }
    }
}
