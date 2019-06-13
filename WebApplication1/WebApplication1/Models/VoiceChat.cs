using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BLL;


namespace WebApplication1.Models
{
    public class VoiceChat :Hub
    {
        public void SendToAll(string name, string message)

        {

            Clients.All.SendAsync("sendToAll", name, message);

        }

        public void SendId(string id)
        {
            List<MediaBL> mx = new List<MediaBL>();
            mx = GestoreBLL.GetMedia();
            
            try
            {
                MediaBL m = (MediaBL)mx.Where(p => p.id.Equals(int.Parse(id)));
                Clients.All.SendAsync("sendID", m.id);
            }
            catch (NotFoundException)
            {
                Clients.All.SendAsync("sendID", "ID NON VALIDO");
            }
        }
    }
}
