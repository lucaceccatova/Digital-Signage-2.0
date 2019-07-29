import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GetMediaService } from 'src/app/Services/GetMedia/get-media.service';
import { VideoRoutingModule } from './videoGalleryRoutingModule';
import { VideoGalleryComponent } from './video-gallery.component';
import { HttpClientModule } from '@angular/common/http';
import { fullScreenVideo } from '../fullScreenVideo/fullScreenVideo.component';
@NgModule({
  declarations: [VideoGalleryComponent,fullScreenVideo],
  imports: [
    CommonModule,
    VideoRoutingModule,
    HttpClientModule,
  ],
 // exports:[VideoGalleryComponent],
  providers:[GetMediaService]
})
export class VideoGalleryModule { }
