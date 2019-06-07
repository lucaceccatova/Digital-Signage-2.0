export class element
{  
    id:number;
    name:string;
    description:string;
    value:number;
    timer:number;
    create_date:Date;
    path:string;  
    constructor(id:number,name:string,description:string,value:number,timer:number,createdate:Date,path:string)
    {
        this.id=id;
        this.description=description;
        this.create_date=createdate;
        this.value=value;
        this.timer=timer;
        this.name=name;
        this.path=path;
    }

}