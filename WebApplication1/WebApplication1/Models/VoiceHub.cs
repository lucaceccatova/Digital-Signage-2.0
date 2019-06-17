using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class VoiceHub : Hub
    {
        public  async Task SendMessage(int id)
        {
             await Clients.All.SendAsync("ReceiveMessage", id);
        }


    }
}
