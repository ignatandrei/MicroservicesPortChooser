export class ISystem_Environment{
    public CurrentDirectory: string='';
    public CommandLine: string='';
    public ProcessorCount: string='';
    public MachineName : string='';
    public UserDomainName : string='';
    public UserName : string='';
    public Is64BitProcess : boolean=false;
    public Is64BitOperatingSystem : boolean=false;
    
}
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
    envCache : ISystem_Environment|null = null;
    get EnvironmentData(): ISystem_Environment|null {
        try{
            if(this.envCache != null)
                return this.envCache;

            this.envCache= JSON.parse(this.envData) as ISystem_Environment;
            return this.envCache;
        }
        catch(e){
            return null;
        }
    }
}