import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { tireSpecsComponent } from './tire-specs.component';
import { tireSpecsRouter } from './tire-specs.router';
import { preserveSpecService } from 'src/app/Services/preserveSpecComponent/preserveSpec.service';
import { MatSnackBarModule } from '@angular/material';

@NgModule({
    declarations: [tireSpecsComponent
    ],
    imports: [ CommonModule,tireSpecsRouter,MatSnackBarModule, ],
    providers: [preserveSpecService],
    entryComponents:[]
})
export class tireSpecsModule {}