import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { myRouter } from './router/router.module';

@NgModule({
  declarations: [
    AppComponent,    
  ],
  imports: [
    BrowserModule,
    myRouter
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
