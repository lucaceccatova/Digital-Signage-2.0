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

        public async Task returnToSlide(bool b)
        {
            await Clients.All.SendAsync("goToSlide",b);
        }
        public async Task sendCarTires(Car auto)
        {
            await Clients.All.SendAsync("showCarTires", auto);
        }
        public async Task sendTire(Tire ruota)
        {
            await Clients.All.SendAsync("receiveTire", ruota);
        }
    }
}
