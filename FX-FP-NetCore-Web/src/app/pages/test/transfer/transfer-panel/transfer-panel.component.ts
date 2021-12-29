import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { transfer } from './transfer';

@Component({
  selector: 'app-transfer-panel',
  templateUrl: './transfer-panel.component.html',
  styleUrls: ['./transfer-panel.component.less']
})
export class TransferPanelComponent implements OnInit, OnChanges {
  @Input() list: transfer[] = [];
  @Input() showSearchBox = false;
  selectList: transfer[] = [];
  showList: transfer[] = [];
  @Output() changed = new EventEmitter<transfer[]>();


  constructor() {

  }

  ngOnChanges(changes: SimpleChanges): void {
    //console.log(changes);
    const { list } = changes;
    if (list) {
      //this.selectList = [];
      this.showList = list.currentValue.slice();
      this.selectList = this.list.filter(c => c.checked);//filter返回的是一个新的数组
    }
  }

  ngOnInit(): void {
  }

  tragetIndex(key: string): number {
    return this.selectList.findIndex(c => c.key == key);
  }

  itemClick(item: transfer) {
    const index = this.tragetIndex(item.key);
    if (index == -1) {
      this.selectList.push(item);
    } else {
      this.selectList.splice(index, 1);
    }
    //console.log(item);
    this.changed.emit(this.selectList);
  }

  onInput(event: Event) {
    const { value } = event.target as HTMLInputElement;//这里需要断言一下
    //console.log("value",value);
    this.showList = this.list.filter(c => c.value.includes(value));
  }
}
