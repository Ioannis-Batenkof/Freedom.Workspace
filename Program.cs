using System;
using System.Device.Gpio;
using System.Threading;

class Program
{
    static void Main()
    {
        int sensor1Pin = 22; // physical pin 15
        int sensor2Pin = 26; // physical pin 37

        using var gpio = new GpioController();
        gpio.OpenPin(sensor1Pin, PinMode.InputPullUp);
        gpio.OpenPin(sensor2Pin, PinMode.InputPullUp);

        Console.WriteLine("Reading sensors (GPIO22 & GPIO26). Press Ctrl+C to stop.");

        gpio.RegisterCallbackForPinValueChangedEvent(
            sensor1Pin, PinEventTypes.Falling | PinEventTypes.Rising, OnPin1Event);

        gpio.RegisterCallbackForPinValueChangedEvent(
            sensor2Pin, PinEventTypes.Falling | PinEventTypes.Rising, OnPin2Event);

        static void OnPin1Event(object sender, PinValueChangedEventArgs args)
        {
            Console.WriteLine(
                $"({DateTime.Now}) {(args.ChangeType is PinEventTypes.Rising ? "Low on 1" : "High on 1")}");
        }

        static void OnPin2Event(object sender, PinValueChangedEventArgs args)
        {
            Console.WriteLine(
                $"({DateTime.Now}) {(args.ChangeType is PinEventTypes.Rising ? "Low on 2" : "High on 2")}");
        }

        Task.Delay(Timeout.Infinite);

        while (true)
        {
            bool s1 = gpio.Read(sensor1Pin) == PinValue.High;
            bool s2 = gpio.Read(sensor2Pin) == PinValue.High;
            Console.WriteLine($"Sensor1: {s1}, Sensor2: {s2}");
            Thread.Sleep(300);
        }
    }
}
