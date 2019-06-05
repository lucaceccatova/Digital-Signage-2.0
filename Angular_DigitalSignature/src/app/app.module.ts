import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {element} from './Model/element';
import { AppComponent } from './app.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { SliderComponent } from './slider/slider.component';
import {HubConnection} from '@aspnet/signalr';
import { HeaderComponent } from './header/header.component';


@NgModule({
  declarations: [
    AppComponent,
    SliderComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
