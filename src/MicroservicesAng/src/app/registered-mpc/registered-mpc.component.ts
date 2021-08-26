import { Component, OnInit } from '@angular/core';
import { Register } from '../classes/register';
import { MPCService } from '../services/mpcv1.service';
import {Sort} from '@angular/material/sort';
@Component({
  selector: 'app-registered-mpc',
  templateUrl: './registered-mpc.component.html',
  styleUrls: ['./registered-mpc.component.css']
})
export class RegisteredMPCComponent implements OnInit {

  data: Register[]=[];
  constructor(private mpcService: MPCService) { }

  ngOnInit(): void {
    this.LoadData();
  }
  LoadData() : void {
    this.mpcService.getRegisterMS().subscribe(
      data => this.data = data
    );

  }
  compare(a: number | string |Date, b: number | string|Date, isAsc: boolean) : number {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  sortData(sort: Sort) {
    const data1 = this.data.slice();
    if (!sort.active || sort.direction === '') {
      this.data = data1;
      return;
    }

    this.data = data1.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'uniqueID': return this.compare(a.uniqueID, b.uniqueID, isAsc);
        case 'dateRegistered': return this.compare(a.dateRegistered, b.dateRegistered, isAsc);
        case 'tag': return this.compare(a.tag, b.tag, isAsc);
        default: return 0;
      }
    });
  }

  loadFromDatabase() {
    this.mpcService.loadFromDatabase().subscribe(
      it => this.LoadData()
    );
  }
  deleteFromDatabase(register: Register) {
    this.mpcService.deleteFromDatabase(register.port,register.hostName).subscribe(
      it => this.LoadData()
    );
  }
}

