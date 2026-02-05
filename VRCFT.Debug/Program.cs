using LucHeart.CoreOSC;
using System.Net;

namespace VRCFT.Debug;

internal class Program
{
    private static bool isRunning = true;

    static void Main(string[] args)
    {
        // Ctrl+C to stop the program
        Console.CancelKeyPress += (_, _) => isRunning = false;

        var endPoint = new IPEndPoint(IPAddress.Loopback, 9000);
        var listener = new OscListener(endPoint);

        var listenerTask = Task.Run(async () =>
        {
            while (isRunning)
            {
                var message = listener.ReceiveMessageAsync().Result;

                if (message == null)
                    continue;

                foreach (var arg in message.Arguments)
                {
                    string? argumentMessage = arg switch
                    {
                        bool => (bool)arg ? "True" : "False",
                        int => arg.ToString(),
                        float => arg.ToString(),
                        _ => null
                    };

                    Console.WriteLine($"Parameter: {message.Address} - Value: {argumentMessage ?? "Invalid type!"}");
                }
            }
        });

        while (!listenerTask.IsCompleted)
            Thread.Sleep(1000);

        listener.Dispose();
    }
}