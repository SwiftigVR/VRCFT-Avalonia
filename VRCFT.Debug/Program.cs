using LucHeart.CoreOSC;
using System.Net;

namespace VRCFT.Debug;

internal class Program
{
    static void Main(string[] args)
    {
        var endPoint = new IPEndPoint(IPAddress.Loopback, 9000);
        var listener = new OscListener(endPoint);

        bool isRunning = true;
        Console.CancelKeyPress += (_, _) => isRunning = false;

        var listenerTask = Task.Run(async () =>
        {
            while (isRunning)
            {
                var message = listener.ReceiveMessageAsync().Result;

                if (message == null)
                    continue;

                string argumentMessage = null!;
                foreach (var arg in message.Arguments)
                {
                    switch (arg)
                    {
                        case bool:
                            argumentMessage = (bool)arg ? "True" : "False";
                            break;

                        case int:
                            argumentMessage = arg.ToString()!;
                            break;

                        case float:
                            argumentMessage = arg.ToString()!;
                            break;

                        case null:
                        default:
                            argumentMessage = null!;
                            break;
                    }
                }

                Console.WriteLine($"Parameter: {message.Address} - Value: {argumentMessage ?? "Invalid type!"}");
            }
        });

        while (isRunning)
            Thread.Sleep(1000);

        listener.Dispose();
    }
}