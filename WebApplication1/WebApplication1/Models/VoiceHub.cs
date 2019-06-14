using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BLL;


namespace WebApplication1.Models
{
    public class VoiceHub :Hub
    {
        //public void SendToAll(string name, string message)

        //{

        //    Clients.All.SendAsync("sendToAll", name, message);

        //}

        public async Task SendToAll(string id)
        {
            //List<MediaBL> mx = new List<MediaBL>();
            //mx = GestoreBLL.GetMedia();

            //try
            //{
            //    MediaBL m = (MediaBL)mx.Where(p => p.id.Equals(int.Parse(id)));
            //    Clients.All.SendAsync("sendID", m.id);
            //}
            //catch (Exception)
            //{
            //    Clients.All.SendAsync("sendID", "ID NON VALIDO");
            //}
           await Clients.All.SendAsync("sendID", 2);
        }
    }
}
