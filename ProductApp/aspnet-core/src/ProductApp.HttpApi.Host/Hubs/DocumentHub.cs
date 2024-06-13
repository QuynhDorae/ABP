using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ProductApp.Hubs
{
    public class DocumentHub : Hub
    {
        public async Task SendBookmark(string mesage)
        {
            await Clients.All.SendAsync("ReceiveMessage", mesage);
        }
    }
}
