import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TireSelectionComponent } from './tire-selection.component';
const routes:Routes =[
    {
        path:"",
        component:TireSelectionComponent
    }
]
@NgModule({
    declarations: [],
    imports: [ RouterModule.forChild(routes) ],
    exports: [RouterModule],
    providers: [],
})
export class tireRouterModule {}