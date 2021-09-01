
export class Register{

    public constructor( r: Register | null){
        if(r){
            Object.assign(this, r);
            
        }    
    }   
    dateRegistered : Date = new Date();
    uniqueID: string = "";
    name :string= "";
    hostName :string= "";
    port : number=0;
    tag : string= "";
    pcName:string = "";

    get Details():string {
        return JSON.stringify(this);
    }
}