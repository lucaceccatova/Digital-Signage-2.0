using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BLL;
using DAL;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    
    public  class VoiceHub : Hub
{
        public async Task sendAllVideo(List<Media> m)
        {
           await Clients.All.SendAsync("showVideoGallery",m);   
        }

        public async Task sendVideo(Media m)
        {
            await Clients.All.SendAsync("showVideo", m);
        }
    }
}
