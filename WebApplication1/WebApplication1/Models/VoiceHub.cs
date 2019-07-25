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
           //await Clients.All.SendAsync("showVideoGallery",m);   
           await Clients.All.SendAsync("showVideoGallery", m);
                 
        }
        //public async Task sendVideo(Media m)
        //{
        //    await Clients.All.SendAsync("showVideo", m);
        //}
        public async Task sendVideo(int m)
        {
            await Clients.All.SendAsync("showVideo", m);
        }
        public async Task returnToSlide()
        {
            await Clients.All.SendAsync("goToSlide");
        }
        public async Task SendTiresByType(List<Tire> tires)
        {
            await Clients.All.SendAsync("tireShow", tires);
        }
        public async Task sendCarTires(Car auto)
        {
            await Clients.All.SendAsync("tireShow", auto.tires);
            //await Clients.All.SendAsync("showCarTires", auto.tires);
        }
        public async Task sendTireAndVideos(Tire ruota,List<Media> m)
        {
            await Clients.All.SendAsync("tireSpecs", ruota,m);
        }
        public async Task sendPage(string x)
        {
            await Clients.All.SendAsync("receivePage", x);
        }

        public async Task AskIdTire(int number)
        {
            await Clients.All.SendAsync("receiveAskIdTire",number);
        }

        public async Task SendIdTire(int idTire)
        {
            await Clients.All.SendAsync("receiveTireVideos", GestoreBLL.GetVideosByCar(idTire));
        }

    }
}
