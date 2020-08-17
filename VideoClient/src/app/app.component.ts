import { AfterViewChecked, Component, ViewChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewChecked {

  /**
   *
   */
  
  @ViewChild('video') videoPlayer;
  constructor() {
    

  }

  ngAfterViewChecked(): void {
  }

  

  // documentSelectionChanged() {


  //   // Add playback ended event listener
  //   // myPlayer.addEventListener('ended', this.amp_ended);
  //   // myPlayer.addEventListener('seeking', this.amp_seeking);
  //   if (this.selectedDocument) {
  //     let url = this.selectedDocument.streamingUrls.find(a => a.streamingProtocol == 3)
  //     console.log(url.url)
  //   }
  // }
}

