import { HttpClient, HttpEventType, HttpRequest } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DocumentService } from 'src/app/services/document.service';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrls: ['./document-list.component.css']
})
export class DocumentListComponent implements OnInit {
  private baseUrl = 'http://localhost:5000/'
  private url = 'content';
  documents;
  selectedDocument;
  progress = 0;

  myPlayer;
  constructor(private fileService: FileService, private http: HttpClient, private documentService: DocumentService, private router: Router) {
    this.getDocuments();
  }

  ngOnInit() {
  }

  onFileUploaded(event) {
    for (let file of event.target.files) {
      let formData: FormData = new FormData();
      formData.append("file", file)
      formData.append("fileName", String(file.name));
      // this.fileService.uploadFile(formData).subscribe(resp => {
      //   console.log(resp)
      // })
      // .set('Content-Type', 'application/json')

      const options = { params: null, headers: null }
      //  this.setHeaders();

      const request = new HttpRequest('POST', this.baseUrl + this.url, formData, { ...options, reportProgress: true });
      this.http.request(request).subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        }
        else if (event.type === HttpEventType.Response)
          this.getDocuments();
      });

    }
  }
  getDocuments() {
    this.documentService.getAll().subscribe(resp => {
      this.documents = resp;
      console.log(this.documents)
    })
  }

  fileClicked(event, doc) {
    this.fileService.publish(doc.id).subscribe(resp => {
      // this.getDocuments();
      alert("File Publsih edildi")
    })
  }

  selectDocumentForStream($event, doc) {
    this.documentService.getDocument(doc.id).subscribe(resp => {

      this.router.navigate(['/watch', doc.id], { queryParams: { 'returnUrl': this.router.url } })

    })
  }

}
