import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { tireShowComponent } from './tire-show.component';

const routes: Routes = [
    { path: '', component: tireShowComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class tireShowRouter {}
