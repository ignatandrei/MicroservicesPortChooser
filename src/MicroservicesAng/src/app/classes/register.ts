
export class Register{

    public constructor( r: Register | null){
        if(r){
            Object.assign(this, r);
            this.history = r.history?.map(h => new Register(h));
        }    
        this.history=this.history||[];
    }   
    dateRegistered : Date = new Date();
    uniqueID: string = "";
    name :string= "";
    hostName :string= "";
    port : number=0;
    tag : string= "";
    pcName:string = "";
    envData: string="";
    get Details():string {
        const { history, ...rest } = this;
        return JSON.stringify(rest);
    }
    set Details(details:string){
        //do nothing
    }
    history:Register[] = [];

    get EnvironmentData(): any{
        try{
            return JSON.parse(this.envData);
        }
        catch(e){
            return {};
        }
    }
}