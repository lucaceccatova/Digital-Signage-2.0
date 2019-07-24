import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { tireSpecsComponent } from './tire-specs.component';
import { tireSpecsRouter } from './tire-specs.router';
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { preserveSpecService } from 'src/app/Services/preserveSpecComponent/preserveSpec.service';

@NgModule({
    declarations: [tireSpecsComponent],
    imports: [ CommonModule,tireSpecsRouter ],
    exports: [],
    providers: [sharedStringService,preserveSpecService],
})
export class tireSpecsModule {}