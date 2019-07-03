import { Injectable } from '@angular/core';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  connection:HubConnection;
  constructor() { }

  ngOnInit() {
   
    }
  connect()
  {
    this.connection=new HubConnectionBuilder().withUrl('https://localhost:44303/voice')
    .configureLogging(signalR.LogLevel.Information)
    .build();
      this.connection
      .start()
      .then(()=>console.log("connection started"))
      .catch(err=>console.log("Errore di connessione"));
      //initialize connection
  }
}
