using Microsoft.AspNetCore.SignalR;

namespace ECAP_Server;
internal class MirrorHub : Hub
{
    public async Task SendMessageAsync(string username, string message)
    {
        if (Clients == null)
        {
            Console.WriteLine("There's no one to listen you, stop embarrasing yourself");
            return;
        }

        await Clients.All.SendAsync("ListenKeyboard", username, message);
    }
}
