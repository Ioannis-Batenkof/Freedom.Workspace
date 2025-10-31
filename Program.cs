using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;

namespace Freedom.Workspace
{
    internal class Program
    {
        static void Main()
        {
            int sensor1Pin = 17; // channel 1 output from 817 module
            int sensor2Pin = 1; // channel 2 output from 817 module

            using var controller = new GpioController(PinNumberingScheme.Logical, new LibGpiodDriver(4));

            // Enable pull-ups
            controller.OpenPin(sensor1Pin, PinMode.InputPullUp);
            controller.OpenPin(sensor2Pin, PinMode.InputPullUp);

            Console.WriteLine("Reading sensors... Press Ctrl+C to stop.");

            while (true)
            {
                bool sensor1Active = controller.Read(sensor1Pin) == PinValue.Low; // LOW = active
                bool sensor2Active = controller.Read(sensor2Pin) == PinValue.Low;

                Console.WriteLine($"Sensor1: {sensor1Active}, Sensor2: {sensor2Active}");
                Thread.Sleep(300);
            }
        }
    }
}
