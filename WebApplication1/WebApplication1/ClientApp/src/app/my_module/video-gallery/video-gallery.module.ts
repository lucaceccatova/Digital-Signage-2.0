import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GetMediaService } from 'src/app/Services/GetMedia/get-media.service';
import { VideoRoutingModule } from './videoGalleryRoutingModule';
import { VideoGalleryComponent } from './video-gallery.component';
import { HttpClientModule } from '@angular/common/http';
import { fullScreenVideo } from './fullScreenVideo/fullScreenVideo.component';
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { shareElementsService } from 'src/app/Services/shareElementsServie/shareElement.Service';
@NgModule({
  declarations: [VideoGalleryComponent,fullScreenVideo],
  imports: [
    CommonModule,
    VideoRoutingModule,
    HttpClientModule,
    
  ],
  exports:[VideoGalleryComponent],
  providers:[GetMediaService,sharedStringService,shareElementsService]
})
export class VideoGalleryModule { }
