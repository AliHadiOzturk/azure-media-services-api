import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private baseUrl = 'http://localhost:5000/'
  private url = 'document';
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl + this.url, { params: null, headers: null });
  }

  getDocument(docId) {
    return this.http.get(this.baseUrl + this.url + '/getDocument', { params: new HttpParams().set('documentId', docId), headers: null });
  }



}
