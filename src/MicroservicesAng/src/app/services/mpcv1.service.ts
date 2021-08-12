import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MPCv1Service {
  constructor(private http: HttpClient) { }
  
  deterministic(appName: string) : Observable<string> {
    var url =environment.url +'api/v1/PortChooser/GetDeterministicPortFrom/'+appName;
    return this.http.get<string>(url);
  }

  
}
