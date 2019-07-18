import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { GetObjectService } from 'src/app/services/getListService/get-object.service';
import { apiService } from 'src/app/services/ApiService/Api.service';
import { MatDialog } from '@angular/material';
import { UpateDeleteComponent } from '../upate-delete/dialogCategory/upate-delete.component';
import { categoryDialog } from '../are-you-sure-about-it/are-you-sure-about-it.component';

@Component({
  selector: 'app-category-view',
  templateUrl: './category.component.view.html',
  styleUrls: ['./category.component.view.scss']
})

export class CategoryComponentView implements OnInit {
@Output() public onUploadFinished = new EventEmitter();

urlApi="/assets/Json/loadeddata.list.json";
  list:listMedia[]=[];
  ngRock="https://883c5b7c.ngrok.io/";
  urlRemove="https://localhost:44303/api/deletecategory";
  constructor(private http:GetObjectService,
     private api:apiService,private dialog:MatDialog) { }


  ngOnInit() {
    this.getList();
  }
  getList()
  {
    this.http.getList(this.urlApi).subscribe(data=>{
    this.list=data;
    });
  }

  viewUpdateDelete(id:number)
  {
    const dialogResponse=this.dialog.open(UpateDeleteComponent,{
      data:{
          object:this.list[id],
          message:"Are you sure to delete this Category?"
        }
    } );
    dialogResponse.afterClosed().subscribe(data=>
        {
          if(data=='delete')
          {
            this.deleteMedia(this.list[id].id);
            this.list.splice(id,1);
          }
        }
      );
  }
  deleteMedia(id:number)
  {
    this.api.get_ID(this.urlRemove,id).subscribe(data=>
      {
        alert("cancellato");
      });
  }
  trackByFn(i: number) { 
    return i
  }
}
