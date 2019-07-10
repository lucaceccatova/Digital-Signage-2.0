import { Injectable } from '@angular/core';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root' // means that only one instance of this service will be initialized for all modules
})

export class SignalRService {
  connection:HubConnection;
  constructor(private router:Router) { 
    this.connect();
    this.defaultMethod();

  }
  ngOnInit() {
    }
  connect()
  {
    this.connection=new HubConnectionBuilder().withUrl('https://localhost:44303/voice')
    .configureLogging(signalR.LogLevel.Information)
    .build();
    //start connection
      this.connection
      .start()
      .then(()=>console.log("connection started"))
      .catch(err=>console.log("Errore di connessione"));
      //initialize connection
  }
  disconnect(methodName :string)
  {
    this.connection.off(methodName);
  }

  defaultMethod()
  {
    this.connection.onclose(()=>this.connect());
    this.connection.on('goToSlide',(data=>
      {
        if(data==true)
        {
          
          this.router.navigateByUrl("/slider");
        }
      }));
  }
  //may move all Invoke functions in this service for better mantainance of code
}
