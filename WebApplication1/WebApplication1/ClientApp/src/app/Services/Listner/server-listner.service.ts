import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr'
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import { Observable, observable,Subject } from 'rxjs';
import { connect } from 'net';

@Injectable({
  providedIn: 'root'
})

export class ServerListnerService {
  private connection : HubConnection;
  public directves:number[]=[];
  id:number;
  constructor() {
    this.connection=new HubConnectionBuilder().withUrl('https://localhost:44303/voice')
.configureLogging(signalR.LogLevel.Information)
.build();
  this.connection
  .start()
  .then(()=>console.log("connection started"))
  .catch(err=>console.log("Errore di connessione"));



 
   }

  



  public getDirective() {
    this.connection
      .invoke('SendMessage', 2)
      .catch(err => console.error(err));
    

      this.connection.on("ReceiveMessage", (n)=>
      {
      console.log(n);
      this.directves.push(n);
      });

}

}
