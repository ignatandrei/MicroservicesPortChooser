import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Register } from '../classes/register';

@Injectable({
  providedIn: 'root'
})
export class MPCService {
  constructor(private http: HttpClient) { }
  
  deterministic(appName: string) : Observable<string> {
    var url =environment.url +'api/v1/PortChooser/GetDeterministicPortFrom/'+appName;
    return this.http.get<string>(url);
  }
  nonDeterministic(appName: string) : Observable<string> {
    var url =environment.url +'api/v1/PortChooser/GetNonDeterministicPortFrom/'+appName;
    return this.http.get<string>(url);
  }

  deterministicV2(appName: string, tag: string) : Observable<string> {
    var url =environment.url +'api/v2/PortChooser/GetDeterministicPortFrom/'+appName + "/"+tag;
    return this.http.get<string>(url);
  }
  getRegisterMS() : Observable<Register[]> {
    var url =environment.url +'api/v1/Register/GetAll/';
    return this.http.get<Register[]>(url);
  }
  loadFromDatabase(): Observable<number> {
    var url =environment.url +'api/v1/Register/LoadFromDatabase/';
    return this.http.get<number>(url);
  }
  deleteFromDatabase(port: number, host:string) : Observable<boolean> {
    var url =environment.url +'api/v1/Register/UnRegister/'+ host + "/"+ port;
    return this.http.delete<boolean>(url);
  }
}