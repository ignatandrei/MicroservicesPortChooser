
export class Register{

    public constructor( r: Register | null){
        if(r){
            Object.assign(this, r);
            this.history = r.history?.map(h => new Register(h));
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
    set Details(details:string){
        //do nothing
    }
    history:Register[] = [];
}