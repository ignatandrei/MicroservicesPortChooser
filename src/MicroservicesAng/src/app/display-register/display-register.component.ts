import { Component, Inject, Input, OnInit } from '@angular/core';
import { Sort } from '@angular/material/sort';
import { Register } from '../classes/register';
import { MPCService } from '../services/mpcv1.service';
import Tabulator from 'tabulator-tables';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-display-register',
  templateUrl: './display-register.component.html',
  styleUrls: ['./display-register.component.css']
})
export class DisplayRegisterComponent implements OnInit {
  filterString=["NotContain", "Contains"];
  myLink: string = "";
  selectedFilter: string = "Contains";
  filterUser:string="";
  private dataInner: Register[] = [];
  ShowHistory:boolean =true;  
  get data(): Register[] {
    return this.dataInner;
  }
  
  @Input() 
  set data(value: Register[]) {
    //window.alert("set" + value?.length)
    this.dataInner = value;
    if (this.dataInner?.length > 0) {
      this.dataFiltered = this.dataInner;
      this.drawTable(this.dataInner);
    }
  }
  
  tab = document.createElement('div');
  showTabulator: boolean = false;

  dataFiltered: Register[] = [];

  constructor(private mpcService: MPCService , public dialog: MatDialog, @Inject(MAT_DIALOG_DATA) dataHistory: Register[]) { 

    if(dataHistory?.length> 0){
      this.ShowHistory = false;
      var sorted=dataHistory.sort((a,b)=>{
        try {
          return b.dateRegistered.getTime() -a.dateRegistered.getTime();  
        } catch (error) {
          return -1;
        }
        });
      this.data=sorted;
    }

  }

  openDialog(r:Register): void {
    //window.alert(r.history.length);
    const dialogRef = this.dialog.open(DisplayRegisterComponent, {
      //width: '250px',
      data: r.history,
      maxHeight: '90vh',
      
    });

    // dialogRef.afterClosed().subscribe(result => {
    //   console.log('The dialog was closed');
    //   this.animal = result;
    // });
  }


  ngOnInit(): void {
    this.dataFiltered = this.data;
    this.drawTable(this.data);

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
    this.filterUser= (event.target as HTMLInputElement).value;
    this.filterUser= this.filterUser.toLowerCase().trim();
    console.log(event); 
    this.myLink = environment.url + "static/services?"+ this.selectedFilter+ "=" + this.filterUser;
    this.applyFilterValue(this.filterUser);
  }
  applyFilterDefault(){
    this.applyFilterValue(this.filterUser);
  }
  applyFilterValue(value: string){

    value=value||'';
    console.log(value);
    if(value===""){
      console.log('b');
      this.dataFiltered=this.data;
      return;
    }
    console.log('asd');
    console.log(this.selectedFilter);
    switch (this.selectedFilter) {
      case "Contains":
          this.dataFiltered = this.data.filter(d => d.Details.toLowerCase().indexOf(value) > 1);
          break;
      case "NotContain":
          this.dataFiltered = this.data.filter(d => d.Details.toLowerCase().indexOf(value) < 0);
          break;
      default:
          window.alert("Invalid filter:"+ this.selectedFilter);    
    }
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
  deleteFromDatabase(register: Register) {
    this.mpcService.deleteFromDatabase(register.port,register.hostName).subscribe(
      it => {
        var data1 = this.data.filter(it=>!(it.hostName == register.hostName && it.port == register.port));
        this.data = data1;
      }
    );
  }

}
