using System;
using System.Device.Gpio;
using System.Threading;

class Program
{
    static async Task Main()
    {
        int sensor1Pin = 22; // physical pin 15
        int sensor2Pin = 26; // physical pin 37

        const int Pin = 22;
        const string Alert = "ALERT 🚨";
        const string Ready = "READY ✅";

        using var controller = new GpioController();
        controller.OpenPin(Pin, PinMode.InputPullUp);

        Console.WriteLine(
            $"Initial status ({DateTime.Now}): {(controller.Read(Pin) == PinValue.High ? Alert : Ready)}");

        controller.RegisterCallbackForPinValueChangedEvent(
            Pin,
            PinEventTypes.Falling | PinEventTypes.Rising,
            OnPinEvent);

        await Task.Delay(Timeout.Infinite);

        static void OnPinEvent(object sender, PinValueChangedEventArgs args)
        {
            Console.WriteLine(
                $"({DateTime.Now}) {(args.ChangeType is PinEventTypes.Rising ? Alert : Ready)}");
        }
    }
}
