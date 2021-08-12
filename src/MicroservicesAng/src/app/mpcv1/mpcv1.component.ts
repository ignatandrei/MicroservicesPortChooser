import { Component, OnInit } from '@angular/core';
import { MPCv1Service } from '../services/mpcv1.service';

@Component({
  selector: 'app-mpcv1',
  templateUrl: './mpcv1.component.html',
  styleUrls: ['./mpcv1.component.css']
})
export class MPCv1Component implements OnInit {

  appName: string = '';
  pcName: string=  'localhost';
  fullName: string = '';
  constructor(private mpcv1Service: MPCv1Service) { }

  ngOnInit(): void {

  }
  constructFullName(port:string): string{
    this.fullName = 'http://'+ (this.pcName ||'localhost') + ':' + port;
    return this.fullName;
  } 
  
  onDeterministic(): void {

     this.mpcv1Service.deterministic(this.appName).subscribe(
       v=> window.alert(this.constructFullName(v))
     );
  }

  onNonDeterministic(): void {
    this.mpcv1Service.nonDeterministic(this.appName).subscribe(
      v=> window.alert(this.constructFullName(v))
    );
  }

}
