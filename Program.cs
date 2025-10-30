using System.Device.Gpio;
using System.Threading;

namespace Freedom.Workspace
{
    internal class Program
    {
        static void Main()
        {
            int sensor1Pin = 22;   // black wire of sensor 1
            int sensor2Pin = 26;   // black wire of sensor 2 (OK, but consider changing to 17 or 27 later)

            using var controller = new GpioController();

            controller.OpenPin(sensor1Pin, PinMode.Input);
            controller.OpenPin(sensor2Pin, PinMode.Input);

            Console.WriteLine("Reading sensors... Press Ctrl+C to stop.");

            while (true)
            {
                bool s1 = controller.Read(sensor1Pin) == PinValue.High;
                bool s2 = controller.Read(sensor2Pin) == PinValue.High;

                Console.WriteLine($"Sensor1: {s1}, Sensor2: {s2}");
                Thread.Sleep(300);
            }
        }
    }
}
