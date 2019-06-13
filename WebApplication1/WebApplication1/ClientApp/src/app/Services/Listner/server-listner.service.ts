import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr'
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})

export class ServerListnerService {
  private connection : HubConnection;

  constructor() {
    this.connection=new HubConnectionBuilder().withUrl('https://localhost:44303/chat')
.configureLogging(signalR.LogLevel.Information)
.build();
  this.connection
  .start()
  .then(()=>console.log("connection started"))
  .catch(err=>console.log("Errore di connessione"));

   }

  public GetId()
   {
     this.connection.on("sendId", (n:number)=>
     {
        return n;
     });
     return null;
   }
}
