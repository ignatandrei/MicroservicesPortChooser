import { Component, OnInit } from '@angular/core';
import { Register } from '../classes/register';
import { MPCService } from '../services/mpcv1.service';
import {Sort} from '@angular/material/sort';
import Tabulator from 'tabulator-tables';
@Component({
  selector: 'app-registered-mpc',
  templateUrl: './registered-mpc.component.html',
  styleUrls: ['./registered-mpc.component.css']
})
export class RegisteredMPCComponent implements OnInit {

  tab = document.createElement('div');

  data: Register[]=[];
  constructor(private mpcService: MPCService) { }

  ngOnInit(): void {
    this.LoadData();
  }
  LoadData() : void {
    this.mpcService.getRegisterMS().subscribe(
      data => {
        this.data = data;
        this.drawTable(data);
      }
    );

  }
  private drawTable(tableData:Register[]): void {
    var hot= new Tabulator(this.tab, {
      data: tableData,
      clipboard:true,           
      reactiveData:true, //enable data reactivity
      columns: [
        {title:"ID", field:"name", formatter:function(cell, formatterParams, onRendered){
          return JSON.stringify( cell.getRow().getPosition()+1) ; 
          
       },
      // cellClick: (e, cell) => {
      //   var r = cell.getData() as Register;
      //   this.deleteFromDatabase(r); 
      // }
    
       },
       {title:"hostName", field:"hostName", sorter:"string", headerFilter:true},
        {title:"Name", field:"name", sorter:"string",  headerFilter:true} ,
        {title:"dateRegistered", field:"dateRegistered", sorter:"string"  ,headerFilter:true},
        {title:"tag", field:"tag", sorter:"string"  ,headerFilter:true},
        {title:"pcName", field:"pcName", sorter:"string" ,headerFilter:true},
        {title:"Details", field:"pcName", sorter:"string", formatter:function(cell, formatterParams, onRendered){
      
          return JSON.stringify( cell.getData()); //return the contents of the cell;
      }},
        

      ],
      layout: 'fitColumns'
      
    });
    var x =document.getElementById('my-tabular-table');
    if(x) x.appendChild(this.tab);
    hot.redraw(true);
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

