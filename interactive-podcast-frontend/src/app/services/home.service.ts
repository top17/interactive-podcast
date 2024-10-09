import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  private apiUrl = 'http://localhost:5008/api/home';

  constructor(private http: HttpClient) {}

  getWelcomeMessage(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
