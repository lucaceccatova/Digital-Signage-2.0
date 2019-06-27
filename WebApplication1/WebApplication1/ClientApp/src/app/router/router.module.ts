import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule,Routes} from '@angular/router';
import { AppComponent } from '../app.component';
const MyRoutes:Routes=[
  {
    path: "",
    redirectTo:'slider',
    pathMatch:"full",
  },
  {
    path:"",
    component:AppComponent,
  children:[
    {
      path:"slider",
      loadChildren:() => import("../my_module/Slider/slider.module").then(m=>m.SliderModule)

    },
    {
      path:"video",
      loadChildren:()=> import ("../my_module/video-gallery/video-gallery.module").then(m=>m.VideoGalleryModule)
    }
  ]
}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(MyRoutes)
  ],
  exports:[RouterModule]
})
export class myRouter { }
