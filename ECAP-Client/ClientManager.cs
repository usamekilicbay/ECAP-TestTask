using Microsoft.AspNetCore.SignalR.Client;

namespace ECAP_Client;
internal class ClientManager
{
    private readonly HubConnection _hubConnection = null!;

    public ClientManager()
    {
        // Start the connection by using the server Url, I could use Env variables to do it more flexible.
        var hubConnection = new HubConnectionBuilder().WithUrl("http://host.docker.internal:8080/listenkeyboard").Build();

        // Start the connection
        hubConnection.StartAsync().Wait();

        // Simply it's a event subscription to listen server
        hubConnection.On("ListenKeyboard", ListenKeyboard());
    }

    private Action<string, string> ListenKeyboard()
    {
        return (string username, string message) =>
        {
            //Console.WriteLine($"Did you mean \"{message}\", Sir {username}?");
            //Console.Write($"{username}:{message}");
            Console.Write(message);
        };
    }

    internal async Task ContinuousTyping()
    {
        Console.WriteLine("Please write the message you want to echo:");

        // Create infinite loop to not kill the application
        do
        {
            // These conditions for running app in Docker container, otherwise if might throw "InvalidOperationException application does not have a console" error
            if (!Console.IsInputRedirected && Console.KeyAvailable)
            {
                // Read button instead of the char to check is it Enter 
                // !!! Here we could use many different approaches but it works well for now
                // true parameter is for not writing the actual input, all the chars user sees on the console is written by server
                var pressedKey = Console.ReadKey(true);

                // Kill the application when use presses Enter
                if (pressedKey.Key == ConsoleKey.Enter)
                    Environment.Exit(0);

                // Get the input char
                var message = pressedKey.KeyChar.ToString();

                await SendMessageToServer(message);
            }
        }
        while (true);
    }

    private async Task SendMessageToServer(string message)
    {
        // And lastly, call the SendMessageAsync method from Server (Username is not necessary)
        await _hubConnection.InvokeCoreAsync("SendMessageAsync", args: new[] { "UnemployedOne", message });
    }
}
