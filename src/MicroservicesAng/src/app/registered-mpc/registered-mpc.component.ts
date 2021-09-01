import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Register } from '../classes/register';
import { MPCService } from '../services/mpcv1.service';
import {Sort} from '@angular/material/sort';
import Tabulator from 'tabulator-tables';
import { AngularCsv } from 'angular-csv-ext/dist/Angular-csv';
import { ShepherdService } from 'angular-shepherd';
import { registerLocaleData } from '@angular/common';


@Component({
  selector: 'app-registered-mpc',
  templateUrl: './registered-mpc.component.html',
  styleUrls: ['./registered-mpc.component.css']
})
export class RegisteredMPCComponent implements OnInit , AfterViewInit {

  tab = document.createElement('div');
  showTabulator: boolean = false;

  dataFiltered: Register[] = [];
  data: Register[]=[];
  constructor(private mpcService: MPCService, private shepherdService: ShepherdService) { }
  onHelp(): void {
    this.shepherdService.show('intro');
  }
  ngAfterViewInit() {
    var buttons = [
      {
        classes: 'shepherd-button-secondary',
        text: 'Exit',
        type: 'cancel',
      },
      {
        classes: 'shepherd-button-primary',
        text: 'Back',
        type: 'back',
      },
      {
        classes: 'shepherd-button-primary',
        text: 'Next',
        type: 'next',
      },
    ];
    this.shepherdService.steps = [];
    if (this.shepherdService.tourObject) {
      this.shepherdService.tourObject.steps = [];
    }


    this.shepherdService.defaultStepOptions = {
      cancelIcon: {
        enabled: true,
      },
      modal: true,
      classes: 'custom-class-name-1 custom-class-name-2',
      highlightClass: 'highlight',
      scrollTo: false,
      title: 'Welcome to Port List!',
    };
    this.shepherdService.modal = true;
    this.shepherdService.confirmCancel = false;
    this.shepherdService.requiredElements = [
      {
        selector: '.first-element-help',
        message: 'No found intro.',
        title: 'MPCV1',
      },
      
    ];
    //     this.shepherdService.steps = [];
    // this.shepherdService.tourObject.steps = [];
    this.shepherdService.modal = true;
    this.shepherdService.addSteps([
      {
        id: 'intro',
        attachTo: {
          element: '.first-element-help',
          on: 'bottom',
        },
        text: [
          'Click next for tour or exit if you know how to use already the app',
        ],
        buttons: buttons,
      },

      {
        id: 'download',
        attachTo: {
          element: '.download-help',
          on: 'right',
        },
        text: ['Click to download the list of registered microservices'],
        buttons: buttons,
      },
      {
        id: 'list',
        attachTo: {
          element: '.listMSPC-help',
          on: 'bottom',
        },
        text: ['Here you can see the list of registered microservices'],
        buttons: buttons,
      },
    ]);
    // this.shepherdService.addSteps(defaultSteps);
  }
  ngOnInit(): void {
    this.LoadData();
  }
  exportToCSV() {
    new AngularCsv(this.data, 'RegisteredMPC', {
      headers: ['uniqueID', 'hostName','port', 'name', 'dateRegistered', 'tag', 'pcName','Details'],
      showLabels: true,
      useHeader : true, 
    });
  }

  LoadData() : void {
    this.mpcService.getRegisterMS().subscribe(
      data => {
        this.data = data.map(it=>new Register(it));
        this.dataFiltered = this.data;
        this.drawTable(this.data);
      }
    );

  }
  private drawTable(tableData:Register[]): void {
    // console.log(tableData)
    var hot= new Tabulator(this.tab, {
      data: tableData,
      clipboard:true,           
      reactiveData:true, //enable data reactivity
      columns: [
        {title:"ID", field:"name", formatter:function(cell:any, formatterParams:any, onRendered:any){
          return JSON.stringify( cell.getRow().getPosition()+1) ; 
          
       },
      // cellClick: (e, cell) => {
      //   var r = cell.getData() as Register;
      //   this.deleteFromDatabase(r); 
      // }
    
       },
       {title:"hostName", field:"hostName", sorter:"string", headerFilter:true},
        {title:"Name", field:"name", sorter:"string",  headerFilter:true} ,
        {title:"Port", field:"port", sorter:"string"  ,headerFilter:true},
        {title:"Date Registered", field:"dateRegistered", sorter:"string"  ,headerFilter:true},
      
        {title:"tag", field:"tag", sorter:"string"  ,headerFilter:true},
        {title:"pcName", field:"pcName", sorter:"string" ,headerFilter:true},
        {title:"Details", field:"Details", sorter:"string" ,headerFilter:true},
        
      ],
      layout:"fitColumns",      
    });
    var x =document.getElementById('my-tabular-table');
    if(x) x.appendChild(this.tab);
    hot.redraw(true);
  }
  applyFilter(event: Event) {
    let filterValue = (event.target as HTMLInputElement).value;
    filterValue= filterValue.toLowerCase().trim();
    if(filterValue==""){
      this.dataFiltered=this.data;
      return;
    }

    this.dataFiltered = this.data.filter(d => d.Details.toLowerCase().indexOf(filterValue) > 1);
    // console.log(this.dataFiltered);
  }
  compare(a: number | string |Date, b: number | string|Date, isAsc: boolean) : number {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  sortData(sort: Sort) {
    const data1 = this.dataFiltered.slice();
    if (!sort.active || sort.direction === '') {
      this.dataFiltered = data1;
      return;
    }

    this.dataFiltered = data1.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
      
        case 'name': return this.compare(a.name, b.name, isAsc);
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

