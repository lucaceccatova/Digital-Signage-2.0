import { NgModule } from '@angular/core';
import {Routes,RouterModule} from '@angular/router'
import { VideoGalleryComponent } from './video-gallery.component';
import { fullScreenVideo } from '../fullScreenVideo/fullScreenVideo.component';

const routes:Routes=[
    {
        path:"",
        component:VideoGalleryComponent
    },
    {
        path:"media",
        component:fullScreenVideo
    }
];
@NgModule({
    imports: [ RouterModule.forChild(routes) ],
    exports: [RouterModule]
})
export class VideoRoutingModule {}