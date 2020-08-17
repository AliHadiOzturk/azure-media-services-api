import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DocumentService } from 'src/app/services/document.service';
@Component({
  selector: 'app-watch',
  templateUrl: './watch.component.html',
  styleUrls: ['./watch.component.css']
})
export class WatchComponent implements OnInit {

  src;
  id;
  returnUrl = '';
  constructor(private documentService: DocumentService, private router: Router, private route: ActivatedRoute) {
    this.route.params.subscribe(param => {
      this.id = param['id']
      console.log(param)
      this.getVideo();
    })
    this.route.queryParams.subscribe(param => {
      this.returnUrl = param['returnUrl']
    })
  }

  ngOnInit() {
  }
  goBack() {
    this.router.navigateByUrl(this.returnUrl)
  }
  getVideo() {
    this.documentService.getDocument(this.id).subscribe(resp => {
      let data: any = resp;
      let streamingData = data.streamingUrls.find(a => a.streamingProtocol == 3)
      this.src = streamingData.url;
      // console.log(this.src)

    })
  }

  onSeeking(event) {

  }
  onEnded(event) {

  }

}
