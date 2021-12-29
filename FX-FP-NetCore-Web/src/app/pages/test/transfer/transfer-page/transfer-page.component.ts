import { Component, OnInit } from '@angular/core';
import { transfer } from '../transfer-panel/transfer';

@Component({
  selector: 'app-transfer-page',
  templateUrl: './transfer-page.component.html',
  styleUrls: ['./transfer-page.component.less']
})
export class TransferPageComponent implements OnInit {

  list: transfer[] = [];
  constructor() {
    this.setList();
  }

  ngOnInit(): void {
  }
  setList() {
    this.list = [];
    let item = 'item' + Date.now().toString().slice(-3);
    for (let i = 0; i < 20; i++) {
      this.list.push({
        checked: i % 6 === 0,
        key: item + '_' + i,
        value: `${item}_${i + 1}`
      });
    }
  }

  onChanged(selectList: transfer[]) {
    console.log(selectList);
  }
}
