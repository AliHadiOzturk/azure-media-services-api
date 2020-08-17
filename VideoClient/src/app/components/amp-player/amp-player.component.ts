import { Component, EventEmitter, Input, OnChanges, OnInit, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-amp-player',
  templateUrl: './amp-player.component.html',
  styleUrls: ['./amp-player.component.css']
})
export class AmpPlayerComponent implements OnInit, OnChanges {


  @ViewChild('video') videoPlayer;
  @Input() id;
  @Input() src;
  @Input() autoplay = false;
  @Input() width;
  @Input() height;
  @Input() containerStyle;
  @Output() ended: EventEmitter<any> = new EventEmitter();
  @Output() seeking: EventEmitter<any> = new EventEmitter();
  myPlayer: amp.Player;
  constructor() { }

  ngOnInit() {
    // Dynamically load the amp player control


  }

  ngOnChanges(changes: import("@angular/core").SimpleChanges): void {

    this.myPlayer = amp(this.videoPlayer.nativeElement, {


      autoplay: false,
      controls: true,
      fluid: true,

      logo: { enabled: false },
    }
    );
    this.myPlayer.width(500);
    if (this.src)
      this.myPlayer.src([{
        src: this.src,
        type: "application/vnd.ms-sstr+xml"
      },]);

    // // Add playback ended event listener
    // this.myPlayer.addEventListener('ended', this.amp_ended);
    // this.myPlayer.addEventListener('seeking', this.amp_seeking);
  }

  amp_ended() {
    console.log("AMP::Playback ended");
    this.ended.emit(null);
  }

  amp_seeking() {
    console.log("AMP::Seek event");
    this.seeking.emit(null);
  }

}
