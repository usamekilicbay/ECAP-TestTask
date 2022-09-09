using Microsoft.AspNetCore.SignalR;

namespace ECAP_Server;
internal class MirrorHub : Hub
{
    // The actualy method behind the all magic, called by the client and sends message to all client
    // For that example we can use Client.Caller as well, but maybe you want to use multiple clients. That's why I used Clients.All
    public async Task SendMessageAsync(string username, string message)
    {
        // If Clients null don't let the SendMessage, just noticed I could use try catch here 🤦‍
        if (Clients == null)
        {
            Console.WriteLine("There's no one to listen you, stop embarrasing yourself");
            return;
        }

        Console.Write(message);
        await Clients.All.SendAsync("ListenKeyboard", username, message);
    }
}
