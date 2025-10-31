using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;

namespace Freedom.Workspace
{
    internal class Program
    {
        static void Main()
        {
            int sensor1Pin = 15;   // black wire of sensor 1
            int sensor2Pin = 37;   // black wire of sensor 2 (OK, but consider changing to 17 or 27 later)

            var driver = new LibGpiodDriver(4);
            using var controller = new GpioController(driver);

            controller.OpenPin(sensor1Pin, PinMode.Input);
            controller.OpenPin(sensor2Pin, PinMode.Input);

            Console.WriteLine("Reading sensors... Press Ctrl+C to stop.");

            while (true)
            {
                var s1 = controller.Read(sensor1Pin);
                var s2 = controller.Read(sensor2Pin);

                Console.WriteLine($"Sensor1: {s1}, Sensor2: {s2}");
                Thread.Sleep(300);
            }
        }
    }
}
