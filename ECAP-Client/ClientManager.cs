using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace ECAP_Client;
internal class ClientManager
{
    private readonly HubConnection _hubConnection = null!;

    public ClientManager(IConfiguration configuration)
    {
        try
        {
            var serverSettings = new ServerSettings(configuration["SERVER_IP"], configuration["SERVER_PORT"]);

            // Set the connection by using the server settings. Port part is for debugging, it has to be empty if you want to use docker compose
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://{serverSettings.Ip}:{serverSettings.Port}/listenkeyboard")
                // Reconnect if connection lost (2-2-10-30 seconds) after 4 try connection will be closed
                .WithAutomaticReconnect()
                .Build();

            // Start connection until it connects succesfully
            StartConnection();

            // Simply it's a event subscription to listen server
            _hubConnection.On("ListenKeyboard", ListenKeyboard());
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine($"HubConnection is failed {e.Message}");
            throw e;
        }
    }

    private void StartConnection()
    {
        // Break recursion if hub is connected
        if (_hubConnection.State == HubConnectionState.Connected)
            return;

        try
        {
            // Start the hub connection
            _hubConnection.StartAsync().Wait();
        }
        catch (Exception)
        {
            // If the hub connection throws error call method as recursive
            StartConnection();
        }
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
        try
        {
            // And lastly, call the SendMessageAsync method from Server (Username is not necessary)
            await _hubConnection.InvokeCoreAsync("SendMessageAsync", args: new[] { "UnemployedOne", message });
        }
        catch (Exception)
        {
            Console.WriteLine("Server is dead! I can't let you type anything until server revives");
        }
    }
}
