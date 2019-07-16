import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatToolbarModule, MatTabsModule} from '@angular/material'
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { DashboardElementComponent } from './component/dashboard-element/dashboard-element.component';
import { FeatureRoutingModule } from './router-module';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule,MatCardModule} from '@angular/material';
import {MatSliderModule} from '@angular/material/slider';
import {MatSelectModule} from '@angular/material/select';
import { HttpClientModule } from '@angular/common/http';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatButtonModule} from '@angular/material/button';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import {MatIconModule} from '@angular/material/icon';
import { HomeComponent } from './component/home/home.component';
import { CategoryComponent } from './component/category/categoryAdd/category.component.add';
import { UploadComponent } from './component/upload/mediaAdd/upload.component';
import { MediaComponent } from './component/upload/tabs/media';
import { TabsComponent } from './component/category/tabs/tabs.component';
import { CategoryComponentView } from './component/category/CategoryView/category.component.view';
@NgModule({
  declarations: [
    AppComponent,
    DashboardElementComponent,
    UploadComponent,
    HomeComponent,
    CategoryComponent,
    MediaComponent,
    TabsComponent,
    CategoryComponentView
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    FormsModule,
    ReactiveFormsModule,
    FeatureRoutingModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSliderModule,
    MatSelectModule ,
    HttpClientModule,
    MatProgressBarModule  ,
    MatButtonModule,
    MaterialFileInputModule,
    MatIconModule,
    MatTabsModule,MatIconModule    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }