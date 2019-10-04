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
  //al modules are lazy loaded for better performance cause there are a lot of subscription and
  //media around the app. the signage could also load all component on start but
  //in that case some services had to be rewritten like slider service


  //pros: easier to modify and to add some external module 
  //cons: app is way slower on changing context cause it had to load a new module and
  //also destroy old module(this is the reason cause some services had to be rewritten in
  //case of a structural change)
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
      loadChildren:()=> import("../my_module/video-gallery/video-gallery.module").then(m=>m.VideoGalleryModule)
    },
    {
      path:"tire",
      loadChildren:()=> import("../my_module/tireShow/tire-show.module").then(m=>m.tireShowModule)
    },
    {
      path:"specs",
      loadChildren:()=> import ("../my_module/tireSpecs/tire-specs.module").then(m=>m.tireSpecsModule)
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
