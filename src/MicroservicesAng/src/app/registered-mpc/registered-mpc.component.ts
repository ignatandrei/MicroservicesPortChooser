import { Component, OnInit } from '@angular/core';
import { Register } from '../classes/register';
import { MPCService } from '../services/mpcv1.service';

@Component({
  selector: 'app-registered-mpc',
  templateUrl: './registered-mpc.component.html',
  styleUrls: ['./registered-mpc.component.css']
})
export class RegisteredMPCComponent implements OnInit {

  data: Register[]=[];
  constructor(private mpcService: MPCService) { }

  ngOnInit(): void {
    this.mpcService.getRegisterMS().subscribe(
      data => this.data = data
    );
  }
}
