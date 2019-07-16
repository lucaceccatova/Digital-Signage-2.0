import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-dashboard-element',
  templateUrl: './dashboard-element.component.html',
  styleUrls: ['./dashboard-element.component.scss']
})
export class DashboardElementComponent implements OnInit {

  elements=[
    {name:"HOME", path:"",icon:"home"},
    {name:"MEDIA",path:"/media",icon:'movie'},
    {name:"CATEGORY",path:"/category",icon:'view_module'},

  ];
  constructor() { }

  ngOnInit() {
  }

}
