import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { tireSpecsComponent } from './tire-specs.component';
import { tireSpecsRouter } from './tire-specs.router';

@NgModule({
    declarations: [tireSpecsComponent],
    imports: [ CommonModule,tireSpecsRouter ],
    exports: [],
    providers: [],
})
export class tireSpecsModule {}