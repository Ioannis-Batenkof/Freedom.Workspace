using System;
using System.Device.Gpio;
using System.Threading;

class Program
{
    static void Main()
    {
        int sensor1Pin = 22; // physical pin 15
        int sensor2Pin = 26; // physical pin 37

        using var gpio = new GpioController(PinNumberingScheme.Logical);
        gpio.OpenPin(sensor1Pin, PinMode.InputPullUp);
        gpio.OpenPin(sensor2Pin, PinMode.InputPullUp);

        Console.WriteLine("Reading sensors (GPIO22 & GPIO26). Press Ctrl+C to stop.");

        while (true)
        {
            bool s1 = gpio.Read(sensor1Pin) == PinValue.High;
            bool s2 = gpio.Read(sensor2Pin) == PinValue.High;
            Console.WriteLine($"Sensor1: {s1}, Sensor2: {s2}");
            Thread.Sleep(300);
        }
    }
}
