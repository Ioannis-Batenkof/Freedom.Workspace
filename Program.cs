using System;
using System.Device.Gpio;
using System.Threading;



class Program
{
    static void Main()
    {
        int sensorPin = 22; // GPIO17, change if needed
        int sensorPin2 = 23;
        using var controller = new GpioController();



        // Set pin mode to input (most light barriers have a digital output)
        controller.OpenPin(sensorPin, PinMode.InputPullUp);
        controller.OpenPin(sensorPin2, PinMode.InputPullUp);


        Console.WriteLine("Lichtschranke gestartet – Ctrl+C zum Beenden.");



        while (true)
        {
            PinValue value = controller.Read(sensorPin);
            PinValue value2 = controller.Read(sensorPin2);


            if (value == PinValue.High)
            {
                Console.WriteLine("➡️  Objekt erkannt – Lichtstrahl unterbrochen!");
            }

            if (value2 == PinValue.High)
            {
                Console.WriteLine("➡️  Objekt erkannt – Lichtstrahl 2 unterbrochen!");
            }

            Thread.Sleep(500); // check twice per second
        }
    }
}

