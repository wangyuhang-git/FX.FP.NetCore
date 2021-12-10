import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { city } from '../city';

@Component({
  selector: 'app-jichu',
  templateUrl: './jichu.component.html',
  styleUrls: ['./jichu.component.less']
})
export class JichuComponent implements OnInit {

  @Input() size: number | string = 0;
  @Output() sizeChange = new EventEmitter<number>();

  userName: string = '';

  show: boolean = true;

  citys: Array<any> = [{ id: 1, name: '平湖' }, { id: 2, name: '海盐' }, { id: 3, name: '经开区' }, { id: 4, name: '海宁' }];

  city: string = '经开区';

  phoneStr!: string;

  submitMessage: any;

  firstExample: any;

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

  toggleShow() {
    this.show = !this.show;
  }

  callPhone(phone: string) {
    this.phoneStr = phone;
    console.log(this.phoneStr);
  }

  onSubmit(form: NgForm) {
    this.submitMessage = '姓名:' + form.form.value.name + '，身份证号码:' + form.form.value.idcard;
    console.log(form);
  }

  trackByCity(index: number, item: city) {
    return item.id;
  }

  changeCitys(){
    this.citys= [{ id: 1, name: '平湖' }, { id: 2, name: '海盐' }, { id: 3, name: '经开区' }, { id: 4, name: '海宁' }, { id: 5, name: '开化' }];
  }

}
