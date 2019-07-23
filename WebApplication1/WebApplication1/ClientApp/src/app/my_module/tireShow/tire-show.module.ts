import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { tireShowComponent } from './tire-show.component';
import { tireShowRouter } from './tire-show-router';

@NgModule({
    declarations: [tireShowComponent],
    imports: [ CommonModule,tireShowRouter],
    exports: [],
    providers: [],
})
export class tireShowModule {}