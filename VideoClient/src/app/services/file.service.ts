import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  private baseUrl = 'http://localhost:5000/'
  private url = 'content';
  constructor(private http: HttpClient) { }
  publish(docId) {
    const options = { params: new HttpParams().set('documentId', docId), headers: null }
    return this.http.get(this.baseUrl + this.url, options)
  }

}
