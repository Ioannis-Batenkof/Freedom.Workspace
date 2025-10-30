using System;
using System.Device.Gpio;
using System.Threading;

class Program
{
    static void Main()
    {
        int s1Pin = 15; // BCM22 physical pin 15
        int s2Pin = 37; // BCM26 physical pin 37

        using var gpio = new GpioController(PinNumberingScheme.Logical);

        gpio.OpenPin(s1Pin, PinMode.InputPullUp);
        gpio.OpenPin(s2Pin, PinMode.InputPullUp);

        Console.WriteLine("Sensor auto-detect test:");
        Console.WriteLine(" -> Keep both beams CLEAR for 3 seconds (no object).");
        for (int i = 3; i > 0; i--) { Console.Write(i + "... "); Thread.Sleep(1000); }
        Console.WriteLine();

        bool s1Clear = gpio.Read(s1Pin) == PinValue.High;
        bool s2Clear = gpio.Read(s2Pin) == PinValue.High;
        Console.WriteLine($"Reading while CLEAR: S1={s1Clear}  S2={s2Clear}");

        Console.WriteLine("Now BLOCK both beams for 3 seconds (place object in front).");
        for (int i = 3; i > 0; i--) { Console.Write(i + "... "); Thread.Sleep(1000); }
        bool s1Blocked = gpio.Read(s1Pin) == PinValue.High;
        bool s2Blocked = gpio.Read(s2Pin) == PinValue.High;
        Console.WriteLine($"Reading while BLOCKED: S1={s1Blocked}  S2={s2Blocked}");

        // Determine how the sensor maps to activity:
        // Most likely: idle (clear) => HIGH, blocked (active for dark-on) => LOW.
        // We'll decide per-sensor whether active means LOW or HIGH.
        bool s1ActiveIsLow = DetermineActiveIsLow(s1Clear, s1Blocked);
        bool s2ActiveIsLow = DetermineActiveIsLow(s2Clear, s2Blocked);

        Console.WriteLine($"Interpreted: Sensor1 activeIsLow={s1ActiveIsLow}, Sensor2 activeIsLow={s2ActiveIsLow}");
        Console.WriteLine("Starting live monitoring (press Ctrl+C to stop).");

        while (true)
        {
            bool rawS1 = gpio.Read(s1Pin) == PinValue.High;
            bool rawS2 = gpio.Read(s2Pin) == PinValue.High;

            // Convert raw reading to `isActive` (true = sensor output is ON)
            bool s1Active = s1ActiveIsLow ? !rawS1 : rawS1;
            bool s2Active = s2ActiveIsLow ? !rawS2 : rawS2;

            // Map to meaningful boolean: objectDetected (true when an object is in the beam)
            // For Dark-On: object present -> active -> sActive true -> objectDetected true
            // For Light-On: object present -> active -> sActive true -> objectDetected false
            // We can't know which mode is configured per unit, so we present sActive and raw.
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} rawS1={rawS1} rawS2={rawS2} activeS1={s1Active} activeS2={s2Active}");

            Thread.Sleep(250);
        }
    }

    static bool DetermineActiveIsLow(bool clearReading, bool blockedReading)
    {
        // If clear is HIGH and blocked is LOW => active is LOW (typical)
        if (clearReading && !blockedReading) return true;
        // If clear is LOW and blocked is HIGH => active is HIGH
        if (!clearReading && blockedReading) return false;
        // If no change or ambiguous, assume active is LOW (most common for NPN + pull-up)
        return true;
    }
}
