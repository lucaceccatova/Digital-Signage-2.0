import { Routes, RouterModule } from '@angular/router';
import { NgModule, Component } from '@angular/core';
import { DashboardElementComponent } from './component/dashboard-element/dashboard-element.component';
import { UploadComponent } from './component/upload/upload.component';
import { HomeComponent } from './component/home/home.component';
import { CategoryComponent } from './component/category/category.component';


const routes: Routes = [
    {
        path:"",
        component:HomeComponent,
    },
    {path:"upload", component:UploadComponent,},
    {path:"category",component:CategoryComponent}
    

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class FeatureRoutingModule {}
