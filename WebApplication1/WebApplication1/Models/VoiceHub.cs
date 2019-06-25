using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BLL;

namespace WebApplication1.Models
{
    
    public class VoiceHub : Hub
    {


        public  static int ID = 0;
        public  async Task SendMessage(int id)
        {
                await Clients.All.SendAsync("ReceiveMessage",ID);
           
        }
        public static void xnum(int n)
        {
            ID = n;
        }
       



    }
}
