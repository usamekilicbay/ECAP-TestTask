using Microsoft.AspNetCore.SignalR;

namespace ECAP_Server;
internal class MirrorHub : Hub
{
    // The actualy method behind the all magic, called by the client and sends message to all client
    public async Task SendMessageAsync(string username, string message)
    {
        try
        {
            Console.Write(message);

            // For that example we can use Client.Caller as well, but maybe you want to use multiple clients. That's why I used Clients.All
            await Clients.All.SendAsync("ListenKeyboard", username, message);
        }
        catch (Exception)
        {
            Console.WriteLine("There's no one to listen you, stop embarrasing yourself");
        }
    }
}
