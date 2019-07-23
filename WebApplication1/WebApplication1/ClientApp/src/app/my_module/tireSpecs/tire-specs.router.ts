import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { tireSpecsComponent } from './tire-specs.component';



const routes: Routes = [
  
    { path: '', component: tireSpecsComponent }

    //{ path: 'path/:routeParam', component: MyComponent },
    //{ path: 'staticPath', component: ... },
    //{ path: '**', component: ... },
    //{ path: 'oldPath', redirectTo: '/staticPath' },
    //{ path: ..., component: ..., data: { message: 'Custom' }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class tireSpecsRouter {}
