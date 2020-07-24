import { Directive, ElementRef, OnInit } from '@angular/core';
/// <reference path="cesium.js" 
declare function createModel(viewer:any): any;

@Directive({
  selector: '[appCesium]'
})
export class CesiumDirective implements OnInit {

  constructor(private el: ElementRef) {}

  ngOnInit() {
    Cesium.Ion.defaultAccessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjMWI5MzYwOS1lYzUxLTQwOGQtOGU3Zi0wNmI0NGMwZDk2MWYiLCJpZCI6MzA5NjYsInNjb3BlcyI6WyJhc3IiLCJnYyJdLCJpYXQiOjE1OTQ2NDYxMjd9.iFDbV7fce-NeiPCeU15gCalxlnJ3kxzA8HG2kLYW9fY';
    const viewer = new Cesium.Viewer(this.el.nativeElement);
    createModel(viewer);
  }

}
