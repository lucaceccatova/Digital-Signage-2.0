import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-dashboard-element',
  templateUrl: './dashboard-element.component.html',
  styleUrls: ['./dashboard-element.component.scss']
})
export class DashboardElementComponent implements OnInit {

  elements=[
    {name:"Home", path:"",icon:'fas fa-cloud-upload-alt'},
    {name:"New media",path:"/upload",icon:'fas fa-cloud-upload-alt'},
    {name:"New category",path:"/category",icon:''},

  ];
  constructor() { }

  ngOnInit() {
  }

}
