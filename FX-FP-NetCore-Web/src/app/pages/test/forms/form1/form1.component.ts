import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-form1',
  templateUrl: './form1.component.html',
  styleUrls: ['./form1.component.less']
})
export class Form1Component implements OnInit {

  oneForm: FormGroup;
  formData = { username: '', password: '', email: '', address: { city: '', street: '' } };

  name:any;
  constructor(private fb: FormBuilder) {
    this.oneForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      email: [''],
      address: this.fb.group({
        city: ['', Validators.required],
        street: ['']
      })
    });
  }

  ngOnInit(): void {
  }

  submit() {
    console.log(this.oneForm.value);
  }

  get username(){
    return this.oneForm.get('username');
  }
}
