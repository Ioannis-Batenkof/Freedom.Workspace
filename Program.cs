using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;

class Program
{
    static void Main()
    {
        int pin = 22;     // test one channel first
        using var ctl = new GpioController(new LibGpiodDriver(4));
        ctl.OpenPin(pin, PinMode.InputPullUp);

        Console.WriteLine("Watching raw pin level (HIGH/LOW)...");
        while (true)
        {
            Console.WriteLine(ctl.Read(pin));
            Thread.Sleep(500);
        }
    }
}
