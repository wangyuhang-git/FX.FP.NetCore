import { Component, OnInit } from '@angular/core';
import { transfer } from './transfer';

@Component({
  selector: 'app-transfer-panel',
  templateUrl: './transfer-panel.component.html',
  styleUrls: ['./transfer-panel.component.less']
})
export class TransferPanelComponent implements OnInit {

  list: transfer[] = [];
  constructor() {
    this.setList();
  }

  ngOnInit(): void {
  }

  setList() {
    let item = 'item' + Date.now().toString().slice(-3);
    for (let i = 0; i < 20; i++) {
      this.list.push({
        checked: i % 6 == 0,
        key: item + '_' + i,
        value: `${item}_${i + 1}`
      });
    }
  }

  tragetIndex(key: string): number {
    return this.list.findIndex(c => c.key = key);
  }
}
