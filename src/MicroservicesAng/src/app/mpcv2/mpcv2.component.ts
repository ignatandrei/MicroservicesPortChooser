import { Component, OnInit } from '@angular/core';
import { MPCService } from '../services/mpcv1.service';

@Component({
  selector: 'app-mpcv2',
  templateUrl: './mpcv2.component.html',
  styleUrls: ['./mpcv2.component.css']
})
export class Mpcv2Component implements OnInit {

  appName: string = '';
  pcName: string=  'localhost';
  tag: string = '';
  fullName: string = '';
  constructor(private MPCService: MPCService) { }

  ngOnInit(): void {

  }
  constructFullName(port:string): string{
    this.fullName = 'http://'+ (this.pcName ||'localhost') + ':' + port;
    return this.fullName;
  } 
  
  onDeterministic(): void {

     this.MPCService.deterministicV2(this.appName,this.tag).subscribe(
       v=> window.alert(this.constructFullName(v))
     );
  }

}
