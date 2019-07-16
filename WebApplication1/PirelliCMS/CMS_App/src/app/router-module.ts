import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './component/home/home.component';
import { TabsComponent } from './component/category/tabs/tabs.component';
import { MediaComponent } from './component/upload/tabs/media';


const routes: Routes = [
    {
        path:"",
        component:HomeComponent,
    },
    {path:"media", component:MediaComponent,},
    {path:"category",component:TabsComponent}
    

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class FeatureRoutingModule {}
