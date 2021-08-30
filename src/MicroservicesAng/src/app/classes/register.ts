
export class Register{

    public constructor( r: Register | null){
        if(r){
            Object.assign(this, r);
            this.Details=JSON.stringify(this);
        }    
    }   
    dateRegistered : Date = new Date();
    uniqueID: string = "";
    name :string= "";
    hostName :string= "";
    port : number=0;
    tag : string= "";
    pcName:string = "";

    Details : string="asdasd";
}