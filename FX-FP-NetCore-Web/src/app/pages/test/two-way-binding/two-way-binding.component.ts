import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-two-way-binding',
  templateUrl: './two-way-binding.component.html',
  styleUrls: ['./two-way-binding.component.less']
})
export class TwoWayBindingComponent implements OnInit {

  fontSizePx:number=16;
  constructor() { }

  ngOnInit(): void {
  }

}
