using Microsoft.AspNetCore.SignalR;
using PharmaQueue.Models;
using System.Threading.Tasks;

namespace PharmaQueue.Hubs
{
    public class StatusHub : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendPrescription (string userId)
        {
            await Clients.All.SendAsync("PrescriptionUpdate", userId);
        }
    }
}