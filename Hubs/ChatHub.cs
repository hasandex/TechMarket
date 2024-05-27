using Microsoft.AspNetCore.SignalR;

namespace TechMarket.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userId, string productName)
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, productName);
        }
    }
}
