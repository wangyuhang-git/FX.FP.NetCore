import { Component, OnInit } from '@angular/core';
import { FormControl,FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup;

  constructor(private fb:FormBuilder) {
    this.loginForm = this.fb.group({
      'username':[''],
      'password':['']
    });
   }

  ngOnInit() {
  }

  login(){
    console.log(this.loginForm.value);
  }
}
