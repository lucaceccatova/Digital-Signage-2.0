import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr'
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import { Observable, observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ServerListnerService {
  private connection : HubConnection;
  
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

  



   public getDirective(): any {
    const directiveObservable = new Observable(observer => {
      this.connection.on("sendID", (n)=>
      {
      console.log(n);
      this.id=n;
      });
      observer.next(this.id);
    });
    return directiveObservable;

}
}
