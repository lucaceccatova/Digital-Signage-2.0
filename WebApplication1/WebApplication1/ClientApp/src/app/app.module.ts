import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { myRouter } from './router/router.module';
import { FiltersPipe } from './Pipes/filters.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [    
    AppComponent,
    FiltersPipe,
    
    ],
  imports: [
    BrowserModule,BrowserAnimationsModule,
    myRouter,
  ],
  exports:[],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
