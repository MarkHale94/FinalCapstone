using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PharmaQueue.Hubs
{
    public class StatusHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        
    }
}