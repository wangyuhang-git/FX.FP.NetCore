import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-jichu',
  templateUrl: './jichu.component.html',
  styleUrls: ['./jichu.component.less']
})
export class JichuComponent implements OnInit {

  @Input() size: number | string = 0;
  @Output() sizeChange = new EventEmitter<number>();

  userName: string = '';

  constructor() { }

  ngOnInit(): void {
  }

  dec() {
    this.resizer(-1);
  }

  inc() {
    this.resizer(+1);
  }

  resizer(delta: number) {
    this.size = Math.min(40, Math.max(8, +this.size + delta));
    this.sizeChange.emit(this.size);
  }

}
