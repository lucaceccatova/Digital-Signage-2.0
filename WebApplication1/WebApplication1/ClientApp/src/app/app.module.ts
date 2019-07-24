import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { myRouter } from './router/router.module';
import { shareElementsService } from './Services/sharedServices/shareElement.Service';
import { FiltersPipe } from './Pipes/filters.pipe';

@NgModule({
  declarations: [
    AppComponent,
    FiltersPipe
    ],
  imports: [
    BrowserModule,
    myRouter,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
