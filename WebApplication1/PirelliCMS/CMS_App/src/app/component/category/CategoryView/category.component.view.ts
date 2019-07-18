import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { GetListService } from 'src/app/services/getListService/get-list.service';
import { apiService } from 'src/app/services/PostService/post.service';
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

  list:listMedia[]=[];

  urlRemove="https://localhost:44303/api/deletecategory";
  constructor(private http:GetListService,
     private api:apiService,private dialog:MatDialog) { }


  ngOnInit() {
    this.getList();
  }
  getList()
  {
    this.http.getList("https://localhost:44303/api/getCategories").subscribe(data=>{
    this.list=data;
    });
  }

  viewUpdateDelete(id:number)
  {
    const dialogResponse=this.dialog.open(UpateDeleteComponent,{
      data:{
          category:this.list[id]
        }
    } );
    dialogResponse.afterClosed().subscribe(data=>
        {
          if(data=='delete')
          {
            this.deleteMedia(this.list[id].id);
            this.list.splice(id, 1);
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
